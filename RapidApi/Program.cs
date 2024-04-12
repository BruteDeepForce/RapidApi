
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RapidApi.Authentication;
using RapidApi.Middlewares;
using RapidApi.Model;
using RapidApi.ServiceDependencies;
using System;

namespace RapidApi
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

            //builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
            //builder.Services.AddTransient<GenerateKey>(); 
            //builder.Services.AddTransient<Actions>();
            //builder.Services.AddTransient<MailActions>();

            builder.Services.AddServiceDependencies();

            builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("RapidDatabase")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCheckRemoteMiddleware();
            ////app.UseMiddleware<CheckRemoteIpAdressMiddleware>();
            app.UseApiMiddleware();


            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
