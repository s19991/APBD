using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApp.Models
{
    public class Student
    {
        [Key]
        public int IdStudent { get; set; }

        [Required, MaxLength(150)]
        [Display(Name ="Imię")]
        public string FirstName { get; set; }

        [Required, MaxLength(150)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required, MaxLength(255)]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
