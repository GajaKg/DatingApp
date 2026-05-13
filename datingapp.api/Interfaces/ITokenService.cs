using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingapp.api.Entities;

namespace datingapp.api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}