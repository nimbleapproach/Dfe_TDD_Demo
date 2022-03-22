using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;


namespace DfE_Testing1
{

    //Following the process of TDD and keeping it simple, in the first instance we're wanting to write a test
    //to ensure that when our CRMService.AddReport method is called the returning value is not empty.

    //In a real world scenario each of these steps would be accompanied by a Red Light, Green Light, Refactor method of approach.
    //Initially the test would be failing (Red Light) and in order to ensure the test passes we would then add the 
    //corresponding implementation code (Green Light). From this, each 'Step' in this example begins the Refactor process before the cycle continues.


    public class Step1
    {

        // Fact when we have some criteria that always must be met, regardless of data.
        [Fact]
        public void Create_A_Pothole_Report_Test()
        {

            //throw (new NotImplementedException());

            //Act
            ICRMService service = new CRMService();
            var response = service.AddPotholeReport();
            
            //Assert
            response.Should().NotBeEmpty();
        }
    }
 
    public interface ICRMService
    {
        string AddPotholeReport();
    }

    public class CRMService : ICRMService
    {
        public string AddPotholeReport()
        {
            //return string.Empty;
            return Guid.NewGuid().ToString();
        }
    }
}
