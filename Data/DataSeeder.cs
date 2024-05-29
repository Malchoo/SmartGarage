using Microsoft.EntityFrameworkCore;
using SmartGarage.Models;

namespace SmartGarage.Data
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed VehicleBrands
            modelBuilder.Entity<VehicleBrand>().HasData(
                new VehicleBrand { Id = 1, Name = "BMW" },
                new VehicleBrand { Id = 2, Name = "Mercedes-Benz" },
                new VehicleBrand { Id = 3, Name = "Audi" }
            );

            // Seed VehicleModels
            modelBuilder.Entity<VehicleModel>().HasData(
                new VehicleModel { Id = 1, Name = "320d" },
                new VehicleModel { Id = 2, Name = "530i" },
                new VehicleModel { Id = 3, Name = "E220" },
                new VehicleModel { Id = 4, Name = "S400" },
                new VehicleModel { Id = 5, Name = "A4" },
                new VehicleModel { Id = 6, Name = "Q7" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, IsEmployee = false, Username = "johndoe", Email = "john@example.com", PhoneNumber = "1234567890", Password = "password123", PasswordResetToken = "" },
                new User { Id = 2, IsEmployee = false, Username = "janedoe", Email = "jane@example.com", PhoneNumber = "0987654321", Password = "password456", PasswordResetToken = "" },
                new User { Id = 3, IsEmployee = true, Username = "employee1", Email = "employee1@example.com", PhoneNumber = "1112223333", Password = "password789", PasswordResetToken = "" }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { UserId = 1 },
                new Customer { UserId = 2 }
            );

            // Seed Vehicles
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, CustomerId = 1, BrandId = 1, ModelId = 1, LicensePlate = "ABC123", VIN = "WBA1234567890", YearOfCreation = 2018 },
                new Vehicle { Id = 2, CustomerId = 2, BrandId = 2, ModelId = 3, LicensePlate = "XYZ789", VIN = "WDB9876543210", YearOfCreation = 2020 }
            );

            // Seed Services
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Oil Change", Price = 50.00m },
                new Service { Id = 2, Name = "Tire Rotation", Price = 30.00m }
            );

            // Seed ServiceOrders
            modelBuilder.Entity<ServiceOrder>().HasData(
                new ServiceOrder { Id = 1, CustomerId = 1, VehicleId = 1, ServiceId = 1, Date = DateTime.Now, Status = ServiceOrderStatus.NotStarted },
                new ServiceOrder { Id = 2, CustomerId = 2, VehicleId = 2, ServiceId = 2, Date = DateTime.Now, Status = ServiceOrderStatus.InProgress }
            );
        }
    }
}