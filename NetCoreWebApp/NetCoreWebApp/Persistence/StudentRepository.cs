using NetCoreWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWebApp.Persistence
{
    public class StudentRepository : IStudentsRepository
    {
        public readonly UniversityDbContext _context;

        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public int AddStudent(Student newStudent)
        {
            _context.Add(newStudent);
            _context.SaveChanges();

            return newStudent.IdStudent;
        }

        public IEnumerable<Grade> GetGrades(int studentId)
        {
            return _context.Grades
                          .Where(g => g.IdStudent == studentId)
                          .ToList();
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Students
                               .OrderBy(s => s.LastName)
                               .ThenBy(s => s.FirstName)
                               .ToList();
        }
    }
}
