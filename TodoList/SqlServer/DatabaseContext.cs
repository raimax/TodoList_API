using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.SqlServer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(builder =>
            {
                builder.HasMany(x => x.TodoItems).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId);
            });

            modelBuilder.Entity<TodoItem>(builder =>
            {
                builder.HasOne(x => x.AppUser).WithMany(x => x.TodoItems);
                builder.Property(x => x.AppUserId).IsRequired();
            });
        }
    }
}
