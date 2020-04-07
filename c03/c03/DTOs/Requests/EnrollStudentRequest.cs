using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace c03.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}