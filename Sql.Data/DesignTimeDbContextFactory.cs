using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sql.Data
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {


        public TestDbContext CreateDbContext(string[] args)
        {
            string? value = "Server=ChrisServer;Initial Catalog=NetLayered;Persist Security Info=False;User ID=sa;Password=1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;";
            DbContextOptionsBuilder<TestDbContext> builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseSqlServer(value);
            TestDbContext ctx = new TestDbContext(builder.Options);
            return ctx;
        }
    }
}
