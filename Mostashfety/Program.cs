
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.Managers.Implementation;
using Mostashfety.DAL.Context;
using Mostashfety.DAL.Models;
using Mostashfety.DAL.Repos.Abstract;
using Mostashfety.DAL.Repos.Implementation;
using System.Text;

namespace Mostashfety
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("defaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
            // identity
            builder.Services.AddIdentity<User, IdentityRole<int>>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            builder.Services.AddIdentityCore<Admin>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityCore<Doctor>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            // Repos
            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IAppoinmentRepo, AppoinmentRepo>();
            builder.Services.AddScoped<IMedicalRepo, MedicalRepo>();

            // Services (Managers)
            builder.Services.AddScoped<IDoctorManager, DoctorManager>();
            builder.Services.AddScoped<IPatientManager, PatientManager>();
            builder.Services.AddScoped<IAdminManager, AdminManager>();
            builder.Services.AddScoped<IAppointmentManager, AppointmentManager>();
            builder.Services.AddScoped<IMedicalManager, MedicalManager>();

            //Identity 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                  options =>
                  {
                      options.LoginPath = "/Account/Login"; // Set login path
                      options.LogoutPath = "/Account/Logout"; // Set logout path
                      options.AccessDeniedPath = "/Account/AccessDenied";
                  });
          

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(AppRoles.Admin), policy => policy.RequireRole(nameof(AppRoles.Admin)));
                options.AddPolicy(nameof(AppRoles.Doctor), policy => policy.RequireRole(nameof(AppRoles.Doctor)));
               
            });
          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
