using datingapp.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options
        ) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

    }
}