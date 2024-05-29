﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartGarage.Data;

#nullable disable

namespace SmartGarage.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240529143931_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SmartGarage.Models.Customer", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsDeleted = false
                        },
                        new
                        {
                            UserId = 2,
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Oil Change",
                            Price = 50.00m
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Tire Rotation",
                            Price = 30.00m
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.ServiceOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ServiceOrders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            Date = new DateTime(2024, 5, 29, 17, 39, 31, 358, DateTimeKind.Local).AddTicks(8570),
                            IsDeleted = false,
                            ServiceId = 1,
                            Status = 0,
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            Date = new DateTime(2024, 5, 29, 17, 39, 31, 358, DateTimeKind.Local).AddTicks(8626),
                            IsDeleted = false,
                            ServiceId = 2,
                            Status = 1,
                            VehicleId = 2
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmployee")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordResetToken")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("PasswordResetTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john@example.com",
                            IsDeleted = false,
                            IsEmployee = false,
                            Password = "password123",
                            PasswordResetToken = "",
                            PhoneNumber = "1234567890",
                            Username = "johndoe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane@example.com",
                            IsDeleted = false,
                            IsEmployee = false,
                            Password = "password456",
                            PasswordResetToken = "",
                            PhoneNumber = "0987654321",
                            Username = "janedoe"
                        },
                        new
                        {
                            Id = 3,
                            Email = "employee1@example.com",
                            IsDeleted = false,
                            IsEmployee = true,
                            Password = "password789",
                            PasswordResetToken = "",
                            PhoneNumber = "1112223333",
                            Username = "employee1"
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int>("YearOfCreation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CustomerId = 1,
                            IsDeleted = false,
                            LicensePlate = "ABC123",
                            ModelId = 1,
                            VIN = "WBA1234567890",
                            YearOfCreation = 2018
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            CustomerId = 2,
                            IsDeleted = false,
                            LicensePlate = "XYZ789",
                            ModelId = 3,
                            VIN = "WDB9876543210",
                            YearOfCreation = 2020
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.VehicleBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("VehicleBrands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "BMW"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Mercedes-Benz"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Audi"
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("VehicleModels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "320d"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "530i"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "E220"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "S400"
                        },
                        new
                        {
                            Id = 5,
                            IsDeleted = false,
                            Name = "A4"
                        },
                        new
                        {
                            Id = 6,
                            IsDeleted = false,
                            Name = "Q7"
                        });
                });

            modelBuilder.Entity("SmartGarage.Models.Customer", b =>
                {
                    b.HasOne("SmartGarage.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("SmartGarage.Models.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartGarage.Models.ServiceOrder", b =>
                {
                    b.HasOne("SmartGarage.Models.Customer", "Customer")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SmartGarage.Models.Service", "Service")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartGarage.Models.Vehicle", "Vehicle")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Service");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("SmartGarage.Models.Vehicle", b =>
                {
                    b.HasOne("SmartGarage.Models.VehicleBrand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartGarage.Models.Customer", "Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartGarage.Models.VehicleModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Customer");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("SmartGarage.Models.Customer", b =>
                {
                    b.Navigation("ServiceOrders");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("SmartGarage.Models.Service", b =>
                {
                    b.Navigation("ServiceOrders");
                });

            modelBuilder.Entity("SmartGarage.Models.Vehicle", b =>
                {
                    b.Navigation("ServiceOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
