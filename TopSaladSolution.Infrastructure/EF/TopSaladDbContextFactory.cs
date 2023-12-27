using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TopSaladSolution.Infrastructure.EF
{
    public class TopSaladDbContextFactory : IDesignTimeDbContextFactory<TopSaladDbContext>
    {
        public TopSaladDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TopSaladSolutionDb");
            var optionsBuilder = new DbContextOptionsBuilder<TopSaladDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TopSaladDbContext(optionsBuilder.Options);
        }
    }
}
