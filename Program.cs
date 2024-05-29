using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartGarage.Data;
using SmartGarage.Models;
using SmartGarage.Repostiories;
using SmartGarage.Repostiories.Contracts;
using SmartGarage.Services;
using SmartGarage.Services.Contracts;

namespace SmartGarage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
            builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddRazorPages();

            // Add cookie authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // Specify the path for the login page
                options.AccessDeniedPath = "/Account/AccessDenied"; // Specify the path for the access denied page
            });

            // Register the AuthenticationService
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            //builder.Services.AddScoped<IEmailService, EmailService>();

            // Add authorization policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomerOnly", policy =>
                {
                    policy.RequireRole("Customer");
                });

                options.AddPolicy("EmployeeOnly", policy =>
                {
                    policy.RequireRole("Employee");
                });
            });

            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Add Web API services
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.AddHttpClient<CurrencyApiService>();
            builder.Services.AddControllersWithViews();

            // Add Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartGarage API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartGarage API V1");
                });
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllers();

            app.Run();
        }
    }
}