using Microsoft.EntityFrameworkCore;
using MinionUrl.Data;
using Microsoft.Extensions.DependencyInjection;
using MinionUrl.Controllers;
using Microsoft.Data.SqlClient;
using System.Text;

namespace MinionUrl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=MinionUrlDb;" +
                    "Trusted_Connection=True; MultipleActiveResultSets=true; Integrated Security=True;";
            var sqlConnection = new SqlConnection(connectionString);

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MinionUrlContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MinionUrlContext") ?? throw new InvalidOperationException("Connection string 'MinionUrlContext' not found.")));
            // Add services to the container.

            builder.Services.

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
                string a = req.ToString();
                if (a[1] != 'm')
                    return next();

                var enterUrl = new StringBuilder();
                enterUrl.Append(req.Request.Path.ToString());
                enterUrl.Remove(0, 1);
                string getUrlFromDb = $"SELECT FullUrl FROM UrlData WHERE ShortUrl='{enterUrl}'";
                var cmGetUrlFromDb = new SqlCommand(getUrlFromDb, sqlConnection);
                sqlConnection.Open();
                try
                {
                    var reader = cmGetUrlFromDb.ExecuteReader();
                    string exitUrl = reader.ToString();
                    req.Response.Redirect($"{exitUrl}");
                }
                catch
                {
                    sqlConnection.Close();
                    throw new Exception("Can't redirect. Possible invalid link");
                }
                sqlConnection.Close();
                return next();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}