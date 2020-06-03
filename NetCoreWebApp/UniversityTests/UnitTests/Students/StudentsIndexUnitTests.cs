using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreWebApp.Controllers;
using NetCoreWebApp.Models;
using NetCoreWebApp.Persistence;
using NetCoreWebApp.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UniversityTests.UnitTests.Students
{
    [TestFixture]
    public class StudentsIndexUnitTests
    {
        [Test]
        public void IndexMethod_OnlyStudentsWithGrades_Correct()
        {
            //Arrange
            var dbLayer = new Mock<IStudentsRepository>();
            dbLayer.Setup(d => d.GetStudents()).Returns(new List<Student>()
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski", Address="Warszawa, Złota 12"},
                new Student{IdStudent=2, FirstName="Andrzej", LastName="Malewski", Address="Warszawa, Kolorowa 12"},
                new Student{IdStudent=3, FirstName="Mariusz", LastName="Andrzejewski", Address="Warszawa, Błękitna 23"}
            });
            dbLayer.Setup(d => d.GetGrades(1)).Returns(new List<Grade>()
            {
                new Grade{IdGrade=1, IdStudent=1, GradeValue=4, Subject="Matematyka dyskretna", SubjectType="Group 1"}
            });
            dbLayer.Setup(d => d.GetGrades(2)).Returns(new List<Grade>()
            {
                new Grade{IdGrade=2, IdStudent=2, GradeValue=3, Subject="Matematyka dyskretna", SubjectType="Group 1"},
                new Grade{IdGrade=3, IdStudent=2, GradeValue=5, Subject="Relacyjne bazy danych", SubjectType="Group 1"}
            });
            dbLayer.Setup(d => d.GetGrades(3)).Returns(new List<Grade>()
            {
                new Grade{IdGrade=4, IdStudent=3, GradeValue=3, Subject="Matematyka dyskretna", SubjectType="Group 1"},
                new Grade{IdGrade=5, IdStudent=3, GradeValue=4, Subject="Wychowanie fizyczne", SubjectType="Group 2"}
            });

            var cont = new StudentsController(dbLayer.Object);

            //Act
            var result = cont.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            var vr = (ViewResult)result;
            Assert.IsNotNull(vr.Model);
            Assert.IsTrue(vr.Model is IEnumerable<StudentViewModel>);
            var vm = (IEnumerable<StudentViewModel>)vr.Model;
            Assert.IsTrue(vm.Count() == 3);

            //Average and ECTS grade of first student
            Assert.IsTrue(vm.ElementAt(0).AverageGrade == 4);
            Assert.IsTrue(vm.ElementAt(0).EctsSum == 5);

            //...
        }

        [Test]
        public void IndexMethod_OnlyStudentWithoutGrades_Correct()
        {
            //Arrange
            var dbLayer = new Mock<IStudentsRepository>();
            dbLayer.Setup(d => d.GetStudents()).Returns(new List<Student>()
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski", Address="Warszawa, Złota 12"}
            });

            var cont = new StudentsController(dbLayer.Object);

            //Act
            var result = cont.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            var vr = (ViewResult)result;
            Assert.IsNotNull(vr.Model);
            Assert.IsTrue(vr.Model is IEnumerable<StudentViewModel>);
            var vm = (IEnumerable<StudentViewModel>)vr.Model;
            Assert.IsTrue(vm.Count() == 1);

            //Average and ECTS grade of first student
            Assert.IsTrue(vm.ElementAt(0).AverageGrade == 0);
            Assert.IsTrue(vm.ElementAt(0).EctsSum == 0);

            //...
        }
        
        [Test]
        public void IndexMethod_NoStudents_Correct()
        {
            var dbLayer = new Mock<IStudentsRepository>();
            dbLayer.Setup(d => d.GetStudents()).Returns(new List<Student>());
            var controller = new StudentsController(dbLayer.Object);
            var result = controller.Index();

            Assert.IsTrue(result is ViewResult);
            var viewResult = (ViewResult) result;
            Assert.IsTrue(viewResult.Model is IEnumerable<StudentViewModel>);
            var viewResultModel = (IEnumerable<StudentViewModel>) viewResult.Model;
            Assert.IsTrue(viewResultModel.Count() == 0);
        }

        [Test]
        public void IndexMethod_StudentsWithGradesAndWithoutGrades_Correct()
        {
            var dbLayer = new Mock<IStudentsRepository>();
            dbLayer.Setup(d => d.GetStudents()).Returns(new List<Student>()
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski", Address="Warszawa, Złota 12"},
                new Student{IdStudent=2, FirstName="Andrzej", LastName="Malewski", Address="Warszawa, Kolorowa 12"},
                new Student{IdStudent=3, FirstName="Mariusz", LastName="Andrzejewski", Address="Warszawa, Błękitna 23"}
            });
            dbLayer.Setup(d => d.GetGrades(1)).Returns(new List<Grade>()
            {
                new Grade{IdGrade=1, IdStudent=1, GradeValue=4, Subject="Matematyka dyskretna", SubjectType="Group 1"}
            });
            dbLayer.Setup(d => d.GetGrades(3)).Returns(new List<Grade>()
            {
                new Grade{IdGrade=4, IdStudent=3, Subject="Matematyka dyskretna", SubjectType="Group 1"},
                new Grade{IdGrade=5, IdStudent=3, GradeValue=4, Subject="Wychowanie fizyczne", SubjectType="Group 2"}
            });

            var controller = new StudentsController(dbLayer.Object);
            var result = controller.Index();

            Assert.IsTrue(result is ViewResult);
            var viewResult = (ViewResult) result;
            Assert.IsNotNull(viewResult.Model);
            Assert.IsTrue(viewResult.Model is IEnumerable<StudentViewModel>);
            var viewResultModel = (IEnumerable<StudentViewModel>) viewResult.Model;
            Assert.IsTrue(viewResultModel.Count() == 3);
            Assert.IsTrue(viewResultModel.ElementAt(0).AverageGrade == 4);
            Assert.IsTrue(viewResultModel.ElementAt(0).EctsSum == 5);
            Assert.IsTrue(viewResultModel.ElementAt(1).AverageGrade == 0);
            Assert.IsTrue(viewResultModel.ElementAt(1).EctsSum == 0);
        }
    }
}
