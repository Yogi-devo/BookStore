using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Helpers;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();
            services.Configure<IdentityOptions>(Option =>
            {
                Option.Password.RequiredLength = 4;
                Option.Password.RequiredUniqueChars = 1;
                Option.Password.RequireDigit = false;
                Option.Password.RequireLowercase = false;
                Option.Password.RequireUppercase= false;
                Option.Password.RequireNonAlphanumeric = false;
            });
            services.AddControllersWithViews();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = _configuration["Applicatio:LoginPath"];
            });

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped < IUserService, UserService > ();
            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
            services.AddScoped<IEmailService, EmailService>();

#if DEBUG

            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option=> 
            {
                option.HtmlHelperOptions.ClientValidationEnabled = false;
            });
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("  Hellow from first Middleware");

            //    await next();

            //    await context.Response.WriteAsync("  Hellow from first Middleware");
            //});

            // app.Use(async (context, next) =>
            // {
            //     await context.Response.WriteAsync("  Hellow from Second Middleware");

            //     await next();

            //     await context.Response.WriteAsync("  Hellow from Second Middleware");
            // });

            // app.Use(async (context, next) =>
            // {
            //     await context.Response.WriteAsync("  Hellow from Third Middleware");

            //     await next();
            // });
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFile")),
            //    RequestPath= "/MyStaticFile"

            //}) ; 
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //    if (env.IsDevelopment())
            //    { 
            //    await context.Response.WriteAsync(" Hello from Development");
            //    }
            //       else if (env.IsProduction())
            //        {
            //            await context.Response.WriteAsync(" Hello from Production");
            //        }
            //       else if (env.IsStaging())
            //        {
            //            await context.Response.WriteAsync(" Hello from Staging");
            //        }
            //       else
            //        {
            //            await context.Response.WriteAsync(env.EnvironmentName);
            //        }
            //    });
            //});
        }
    }
}
