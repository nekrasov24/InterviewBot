using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Models
{
    public class TelegramContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<QuestionDiscription> QuestionDiscription { get; set; }
        public DbSet<User> Users { get; set; }

        public TelegramContext(DbContextOptions<TelegramContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=dbTGService;Database=TelegramService;User Id=sa;Password=Your_password123;");
        }
    }
}
