using System;
using System.Collections.Generic;
using System.Linq;
using c03.DTOs.Requests;
using c03.DTOs.Responses;
using c03.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace c03.DAL
{
    public class EfDbService : IDbService
    {
        private readonly s19991Context _context;

        public EfDbService(s19991Context context)
        {
            _context = context;
        }

        public c03.Models.Student GetStudent(string indexNumber)
        {
            throw new System.NotImplementedException();
        }

        public GetStudentsResponse GetStudents()
        {
            GetStudentsResponse response = new GetStudentsResponse();
            
            try
            {
                response.Students = _context.Student.ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn\'t get students due to: {e.StackTrace} {e.Message}");
            }

            return response;
        }

        public void ModifyStudent(ModifyStudentRequest request)
        {
            try
            {
                var student = (from s in _context.Student where s.IndexNumber == request.IndexNumber select s).Single();
                student.FirstName = request?.FirstName ?? student.FirstName;
                student.LastName = request?.LastName ?? student.LastName;
                student.BirthDate = request?.BirthDate ?? student.BirthDate;
                student.IdEnrollment = request?.IdEnrollment ?? student.IdEnrollment;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"Couldn\'t update student {request.IndexNumber} due to: {e.StackTrace} {e.Message}"
                    );
            }
        }

        public void DeleteStudent(DeleteStudentRequest request)
        {
            try
            {
                var student = (from s in _context.Student where s.IndexNumber == request.IndexNumber select s).Single();
                _context.Student.Remove(student);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"Couldn\'t delete student {request.IndexNumber} due to: {e.StackTrace} {e.Message}"
                );
            }
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            // todo zerknac czy git, ale chyba ok
            EnrollStudentResponse response;
            try
            {
                Console.WriteLine($"Looking for studies {request.Studies}");
                if (!_context.Studies.Any(s => s.Name == request.Studies))
                {
                    throw new Exception($"Unknown studies {request.Studies}");
                }

                Console.WriteLine($"Looking for enrollment {request.Studies}");
                int idEnrollment;
                var idStudy = (
                    from s in _context.Studies where s.Name == request.Studies select s.IdStudy
                ).Single();
                if (_context.Enrollment.Any())
                {
                    idEnrollment = (from e in _context.Enrollment
                        where e.Semester == 1
                              && e.IdStudy == idStudy
                              && e.StartDate == _context.Enrollment
                                  .Where(x => x.Semester == 1 && x.IdStudy == idStudy)
                                  .Max(x => x.StartDate)
                        select e
                    ).Single().IdEnrollment;
                }
                else
                {
                    Enrollment enrollment = new Enrollment
                    {
                        IdEnrollment = _context
                            .Enrollment
                            .OrderByDescending(x => x.IdEnrollment)
                            .FirstOrDefault()
                            .IdEnrollment,
                        Semester = 1,
                        IdStudy = idStudy,
                        StartDate = DateTime.Now
                    };
                    _context.Enrollment.Add(enrollment);
                    idEnrollment = enrollment.IdEnrollment;
                }
                
                Console.WriteLine($"Looking for student {request.IndexNumber}");
                if (!_context.Student.Any(s => s.IndexNumber == request.IndexNumber))
                {
                    Console.WriteLine("Creating student...");
                    Student student = new Student
                    {
                        IndexNumber = request.IndexNumber,
                        FirstName = request.Firstname,
                        LastName = request.LastName,
                        BirthDate = request.BirthDate,
                        IdEnrollment = idEnrollment
                    };
                    _context.Student.Add(student);
                }

                Console.WriteLine("Saving...");
                _context.SaveChanges();
                response = new EnrollStudentResponse
                {
                    LastName = request.LastName,
                    Semester = 1,
                    StartDate = DateTime.Now
                };
            }
            catch (Exception e)
            {
                throw new Exception($"Somme error occured:\n{e.StackTrace}\n{e.Message}");
            }

            return response;
        }

        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            // todo zerknac czy git, ale chyba ok
            PromoteStudentResponse response = new PromoteStudentResponse();
            response.Enrollments = new List<object>();
            try
            {
                Console.WriteLine($"Looking for studies {request.Studies} and semester {request.Semester}");
                if (!_context.Studies
                    .Join(
                        _context.Enrollment,
                        st => st.IdStudy,
                        e => e.IdStudy,
                        (st, e) => new {Studies = st, Enrollment = e}
                    )
                    .Any(
                        x => x.Studies.Name == request.Studies && x.Enrollment.Semester == request.Semester
                    )
                )
                {
                    throw new Exception($"Couldn't find students on {request.Studies} {request.Semester}");
                }

                Console.WriteLine($"Updating enrollments");
                var idStudy = (
                    from s in _context.Studies where s.Name == request.Studies select s.IdStudy
                ).Single();
                var previousEnrollmentId = (from e in _context.Enrollment
                        where e.Semester == request.Semester && e.IdStudy == idStudy
                        select e
                    ).Single().IdEnrollment;

                int newEnrollmentId;
                if (_context.Enrollment.Any(e => e.Semester == request.Semester + 1 && e.IdStudy == idStudy))
                {
                    // todo narzeka na Sequence contains more than one element
                    newEnrollmentId = (from e in _context.Enrollment
                        where e.Semester == request.Semester + 1 && e.IdStudy == idStudy
                        select e
                    ).Single().IdEnrollment;
                }
                else
                {
                    Enrollment tmp = new Enrollment
                    {
                        IdEnrollment = _context
                            .Enrollment
                            .OrderByDescending(x => x.IdEnrollment)
                            .FirstOrDefault()
                            .IdEnrollment,
                        Semester = request.Semester+1,
                        IdStudy = idStudy,
                        StartDate = DateTime.Now
                    };
                    _context.Enrollment.Add(tmp);
                    newEnrollmentId = tmp.IdEnrollment;
                }
                
                var enrollment = (
                    from e in _context.Enrollment where e.IdEnrollment == previousEnrollmentId select e
                ).Single();
                enrollment.IdEnrollment = newEnrollmentId;
                _context.SaveChanges();

                Console.WriteLine($"Getting all new promoted records");
                response = new PromoteStudentResponse
                {
                    Enrollments = new List<object>
                    {
                        (
                            from e in _context.Enrollment
                            where e.Semester == request.Semester + 1 && e.IdStudy == idStudy
                            select e
                        ).ToList()
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception($"Somme error occured:\n{e.StackTrace}\n{e.Message}");
            }

            return response;
        }

        public LoginResponse Login(string login, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}