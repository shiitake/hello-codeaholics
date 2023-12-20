using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloCodeaholics.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCodeaholics.Infrastructure;

public partial class HelloCodeDbContext : DbContext
{
    public HelloCodeDbContext() { }

    public HelloCodeDbContext(DbContextOptions<HelloCodeDbContext> options) : base(options) { }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
