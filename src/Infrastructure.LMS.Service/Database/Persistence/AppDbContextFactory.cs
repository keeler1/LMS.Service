using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.LMS.Service.Database.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var startupProjectPath = AppContext.BaseDirectory; // always absolute
            var config = new ConfigurationBuilder()
                .SetBasePath(startupProjectPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // Read provider and connection strings
            var provider = config["DatabaseProvider"];
            string connectionString = provider switch
            {
                "SqlServer" => config.GetConnectionString("SqlServer"),
                "MySql" => config.GetConnectionString("MySql"),
                _ => throw new InvalidOperationException($"Unsupported provider: {provider}")
            };

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            else if (provider.Equals("MySql", StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
