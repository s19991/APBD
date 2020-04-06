using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using c03.DTOs.Requests;
using c03.DTOs.Responses;
using c03.Models;

namespace c03.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student
                {
                    IdStudent = 1, 
                    IndexNumber = "s003",
                    FirstName = "Jan", 
                    LastName = "Kowalski",
                    BirthDate = new DateTime(1990, 2, 3),
                    IdEnrollment = 1
                },
                new Student
                {
                    IdStudent = 2,
                    IndexNumber = "s002",
                    FirstName = "Anna", 
                    LastName = "Annowicz",
                    BirthDate = new DateTime(1990, 1, 18),
                    IdEnrollment = 2
                },
                new Student
                {
                    IdStudent = 3,
                    IndexNumber = "s003",
                    FirstName = "Andrzej",
                    LastName = "Andrzejewicz",
                    BirthDate = new DateTime(1990, 11, 12),
                    IdEnrollment = 3
                }
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            throw new NotImplementedException();
        }

        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}