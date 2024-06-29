using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Main St" }
            );
        }
    }
}
