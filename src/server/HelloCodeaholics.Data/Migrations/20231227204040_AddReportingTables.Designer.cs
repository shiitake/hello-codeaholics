﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelloCodeaholics.Data.Migrations
{
    [DbContext(typeof(HelloCodeDbContext))]
    [Migration("20231227204040_AddReportingTables")]
    partial class AddReportingTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DrugName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnitCount")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Delivery", (string)null);
                });

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Pharmacist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfHire")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<string>("PrimaryDrugSold")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Pharmacist", (string)null);
                });

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("HelloCode");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilledPrescriptionsCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Zip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pharmacy", (string)null);
                });

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Zip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Warehouse", (string)null);
                });

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Delivery", b =>
                {
                    b.HasOne("HelloCodeaholics.Common.Domain.Entities.Pharmacy", "Pharmacy")
                        .WithMany("Deliveries")
                        .HasForeignKey("PharmacyId")
                        .IsRequired();

                    b.HasOne("HelloCodeaholics.Common.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Pharmacist", b =>
                {
                    b.HasOne("HelloCodeaholics.Common.Domain.Entities.Pharmacy", "Pharmacy")
                        .WithMany("Pharmacists")
                        .HasForeignKey("PharmacyId")
                        .IsRequired();

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("HelloCodeaholics.Common.Domain.Entities.Pharmacy", b =>
                {
                    b.Navigation("Deliveries");

                    b.Navigation("Pharmacists");
                });
#pragma warning restore 612, 618
        }
    }
}
