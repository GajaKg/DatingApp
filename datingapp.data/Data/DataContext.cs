using datingapp.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace datingapp.data.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
    }
}