using Asp_Lesson1.Abstractions;
using Asp_Lesson1.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.FileProviders;
using System.Runtime.ConstrainedExecution;

namespace Asp_Lesson1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            string connection = builder.Configuration.GetConnectionString("db");
            var versionString = builder.Configuration.GetConnectionString("Version");
            var version = new MySqlServerVersion(new Version(versionString));


            builder.Services.AddDbContext<ProductsContext>(dbContextOptions => { dbContextOptions.UseLazyLoadingProxies(); dbContextOptions.UseMySql(connection, version); });

            builder.Services.AddMemoryCache(option => option.TrackStatistics = true);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            Directory.CreateDirectory(staticFilesPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                RequestPath = "/static"
            });
            app.MapControllers();

            app.Run();
        }
    }
}
