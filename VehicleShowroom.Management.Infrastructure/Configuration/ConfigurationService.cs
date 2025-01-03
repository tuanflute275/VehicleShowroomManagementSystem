﻿using AspNetCoreHero.ToastNotification;
using AutoMapper;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NToastNotify;
using Serilog;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Services;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.DataAccess.Repository;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Infrastructure.Services;
using VehicleShowroom.Management.Infrastructure.Abstracts;

namespace VehicleShowroom.Management.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


            //
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                  {
                      // Cấu hình cookie authentication
                      options.Cookie = new CookieBuilder
                      {
                          HttpOnly = true,
                          Name = "ShowroomManagmentSystem.cookie", // Đặt tên cookie
                          Path = "/",
                          SameSite = SameSiteMode.Lax,
                          SecurePolicy = CookieSecurePolicy.SameAsRequest
                      };

                      options.LoginPath = "/login"; // Đường dẫn login
                      options.AccessDeniedPath = "/accessDenied";
                      options.ReturnUrlParameter = "returnUrl"; // Tham số đường dẫn trả về
                      options.SlidingExpiration = true; // Bật tính năng hết hạn session động
                  });
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                    options.AddPolicy("Invoice", policy => policy.RequireRole("Admin"));
                    options.AddPolicy("Employee", policy => policy.RequireRole("Admin"));
                });


            services.AddRazorPages()
               .AddNToastNotifyNoty(new NotyOptions
               {
                   ProgressBar = true,
                   Timeout = 5000 // Thời gian hiển thị thông báo
               });

            // Cấu hình Notyf với các tùy chọn cho thông báo
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5; // Thời gian hiển thị thông báo
                config.IsDismissable = true; // Cho phép đóng thông báo
                config.Position = NotyfPosition.TopRight; // Vị trí thông báo
                config.HasRippleEffect = true; // Hiệu ứng ripple khi click vào thông báo
            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<FileUploadHelper>();
            services.AddTransient<IPDFService, PDFService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ICompanyServices, CompanyService>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
            services.AddTransient<ISalesOrderService, SalesOrderService>();
            services.AddTransient<IStockHistoryService, StockHistoryService>();
            services.AddTransient<IBillingService, BillingService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Information()
                                .Enrich.FromLogContext()
                                .WriteTo.Console()
                                .WriteTo.File(
                                   System.IO.Path.Combine("LogFiles", "log.txt"),
                                   rollingInterval: RollingInterval.Day,
                                   fileSizeLimitBytes: 10 * 1024 * 1024, //10mb
                                   retainedFileCountLimit: 2
                                 )
                                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
