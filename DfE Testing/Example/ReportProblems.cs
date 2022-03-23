using Moq;
using System;
using FluentAssertions;
using Xunit;

namespace DfE_Testing
{
    public class ReportingProblemsTests
    {
        //xUnit supports two different types of unit test, Fact and Theory

        // TDD resources 
        // Visual Studio Series on Youtube
        //  https://www.youtube.com/watch?v=HhRvW1b4IwM
        // C# and other Languages with TDD
        // https://tdd.tools/
        // And of course Microsoft docs
        /// https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
        ///Nice intro to Xunit
        /// https://auth0.com/blog/xunit-to-test-csharp-code/
        /// 


        // Some basic requirements
        // Needs a way for customers to report a problem e.g a pothole in the road etc.
        // Service for recording the report
        // The report needs to go into the councils CRM system
        // need a way to update the customer with a CRM ref number via text / email

        // Fact when we have some criteria that always must be met, regardless of data.
        [Fact]
        public void Create_A_Pothole_Report_Test()
        {
            // Arrange
            var pothole = MakeAHole();
            pothole.ReportDate = DateTime.Now;
            pothole.ReportedBy = "Test User";
            pothole.ReportId=  Guid.NewGuid();
            pothole.ReportName = "Pothole Report";

            // Act
            var reportingService = new TestableReportingService(new Mock<ICRMService>(),new Mock<ICommsService>());

            //Assert
            reportingService.Report(pothole).Should().BeTrue();
        }

        [Fact]
        public void Create_A_CRM_Case_Test()
        {
            //Arrange
            Mock<ICRMService> service = new Mock<ICRMService>();
            service.DefaultValue = DefaultValue.Mock;
            //Act and Assert
            service.Object.CreateCase().Should().NotBeEmpty();

        }

        //Theory on the other hand depends on set of parameters and data, our test will pass for some set of this data but not in all cases
        //  A theory is a parametric unit test that allows you to represent a set of unit tests sharing the same structure.
        //  Theories allow you to implement what is called data-driven testing, which is a testing approach heavily based on input data variation.
        // useful for validation rules.

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Validate_Is_Public_Highway_Input(bool isPublic)
        { 
            IRequestValidator validator = new RequestValidator();
            validator.IsPublicHighwayValid(isPublic).Should().Be(isPublic); 
        }

        // Test Helper
        public Pothole MakeAHole()
        {
            return new Pothole(){ 
                Description= "A very big hole in the road",
                Image = "https://unsplash.com/photos/-TQUERQGUZ8",
                IsHazard=true,
                IsPublicHighway=true,
                Location="53.945692, -1.102211",
                PotholeSize= "dustbin lid",                            
                };
        }
    }


    // Classes
    public abstract class ReportingProblems
    {
        public Guid ReportId { get; set; }
        public string CRMId {get;set;}
        public string ReportName { get; set; } 
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string ReportedBy { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime ReportDate {get;set;} 


    }

    public class Pothole:ReportingProblems
    {

        public string PotholeSize {get;set;}
        public bool IsPublicHighway {get;set;}
        public bool IsHazard {get;set;}

    }

    public class Message
    {
        public Guid MessageId { get; set; }
        public string Name {get;set;}
        public string SendAddress {get;set;}
        public string Content {get;set;}
        public string Header {get;set;}
    }


    // Interfaces
    public interface IReportingService
    {
        public bool Report(ReportingProblems problems);
    }


    public interface ICRMService
    {
        string CreateCase();
        
    }

    public interface ICommsService
    {
        bool SendMessage(Message messsage);
    }


    public interface IRequestValidator
    {
        bool IsPublicHighwayValid(bool highwayCheck );      
    }

    // Implementations
    public class ReportingService : IReportingService
    {
        ICommsService commsService;
        ICRMService crmService;

        public ReportingService(ICRMService crm, ICommsService comms )
        {
            commsService = comms;
            crmService = crm;
        }
        public bool Report(ReportingProblems problems)
        {
            crmService.CreateCase();
            commsService.SendMessage(new Message());
            return true;
        }
    }

    public class TestableReportingService : ReportingService
    {
        public Mock<ICommsService> comms;
        public Mock<ICRMService> crm;

        public TestableReportingService(Mock<ICRMService> crmSrv, Mock<ICommsService> commSrv): base (crmSrv.Object, commSrv.Object)
        {
            comms = commSrv;
            crm = crmSrv;
        }
    }


    public class RequestValidator : IRequestValidator
    {
        public bool IsPublicHighwayValid(bool highwayCheck)
        {
           if (highwayCheck)
            { 
                return true;
            }
            return false;
        }
    }
}