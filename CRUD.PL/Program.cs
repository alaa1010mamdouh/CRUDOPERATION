using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.BLL.Interfaces;
using CRUD.BLL.Repositories;
using CRUD.DAL.Context;
using CRUD.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CRUD.PL.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace CRUD.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

          Builder.Services.AddControllersWithViews();
            Builder.Services.AddDbContext<CRUDDbContext>(Options =>
            {
                Options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection"));
            });//Allow Dependency injection
            Builder.Services.AddScoped<IDepartmentRepository, Departmentrepository>();
            Builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            Builder.Services.AddAutoMapper(m => m.AddProfile(new UserProfile()));
            Builder.Services.AddAutoMapper(m => m.AddProfile(new RoleProfile()));
            Builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<CRUDDbContext>()
            .AddDefaultTokenProviders();
            Builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(op =>
                {
                    op.LoginPath = "Account/Login";
                    op.AccessDeniedPath = "Home/Error";

                });
          var app=  Builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

            app.Run();
        }

        
    }
}
