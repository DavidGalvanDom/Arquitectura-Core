using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Charly.Core.Web.Data
{
     public class ContextdbFactory : IDesignTimeDbContextFactory<Contextdb>
    {
       
        //////// 
        public Contextdb CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Contextdb>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new Contextdb(builder.Options);

            // Add-Migration InitialCreate
            // Add-Migration
            // Update-Database
        }
    }    
}
