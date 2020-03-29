using System;

namespace c03.Models
{
    public class Student
    {
        public int IdStudent { get; set; }
        public string IndexNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        
        public int IdEnrollment { get; set; }
    }
}