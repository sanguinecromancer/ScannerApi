using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scanner.Core.Services;
using ScannerApi.Models;

namespace ScannerApi
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<URLContext>(opt =>
                opt.UseInMemoryDatabase("URLList"));
            services.AddTransient<ScannerService>();
            services.AddTransient<VirusScanService>();
            services.AddMvc();
            services.AddCors();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
        }
    }
}
