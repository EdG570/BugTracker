using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Core.Services;
using BugTracker.Infrastructure.Context;
using BugTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BugTrackerCleanArch
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
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<IdentityAppContext>();

            services.AddDbContext<IdentityAppContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly("BugTracker.Infrastructure"));
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserProjectRepository, UserProjectRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserProjectService, UserProjectService>();
            services.AddScoped<IAppUserService, AppUserService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Project}/{action=Index}/{id?}");
            });
        }
    }
}
