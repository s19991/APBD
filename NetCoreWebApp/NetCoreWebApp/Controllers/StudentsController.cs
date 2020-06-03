using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Models;
using NetCoreWebApp.Persistence;
using NetCoreWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWebApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _repository;

        public StudentsController(IStudentsRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var students = _repository.GetStudents();

            var dataForView = new List<StudentViewModel>();
            foreach (var st in students)
            {
                var vm = new StudentViewModel();
                vm.IdStudent = st.IdStudent;
                vm.FirstName = st.FirstName;
                vm.LastName = st.LastName;
                vm.Address = st.Address;

                var grades = _repository.GetGrades(st.IdStudent);
                foreach (var g in grades)
                {
                    if (g.SubjectType == "Group 1")
                    {
                        vm.EctsSum += 5;
                    }
                    else if (g.Subject == "Group 2")
                    {
                        vm.EctsSum += 3;
                    }
                    else if (g.SubjectType == "Group 3")
                    {
                        vm.EctsSum += 1;
                    }

                    vm.AverageGrade += g.GradeValue;
                }

                if (grades.Count() > 0)
                {
                    vm.AverageGrade = vm.AverageGrade / grades.Count();
                }

                dataForView.Add(vm);
            }

            return View(dataForView);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            var newStudentId = _repository.AddStudent(student);

            return RedirectToAction("Index", new { newStudentId });
        }
    }
}