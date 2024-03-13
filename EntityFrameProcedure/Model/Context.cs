using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado3.Model
{
    partial class Context:DbContext
    {

        static DbContextOptions<Context> _options;

        static Context()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            _options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesManagers> SalesManagers { get; set; }




        public Context() : base(_options)
        {
            if (Database.EnsureCreated())
            {
                
            }
        }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_dbo.Customers");

            });



        /*    
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Id);
               
               
                entity.HasOne(e => e.Type)
                      .WithMany(pt => pt.Products)
                      .HasForeignKey(e => e.ProductTypeId);
                
                entity.HasMany(e => e.Sales)
                      .WithOne(s => s.Products)
                      .HasForeignKey(s => s.ProductId);
            });

            
            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.Id);
              
                
                entity.HasMany(e => e.Products)
                      .WithOne(p => p.Type)
                      .HasForeignKey(p => p.ProductTypeId);
            });

           
            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Products)
                      .WithMany(p => p.Sales)
                      .HasForeignKey(e => e.ProductId);
                
                entity.HasOne(e => e.SalesManagers)
                      .WithMany(sm => sm.Sales)
                      .HasForeignKey(e => e.SalesManagerId);
               
                entity.HasOne(e => e.Customer)
                      .WithMany(c => c.Sales)
                      .HasForeignKey(e => e.CustomerId);
            });

           
            modelBuilder.Entity<SalesManagers>(entity =>
            {
                entity.HasKey(e => e.Id);
              
               
                entity.HasMany(e => e.Sales)
                      .WithOne(s => s.SalesManagers)
                      .HasForeignKey(s => s.SalesManagerId);
            });*/

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }



}
