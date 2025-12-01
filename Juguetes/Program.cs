
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Domain.Entities;
using Infraestructure.ContextDB;
using Infraestructure.Repositories.API;

namespace Juguetes
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

            //Base de datos
            builder.Services.AddSqlServer<Context>(builder.Configuration.GetConnectionString("AppConnection"));

            //Servicios
            builder.Services.AddScoped<IJuguetes, JuguetesServices>();
            builder.Services.AddScoped<IJuguetesRepository, JuguetesRepository>();

            builder.Services.AddScoped<ILoginUser, LoginServices>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddSingleton<LoginMapping>();
            builder.Services.AddSingleton<JuguetesMapping>();

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
