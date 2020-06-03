using NetCoreWebApp.Models;
using NetCoreWebApp.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace UniversityTests.UnitTests.Services
{
    [TestFixture]
    public class EctsCounterUnitTests
    {
        [Test]
        public void EctsService_CalculateEctsForGroup1()
        {
            //Arrange
            var service = new EctsService();
            var grades = new List<Grade>()
            {
                new Grade{IdGrade=1, IdStudent=1, GradeValue=3, SubjectType="Group 1", Subject="Matematyka dyskretna"},
                new Grade{IdGrade=2, IdStudent=1, GradeValue=5, SubjectType="Group 1", Subject="Matematyka dyskretna"}
            };

            //Act
            int result = service.CalculateEctsSum(grades);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result == 10);
        }
    }
}
