using Microsoft.EntityFrameworkCore;

namespace Syfora_Test.Domain
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Login).IsRequired();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
            });
        }
    }
}
