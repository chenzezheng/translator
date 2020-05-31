using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransDB.Models;
using System.IO;

namespace TransDB
{
     public class DataContext : DbContext
      {
            private IConfiguration configuration;

            public DataContext()
            {
                configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString("transDataBase"));
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Token> Tokens { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Answer> Answers { get; set; }
      }
}
