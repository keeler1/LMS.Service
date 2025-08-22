
using Infrastructure.LMS.Service;
using Microsoft.EntityFrameworkCore;

namespace Web.LMS.Service
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

            var provider = builder.Configuration.GetValue<string>("DatabaseProvider");

            if (provider == "SqlServer")
            {
                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
            }
            else if (provider == "MySql")
            {
                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(
                        builder.Configuration.GetConnectionString("MySql"),
                        new MySqlServerVersion(new Version(8, 0, 39))
                    ));
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
