using APILabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace APILabb3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Hobbie> Hobbies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Link> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseSqlServer(connectionString: "Server=LAPTOP-7LJQQL26;Database=Labb3API;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true");

    }
}
