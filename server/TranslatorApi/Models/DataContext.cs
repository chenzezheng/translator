using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace TranslatorApi.Models
{
     public class DataContext : DbContext
      {
            public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

            public DbSet<User> Users { get; set; }
            public DbSet<Token> Tokens { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Answer> Answers { get; set; }
      }
}
