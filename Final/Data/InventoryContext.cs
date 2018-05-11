using Microsoft.EntityFrameworkCore;
using Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
            modelBuilder.Entity<Detail>().ToTable("Detail");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Department>().ToTable("Department");

        }
    }
}
