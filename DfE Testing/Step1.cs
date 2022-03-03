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

    public class Step1
    {
        //xUnit supports two different types of unit test, Fact and Theory


        // Fact when we have some criteria that always must be met, regardless of data.
        [Fact]
        public void Create_A_Pothole_Report_Test()
        {

            //throw (new NotImplementedException());
            var service = new CRMService();
            service.AddReport().Should().NotBeEmpty();


        }


    }


   

    public class Pothole
    {
        public string Description { get; set; }
        public string Lat { get; set; }
        public string Longitude { get; set; } 

    }

    public interface ICRMService
    {
        string AddReport();
    }


    public class CRMService : ICRMService
    {
        public string AddReport()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
