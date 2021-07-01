using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Mvc;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using class_library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Application.Controllers;
using Projektni_Zadatak_Project_Service.Data;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using Projektni_Zadatak_Project_Service.Repositories;
using System.Reflection;
using System.Web.Mvc;

namespace MVC_Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();
            services.AddOptions();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleModelsController>().PropertiesAutowired();
            builder.RegisterType<VehicleMakesController>().PropertiesAutowired();

            builder.RegisterInstance(AutoMapperConfig.Initialize());

            builder.RegisterType<SortHelper<VehicleMake>>().As<ISortHelper<VehicleMake>>();
            builder.RegisterType<SortHelper<VehicleModel>>().As<ISortHelper<VehicleModel>>();

            builder.RegisterType<VehicleMakesRepository>().As<IVehicleMakesRepository>();
            builder.RegisterType<VehicleModelsRepository>().As<IVehicleModelsRepository>();

            builder.RegisterType<VehicleModelsRepository>().As<IVehicleModelsRepository>();
            builder.RegisterType<VehicleModelsRepository>().As<IVehicleModelsRepository>();

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();

                var opt = new DbContextOptionsBuilder<ApplicationDbContext>();
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                return new ApplicationDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<VehicleService>().As<IVehicleService>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
