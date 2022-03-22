using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DfE_Testing2;
using FluentAssertions;
using NuGet.Frameworks;
using Xunit;

namespace DfE_TestingExercise1
{
    //Next requirement - Start setting up the test for ensuring the message is sent
    //Following on from our existing flow we don't want to add this into our implementation at this point- we're wanting to test 
    //the functionality in isolation, ensuring the new test fails and our implementation comes after.

    //At this point we don't care about the value of the message that's passed through, just that the initial stub is setup
    //The next step would cover value checks.

    //So initially we want to write our test to setup a message object, and ensure its passed into the Sender service.
    public class Step1
    {               
        //
        [Fact]
        public void Create_A_Report_Response_Test()
        {
            //Arrange
            //- Arrange a new Message() object (this can have empty values at this point, we just want the object)

            //Act
            //- Setup an instance of your EmailService
            //- Act upon the .SendMessage() method

            //Assert
            //- Ensure our response value is correct.
          
        }
    }

    public class Message
    {
        //No properties at this point - they could be added but in the first instance we're not too bothered about their values.
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
}
