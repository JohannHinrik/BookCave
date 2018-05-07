using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookCave
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
            services.AddDbContext<AuthenticationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(config =>
            {   
                //User settings
                //No 2 users can have the same email
                config.User.RequireUniqueEmail = true;
                //Password settings for users
                //Require a specific passwords length
                config.Password.RequiredLength = 6;
            });

            services.ConfigureApplicationCookie(options =>
            {
                //Cookie settings:
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(4);
                //If the LoginPath isn't set, ASP.NET Core defaults
                //the path to /Account/Login:
                options.LoginPath = "/Account/LogIn";
                //If the AccessDeniedPath isn't set, AST.NET Core defaults
                //the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //Middleware added to enable authentication capabilities
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    internal class ApplicationUser
    {
    }
}
