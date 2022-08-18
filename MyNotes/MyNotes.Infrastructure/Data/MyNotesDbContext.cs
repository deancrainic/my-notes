using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Entities;

namespace MyNotes.Infrastructure.Data
{
    public class MyNotesDbContext : IdentityDbContext<User>
    {
        public MyNotesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}
