global using Microsoft.EntityFrameworkCore;
global using PareaBoard.Api.Data;
global using PareaBoard.Api.Data.Entities;
global using PareaBoard.Shared.Players;
using PareaBoard.Api.Endpoints;

namespace PareaBoard.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<PareaBoardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.MapPlayerEndpoints();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
