using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorDB.Models;

namespace TranslatorDB.Models
{
     public class DataContext : DbContext
    {
            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {
                this.Database.EnsureCreated(); //自动建库建表
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Token> Tokens { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Answer> Answers { get; set; }
    }
}
