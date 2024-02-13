using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatabaseSynchServiceUsingWorkerService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Table1Entity> Table1 { get; set; }
        public DbSet<Table2Entity> Table2 { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-1O9GM127\\SQLEXPRESS;Database=DatabaseSynch;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table1Entity>().HasData(
                new Table1Entity { Id = 1, Name = "SmartPhone", Contact = "9876546987" },
                new Table1Entity { Id = 2, Name = "Refrigerators", Contact = "9879046987" },
                new Table1Entity { Id = 3, Name = "TV", Contact = "9812346987" },
                new Table1Entity { Id = 4, Name = "AC", Contact = "9876765212" }
            );

            
        }
    }
}
