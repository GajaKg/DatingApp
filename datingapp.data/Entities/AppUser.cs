
using datingapp.data.Extensions;

namespace datingapp.data.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; } = String.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; } = String.Empty;
        public string Introduction { get; set; } = String.Empty;
        public string LookingFor { get; set; } = String.Empty;
        public string Interests { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public List<Photo> Photos { get; set; } = new List<Photo>();

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }
}