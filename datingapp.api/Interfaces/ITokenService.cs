using datingapp.data.Entities;

namespace datingapp.api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}