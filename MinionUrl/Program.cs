using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinionUrl.Data;

namespace MinionUrl
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

            //Inject DbContext
            builder.Services.AddDbContext<MinionUrlDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MinionUrlConnectionString")
            ));

            builder.Services.AddCors((setup) =>
            {
                setup.AddPolicy("nonRestricted", (options) =>
                {
                    //TODO change restrictions for API
                    options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("nonRestricted");

            app.UseHttpsRedirection();



            app.Use((req, next) =>
            {

                return next();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}