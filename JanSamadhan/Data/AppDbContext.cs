using Microsoft.EntityFrameworkCore;
using JanSamadhan.Models;
namespace JanSamadhan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the User-Issue relationship
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.User)
                .WithMany(u => u.Issues)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
