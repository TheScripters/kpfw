﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using kpfw.Services;
using kpfw.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using kpfw.Models.Identity;
using kpfw.Models;
using Stripe;

namespace kpfw
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.GetSection("Kpfw").Get<KpfwSettings>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = settings.CookieName;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = true;
            });

            services.AddRazorPages();
            services.AddAntiforgery(opts => opts.Cookie.Name = settings.Antiforgery);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(settings.ConnectionString));
            services.AddIdentity<User, UserRole>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();

            if (settings.UseHsts)
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(730);
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext db, SignInManager<User> s)
        {
            UpdateDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //if (s.UserManager.FindByNameAsync("dev").Result == null)
                //{
                //    var result = s.UserManager.CreateAsync(new User
                //    {
                //        UserName = "dev",
                //        UserEmail = "dev@app.com",
                //    }, "Aut94L#G-a").Result;
                //}
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpModule();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict, Secure = CookieSecurePolicy.Always, HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=AdminHome}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "caps",
                    pattern: "Caps/{Episode}/{num:int?}",
                    defaults: new { controller = "Caps", action = "ViewEpisode" });

                endpoints.MapControllerRoute(
                    name: "guides",
                    pattern: "Guides/{Episode?}",
                    defaults: new { controller = "Episode", action = "Index" });

                // this is a catch-all. It MUST BE LAST
                endpoints.MapControllerRoute(
                    name: "pages",
                    pattern: "{*PageUrl:regex(^[^.]+(?!.[a-z0-9A-Z]{{1,5}})$)}",
                    defaults: new { controller = "Page", action = "Index" });
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
