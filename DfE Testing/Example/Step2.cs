using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DfE_Testing2
{

    //After setting up the initial test in Step1 we then want to refactor the service method to actually allow a pothole to be included in the call.
    //Again, at this point we're refactoring the test to include the ability to pass the pothole as a parameter, we don't initially care what the return value is (this comes later)
    public class Step2
    {

        [Fact]
        public void Create_A_Pothole_Report_Test()
        {
            //Assign
            var pothole = MakeAHole();

            //Act
            ICRMService service = new CRMService();
            var response = service.AddPotholeReport(pothole);
            
            //Assert
            response.Should().NotBeEmpty();
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

        public interface ICRMService
        {
            string AddPotholeReport(Pothole hole);
        }


        public class CRMService : ICRMService
        {


            public string AddPotholeReport(Pothole hole)
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
