using Moq;
using System;
using FluentAssertions;
using Xunit;

namespace DfE_Testing
{
    public class ReportingProblemsTests


        // Some basic requirements
        // needs a way for customers to report a problem e.g a pothole in the road, flytipping, litter etc
        // service for recording the report

        // the report needs to go into the councils CRM system

        // need a way to update the customer with a CRM ref number via text / email
        // and maybe on the outcome of their report (this would be very useful for when a councillor reports a problem as they like to know what's happening
        //



        // TDD resources 
        // Visual Studio Series on Youtube
        //  https://www.youtube.com/watch?v=HhRvW1b4IwM
        // C# and other Languages with TDD
        // https://tdd.tools/
        // And of course Microsoft docs
        /// https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test

    {
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




    // Test Helpers

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


}