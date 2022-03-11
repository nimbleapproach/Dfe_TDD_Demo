using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfE_Testing2;
using FluentAssertions;
using NuGet.Frameworks;
using Xunit;

namespace DfE_Testing3
{

    
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
    ///Nice intro to Xunit
    /// https://auth0.com/blog/xunit-to-test-csharp-code/
    /// 
    public class Step3
    {

        // Fact when we have some criteria that always must be met, regardless of data.
        [Fact]
        public void Create_A_Pothole_Report_Test()
        {

            var pothole = MakeAHole();
            ICRMService service = new CRMService();
            var response = service.AddPotholeReport(pothole);
            response.CaseId.Should().NotBeEmpty();


        }

       
        [Fact]
        public void Create_A_Report_Response_Test()
        {

            var message = new Message()
            {
                Content = "Testing 1234",
                MessageID = 1,
                RecipientAddress = "fred@testing.com",
                Subject ="Test Message"
            };
            IMessage sender = new EmailService();
            var response = sender.SendMessage(message);
            response.Should().BeTrue();



        }


        public Pothole MakeAHole()
        {
            return new Pothole()
            {
                Description = "A very big hole in the road",
                ImageURL = "https://unsplash.com/photos/-TQUERQGUZ8",
                IsHazard = true,
                IsPublicHighway = true,
                Latitude = "53.945692",
                Longitude = " -1.102211",
                PotholeSize = "dustbin lid",
            };
        }
    }




    public abstract class ReportingProblems
    {
        public Guid ReportId { get; set; }
        public string CRMId { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImageURL { get; set; }
        public string ReportedBy { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime ReportDate { get; set; }


    }

    public class Pothole : ReportingProblems
    {

        public string PotholeSize { get; set; }
        public bool IsPublicHighway { get; set; }
        public bool IsHazard { get; set; }

    }



    public class CRMResponse
    {
        public int ResponseId { get; set; }
        public Guid CaseId { get; set; }
        public string FriendlyCaseReference { get; set; }


    }


    public class Message
    {
        public int MessageID { get; set; }
        public string RecipientAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

    }

    public interface IMessage
    {
        bool SendMessage(Message message);
    }

    public class EmailService:IMessage
    {
        public bool SendMessage(Message message)
        {
            return true;
        }
    }

    public interface ICRMService
    {
        CRMResponse AddPotholeReport(Pothole hole);
    }


    public class CRMService : ICRMService
    {


        public CRMResponse AddPotholeReport(Pothole hole)
        {
            return new CRMResponse()
            {
                CaseId = Guid.NewGuid(),
                FriendlyCaseReference = "123456789",
                ResponseId = 10001
            };
        }
    }


}

