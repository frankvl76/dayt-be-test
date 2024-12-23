using Dayt.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dayt.EF.Data
{
    public class EventDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<EventEntity>().Property(e => e.Title).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<EventEntity>().Property(e => e.Description).HasMaxLength(1000);
            modelBuilder.Entity<EventEntity>().Property(e => e.City).IsRequired().HasMaxLength(100);
        }
    }
}