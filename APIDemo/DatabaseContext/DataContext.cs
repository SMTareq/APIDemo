using APIDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.DatabaseContext
{
    public class DataContext : DbContext
    {

        public DbSet<Customer> Customers  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=DESKTOP-V83OHUL\SQLEXPRESS;Database=APIDB; Integrated Security=true";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
