using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ado4Customer.Model;

public partial class Ado4Context : DbContext
{
    public Ado4Context()
    {
    }

    public Ado4Context(DbContextOptions<Ado4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesManager> SalesManagers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BORIS;Database=ado4;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerCompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CostPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK__Products__Produc");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK__ProductT__A1312F4E01C9813C");

            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C41FA18A5CD7");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SalesManagerId).HasColumnName("SalesManagerID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Sales__CustomerI__4222D4EF");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Sales__ProductID__403A8C7D");

            entity.HasOne(d => d.SalesManager).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SalesManagerId)
                .HasConstraintName("FK__Sales__SalesMana__412EB0B6");
        });

        modelBuilder.Entity<SalesManager>(entity =>
        {
            entity.HasKey(e => e.SalesManagerId).HasName("PK__SalesMan__28AE036C4817E18F");

            entity.Property(e => e.SalesManagerId).HasColumnName("SalesManagerID");
            entity.Property(e => e.ManagerName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
