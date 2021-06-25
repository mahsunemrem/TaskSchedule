using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.Presentation.Business.Interfaces;
using TaskSchedule.Presentation.Business.Services;
using TaskSchedule.Presentation.Business.Utility;
using TaskSchedule.Presentation.Domain.Entities;
using TaskSchedule.Presentation.Domain.Intercaces;
using TaskSchedule.Presentation.Infrastructure;
using TaskSchedule.Presentation.Infrastructure.Repositories;

namespace TaskSchedule.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddControllersWithViews();

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();

            services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 

        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
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

            app.UseHangfireDashboard("/MEHangfire",new DashboardOptions {
                DashboardTitle = "Mahsun Emrem - Hangfire",
                AppPath = "utility/hangfire"
            });
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));


            lifetime.ApplicationStarted.Register(
            () => Statics.CreateScheduleJob(app, serviceProvider));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }
    }

    public static class Statics
    {
        [Obsolete]
        internal static void CreateScheduleJob(IApplicationBuilder app, IServiceProvider serviceProvider)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                BackgrondJobService.RecurrringJobs(() => _unitOfWork.Products.Add(new Product
                {
                    ProductName = "Test",
                    UnitPrice = 200,
                    UnitsInStock = 500
                }), "addProduct");
            }
        }
    }
}
