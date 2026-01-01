using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MilitaryApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MilitaryApp.Data
{
    public class MilitaryContext: DbContext
    {
        public DbSet<Military> Militaries { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<King> Kings { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<ViewMilitary> viewMilitary { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder
            //    .UseSqlServer("Data Source=DESKTOP-GME0IFH\\SQLEXPRESS01;Initial Catalog = MilitaryDB; Integrated Security = True;TrustServerCertificate=True");

            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                // For enabling sensitive data 
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=DESKTOP-GME0IFH\\SQLEXPRESS01;Initial Catalog = MilitaryDB; Integrated Security = True;TrustServerCertificate=True"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MilitaryBattle>().HasKey(m => new {
                m.MilitaryId,
                m.BattleId
            });
            modelBuilder.Entity<Military>()
               .HasOne(m => m.King)
               .WithMany()
               .HasForeignKey(m => m.KingId);

            modelBuilder.Entity<ViewMilitary>().HasNoKey().ToView("getBattle");

        }

        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
            builder.AddConsole();
            });
    }
}
