using NetCoreWebApp.Models;
using System.Collections.Generic;

namespace NetCoreWebApp.Persistence
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();
        IEnumerable<Grade> GetGrades(int studentId);
        int AddStudent(Student newStudent);
    }
}
