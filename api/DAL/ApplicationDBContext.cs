using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./users.sqlite");
        }
    }
}
