using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<UserRow> UserRows { get; set; }
        public DbSet<UserColumn> UserColumns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserColumn>()
                .Property(c => c.DataType)
                .HasConversion<string>();

        }

    }
}