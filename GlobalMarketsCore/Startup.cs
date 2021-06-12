using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GlobalMarketsCore.Data;
using GlobalMarketsCore.Models;
using GlobalMarketsCore.Services;
using GlobalMarketsCore.AppHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using GlobalMarketsCore.Hubs;

namespace GlobalMarketsCore
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
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });


            services.AddSingleton<IFileProvider>(
                 new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                //config.SignIn.RequireConfirmedEmail = true;

            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddMvc(config =>
            {
         

            });

            services.AddSignalR();
 


            // Add Global services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




            // Add application services.
            services.AddScoped<IClientService, ClientService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {


            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookiePolicy();

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<MessageHub>("message");
            //});

            app.UseSignalR();

            app.UseMvc();


            app.UseStaticFiles();

            app.UseAuthentication();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

        }


    }
}
