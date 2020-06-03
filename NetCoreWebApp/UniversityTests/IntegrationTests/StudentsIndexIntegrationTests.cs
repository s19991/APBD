using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Controllers;
using NetCoreWebApp.Models;
using NetCoreWebApp.Persistence;
using NetCoreWebApp.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UniversityTests.IntegrationTests
{
    [TestFixture]
    public class StudentsIndexIntegrationTests
    {
        [Test]
        public void IndexMethod_OnlyStudentsWithGrades_Correct()
        {
            //Arrange
            var repository = new StudentRepository(new UniversityDbContext());
            var controller = new StudentsController(repository);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            var vr = (ViewResult)result;
            Assert.IsNotNull(vr.Model);
            Assert.IsTrue(vr.Model is IEnumerable<StudentViewModel>);
            var vm = (IEnumerable<StudentViewModel>)vr.Model;

            Assert.IsTrue(vm.Count() == 5);
            Assert.IsTrue(vm.ElementAt(0).LastName == "Kowalewski");
        }

    }
}
