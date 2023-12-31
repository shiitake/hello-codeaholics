﻿using HelloCodeaholics.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCodeaholics.Data;

public partial class HelloCodeDbContext : DbContext
{
    public HelloCodeDbContext() { }

    public HelloCodeDbContext(DbContextOptions<HelloCodeDbContext> options) : base(options) { }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }
    public virtual DbSet<Warehouse> Warehouses { get; set; }
    public virtual DbSet<Pharmacist> Pharmacists { get; set; }
    public virtual DbSet<Delivery> Deliveries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.ToTable("Pharmacy");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Address).HasMaxLength(250).IsRequired();
            entity.Property(p => p.City).HasMaxLength(50).IsRequired();
            entity.Property(p => p.State).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Zip).IsRequired();
            entity.Property(p => p.CreatedDate).IsRequired();
            entity.Property(p => p.CreatedBy).HasMaxLength(50).IsRequired().HasDefaultValue("HelloCode");
            entity.Property(p => p.UpdatedBy).HasMaxLength(50).IsRequired(false);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("Warehouse");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Address).HasMaxLength(250).IsRequired();
            entity.Property(p => p.City).HasMaxLength(50).IsRequired();
            entity.Property(p => p.State).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Zip).IsRequired();
        });

        modelBuilder.Entity<Pharmacist>(entity =>
        {
            entity.ToTable("Pharmacist");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Age).IsRequired();
            entity.Property(p => p.PrimaryDrugSold).HasMaxLength(150).IsRequired();
            entity.Property(p => p.DateOfHire).IsRequired();
            entity.HasOne(p => p.Pharmacy)
                .WithMany(p => p.Pharmacists)
                .HasForeignKey(p => p.PharmacyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.ToTable("Delivery");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.DrugName).HasMaxLength(50).IsRequired();
            entity.Property(p => p.UnitCount).IsRequired();
            entity.Property(p => p.DeliveryDate).IsRequired();
            entity.HasOne(p => p.Pharmacy)
                .WithMany(p => p.Deliveries)
                .HasForeignKey(p => p.PharmacyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();
            entity.HasOne(p => p.Warehouse)
                .WithMany()
                .HasForeignKey(p => p.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
