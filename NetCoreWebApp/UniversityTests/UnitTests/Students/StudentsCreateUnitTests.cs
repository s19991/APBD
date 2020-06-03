using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreWebApp.Controllers;
using NetCoreWebApp.Models;
using NetCoreWebApp.Persistence;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UniversityTests.UnitTests.Students
{
    [TestFixture]
    public class StudentsCreateUnitTests
    {
        [Test]
        public void CreateMethod_AddNewStudent_Correct()
        {
            //Arrange
            var dbLayer = new Mock<IStudentsRepository>();
            var newStudent = new Student
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Address = "Kolorowa 12"
            };
            dbLayer.Setup(d => d.AddStudent(newStudent)).Returns(20);

            var cont = new StudentsController(dbLayer.Object);

            //Act
            var result = cont.Create(newStudent);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is RedirectToActionResult);
            var vr = (RedirectToActionResult)result;
            Assert.IsTrue(vr.ActionName == "Index");
            Assert.IsTrue(vr.RouteValues["newStudentId"].ToString() == "20");
        }

        [Test]
        public void CreateMethod_AddNewStudentWithMissingFirstName_Incorrect()
        {
            //Arrange
            var dbLayer = new Mock<IStudentsRepository>();
            var newStudent = new Student
            {
                FirstName = "",
                LastName = "Kowalski",
                Address = "Kolorowa 12"
            };

            var context = new ValidationContext(newStudent, null, null);
            var results = new List<ValidationResult>();

            //Act
            var isModelStateValid = Validator.TryValidateObject(newStudent, context, results, true);

            //Assert
            Assert.IsFalse(isModelStateValid);
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].MemberNames.ElementAt(0) == nameof(Student.FirstName));
        }
    }
}
