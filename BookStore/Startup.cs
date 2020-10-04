using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(@"Server=DESKTOP-L1720AC\SQLEXPRESS01; Database=BookStore; Integrated Security=True;"));
            services.AddControllersWithViews();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();

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
