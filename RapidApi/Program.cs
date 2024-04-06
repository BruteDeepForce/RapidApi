
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RapidApi.Authentication;
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
            builder.Services.AddTransient<GenerateKey>(); /*Addtransient sisteme servis ekliyoruz ve generatekey sýnýfýný DI için enjekte etmiþ oluyoruz. ve artýk sistem bunu kendisi örnekleyip kullanýma sunuyor.*/

            builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("RapidDatabase")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}