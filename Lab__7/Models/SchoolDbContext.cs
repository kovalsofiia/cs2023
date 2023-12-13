using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab__7.Models
{
    internal class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Documents\uzhnu 3 semester\C#\repos\cs2023\Lab5\SA2_Koval_Sofiia.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True");
        }
    }
}
