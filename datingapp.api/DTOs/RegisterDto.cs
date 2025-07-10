using System.ComponentModel.DataAnnotations;

namespace datingapp.api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = String.Empty;
    }
}