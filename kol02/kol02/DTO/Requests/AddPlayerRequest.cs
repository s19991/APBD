using System;
using System.ComponentModel.DataAnnotations;

namespace kol02.DTO.Requests
{
    public class AddPlayerRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int NumOnShirt { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}