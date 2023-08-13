using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class PRN231_Product_ManagementContext : DbContext
    {
        public PRN231_Product_ManagementContext()
        {
        }

        public PRN231_Product_ManagementContext(DbContextOptions<PRN231_Product_ManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Export> Exports { get; set; } = null!;
        public virtual DbSet<Import> Imports { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Spot> Spots { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryType)
                    .HasMaxLength(50)
                    .HasColumnName("category_type");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Export>(entity =>
            {
                entity.ToTable("Export");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("export_date");

                entity.Property(e => e.ExportPrice)
                    .HasColumnType("money")
                    .HasColumnName("export_price");

                entity.Property(e => e.ExportQuantity).HasColumnName("export_quantity");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Exports)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Export_Product");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Exports)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Export_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Export_User");
            });

            modelBuilder.Entity<Import>(entity =>
            {
                entity.ToTable("Import");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("import_date");

                entity.Property(e => e.ImportPrice)
                    .HasColumnType("money")
                    .HasColumnName("import_price");

                entity.Property(e => e.ImportQuantity).HasColumnName("import_quantity");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Imports)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Import_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Imports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Import_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProfitMoney)
                    .HasColumnType("money")
                    .HasColumnName("profit_money");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Role1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Spot>(entity =>
            {
                entity.ToTable("Spot");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.ImportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("import_date");

                entity.Property(e => e.ImportPrice)
                    .HasColumnType("money")
                    .HasColumnName("import_price");

                entity.Property(e => e.ImportQuantity).HasColumnName("import_quantity");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.SpotCode)
                    .HasMaxLength(50)
                    .HasColumnName("spot_code");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Spots)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Spot_Product");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .HasColumnName("phone");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .HasColumnName("store_name");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .HasColumnName("phone");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(50)
                    .HasColumnName("supplier_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Money)
                    .HasColumnType("money")
                    .HasColumnName("money");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User_Role_Role"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User_Role_User"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("User_Role");

                            j.IndexerProperty<int>("UserId").HasColumnName("user_id");

                            j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
