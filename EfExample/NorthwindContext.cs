using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLibrary
{
    public class NorthwindContext : DbContext
    {   
       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                "server=localhost;database=northwind;uid=root;pwd=puggle;SslMode=none");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            //modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");

            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
            modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");

            modelBuilder.Entity<OrderDetails>().Property(x => x.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetails>().Property(x => x.UnitPrice).HasColumnName("unitprice");
            modelBuilder.Entity<OrderDetails>().Property(x => x.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderDetails>().Property(x => x.Discount).HasColumnName("discount");

            modelBuilder.Entity<Order>().Property(x => x.ShippedDate).HasColumnName("shippeddate");

            modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.OrderId, x.ProductId });



                



        }
    }
}
