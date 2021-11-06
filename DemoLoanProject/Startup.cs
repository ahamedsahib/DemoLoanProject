// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace DemoLoanProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Manager.Interface;
    using Manager.Manager;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Repository.Context;
    using Repository.Interface;
    using Repository.Repository;

    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration"> configuration parameter</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        /// configure services -all services in application
        /// </summary>
        /// <param name="services">services as parameter for Interface service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc();
            services.AddDbContextPool<UserContext>(
            options => options.UseSqlServer(this.Configuration.GetConnectionString("UserDbConnection")));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IPropertyManager, PropertyManager>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1.0",
                 new OpenApiInfo
                 {
                     Title = "DemoLoanProject",
                     Description = "Demo Loan Project",
                     Version = "1.0"
                 });

            });
            services.AddCors(options =>
            {
            options.AddPolicy(name: "CorsPolicyAllHosts",
              builder =>
             {
               builder.AllowAnyHeader();
               builder.AllowAnyMethod();
               builder.AllowAnyOrigin();
           });
       });
        }

    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="app">app as parameter</param>
        /// <param name="env">env host for web application</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicyAllHosts");
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "DemoLoanProject (V 1.0)");
            });
        }
    }
}
