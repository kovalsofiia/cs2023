using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace lab9.Models
{
    public class AppDbContext : DbContext
    {
        protected string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Documents\uzhnu 3 semester\C#\repos\cs2023\mkr___2\Zavod.mdf"";Integrated Security=True;Connect Timeout=30";
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Zavod> Zavods { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zavod>().ToTable("Zavod");
        }

    }
}
