using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTO.Requests
{
    public class LoginUserRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}