using Microsoft.EntityFrameworkCore;

namespace Sql.Data
{
    public class TestDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}