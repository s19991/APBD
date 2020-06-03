using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWebApp.Models
{
    public class Grade
    {
        [Key]
        public int IdGrade { get; set; }
        [Required, MaxLength(100)]
        public string Subject { get; set; }

        public double GradeValue { get; set; }

        [Required, MaxLength(100)]
        public string SubjectType { get; set; }

        public int IdStudent { get; set; }

        [ForeignKey("IdStudent")]
        public Student Student { get; set; }
    }
}
