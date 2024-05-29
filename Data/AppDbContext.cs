using SmartGarage.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SmartGarage.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Service>().HasKey(s => s.Id);
            modelBuilder.Entity<ServiceOrder>().HasKey(so => so.Id);
            modelBuilder.Entity<Vehicle>().HasKey(v => v.Id);

            // Configure relationships
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.UserId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Vehicles)
                .WithOne(v => v.Customer)
                .HasForeignKey(v => v.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.ServiceOrders)
                .WithOne(so => so.Customer)
                .HasForeignKey(so => so.CustomerId);


            modelBuilder.Entity<Service>()
                .HasMany(s => s.ServiceOrders)
                .WithOne(so => so.Service)
                .HasForeignKey(so => so.ServiceId);

            // Configure the Price property of the Service entity
            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServiceOrder>()
                .HasKey(so => so.Id);

            modelBuilder.Entity<ServiceOrder>()
                .Property(so => so.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ServiceOrder>()
                .HasOne(so => so.Vehicle)
                .WithMany(v => v.ServiceOrders)
                .HasForeignKey(so => so.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServiceOrder>()
                .HasOne(so => so.Customer)
                .WithMany(c => c.ServiceOrders)
                .HasForeignKey(so => so.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed data if needed
            // modelBuilder.Entity<Service>().HasData(
            //     new Service { Id = 1, Name = "Oil Change", Price = 50.00m },
            //     new Service { Id = 2, Name = "Brake Replacement", Price = 150.00m }
            // );

            // modelBuilder.Entity<Customer>().HasData(
            //     new Customer { Id = -1, Username = "customer1", Password = "password", Email = "customer1@example.com", PhoneNumber = "1234567890" }
            // );

            // Additional configurations...

            modelBuilder.Seed();
        }
    }
}

