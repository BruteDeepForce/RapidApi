
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RapidApi.Authentication;
using RapidApi.Middlewares;
using RapidApi.Model;
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

            builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
            builder.Services.AddTransient<GenerateKey>(); /*Addtransient sisteme servis ekliyoruz ve generatekey s�n�f�n� DI i�in enjekte etmi� oluyoruz. ve art�k sistem bunu kendisi �rnekleyip kullan�ma sunuyor.*/
            builder.Services.AddTransient<Actions>();
            
            builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("RapidDatabase")));

            //Environment.SetEnvironmentVariable("Amend", "Amendment"); //burada ortam de�i�keni set ediliyor.
            //IConfigurationRoot configuration = new ConfigurationBuilder()  // Config kurma k�sm� buras�.
            //.SetBasePath(Directory.GetCurrentDirectory())   //Config nesnesi �rne�ini almak i�in kuruyoruz.
            //.AddJsonFile("appsettings.json")
            //.AddEnvironmentVariables()
            //.Build();

            //var env = configuration["TEMP"];
            //var queryinstance = new query(configuration);

            //queryinstance.things();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseApiMiddleware();

            //app.Use(async (context, next) =>
            //{

            //    await Console.Out.WriteLineAsync("1 ba�lad�");
            //    await next();
            //    await Console.Out.WriteLineAsync("1 bitiyor");
            //});

            //app.Use(async (context, next) =>
            //{

            //    await Console.Out.WriteLineAsync("2 ba�lad�");
            //    await next();
            //    await Console.Out.WriteLineAsync("2 bitiyor");
            //});

            app.UseHttpsRedirection();

            //app.UseMiddleware<UseApiMiddleWare>();

            //app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}