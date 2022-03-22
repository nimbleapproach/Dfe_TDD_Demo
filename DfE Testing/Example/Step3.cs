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
 
    //Now at this point we've tested that the functionality for setting up the service, and ensuring a response is returned works.
    //Now we want to check that part of the values that are returned are correct.

    //We can also start setting up the test for ensuring the message is sent.
    //Following on from our existing flow we don't want to add this into our implementation at this point- we're wanting to test 
    //the functionality in isolation, ensuring the new test fails and our implementation comes after.
    public class Step3
    {

        //Existing test refactored and updated from Step 2.
        [Fact]
        public void Create_A_Pothole_Report_Test()
        {

            //Arrange
            var pothole = MakeAHole();
            ICRMService service = new CRMService();

            //Act
            var response = service.AddPotholeReport(pothole);

            //Assert
            response.CaseId.Should().NotBeEmpty();
        }

       
        //New test to start ensuring functionality for sending is setup
        [Fact]
        public void Create_A_Report_Response_Test()
        {
            //Arrange
            var message = new Message()
            {
                Content = "Testing 1234",
                MessageID = 1,
                RecipientAddress = "fred@testing.com",
                Subject ="Test Message"
            };
            //Act
            IMessage sender = new EmailService();
            var response = sender.SendMessage(message);

            //Assert
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

