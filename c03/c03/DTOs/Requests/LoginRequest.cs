using System.ComponentModel.DataAnnotations;

namespace c03.DTOs.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}