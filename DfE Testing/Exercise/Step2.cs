using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfE_Testing2;
using FluentAssertions;
using NuGet.Frameworks;
using Xunit;

namespace DfE_TestingExercise2
{
    //Following on from the previous requirement - We've tested the initial implementation, lets flesh out the EmailService to return an object rather than a property.
    public class Step2
    {               
       //
        [Fact]
        public void Create_A_Report_Response_Test()
        {
            //Arrange
            //- Arrange a new Message() object
            var message = new Message()
            {
                Content = "Testing 1234",
                MessageID = 1,
                RecipientAddress = "fred@testing.com",
                Subject ="Test Message"
            };
            //Act
            //- Setup an instance of your EmailService
            //- Act upon the .SendMessage() method
            IMessage sender = new EmailService();
            var response = sender.SendMessage(message);

            //Assert
            //- Ensure our response value is correct.
            response.Success.Should().BeTrue();
        }
    }

    public class Message
    {
        public int MessageID { get; set; }
        public string RecipientAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public class MessageResponse
    {
        public Guid MessageSuccessID {get;set;}
        public bool Success {get;set;}
    }

    public interface IMessage
    {
        MessageResponse SendMessage(Message message);
    }

    public class EmailService : IMessage
    {
        public MessageResponse SendMessage(Message message)
        {
            return new MessageResponse
            {
                MessageSuccessID = Guid.NewGuid(),
                Success = true
            };
        }
    }
}
