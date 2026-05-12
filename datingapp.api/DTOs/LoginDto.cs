using System.ComponentModel.DataAnnotations;

namespace datingapp.api.DTOs
{
    public struct LoginDto
    {
        [Required]
        [MinLength(2)]
        public string Username { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}