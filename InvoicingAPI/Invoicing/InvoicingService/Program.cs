using System;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using InvoicingService.DataAccess;
using InvoicingService.Domain;
using InvoicingService.Filters;
using InvoicingService.RestClients;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WatchDog;

namespace InvoicingService
{
    /// <summary/>
    public class Program
    {
        /// <summary/>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers(opt => opt.Filters.Add(new ExceptionHandlerFilter())); // Add global filters
            
            builder.Services.AddEndpointsApiExplorer();
            
            // Register the Swagger generator, defining 1 or more Swagger documents 
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Invoicing Web API",
                    Description = "ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Will Nilsson",
                        Email = "will@attrib.com.au"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Invoicing Web API repo",
                        Url = new Uri("https://github.com/wnilsson/invoicing-challenge")
                    }
                });

                // Set the comments path for the Swagger 
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddWatchDogServices(opt =>
            {
                opt.IsAutoClear = true;
                opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Quarterly;
                opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("InvoicingDb");
                opt.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.MSSQL;
            });

            builder.Services.AddFluentValidationAutoValidation();
            
            builder.Services.AddMvcCore().AddApiExplorer();
            
            builder.Services.AddDbContext<InvoicingDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("InvoicingDb"));
            });

            // Add functional
            builder.Services.AddScoped<ICompanyProviderRepository, CompanyProviderRepository>();
            builder.Services.AddSingleton<IInvoiceClientFactory, InvoiceClientFactory>();
            
            // Scan controller assembly for auto mapper profiles
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            
            // Build the app and expose web app members
            var app = builder.Build();

            app.UseWatchDogExceptionLogger();

            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoicing Web API V1");
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();

            // Add the admin portal
            app.UseWatchDog(opt =>
            {
                opt.WatchPageUsername = app.Configuration["WatchDogUsername"];
                opt.WatchPagePassword = app.Configuration["WatchDogPassword"];
                opt.Blacklist = "InvoiceHealth";
            });

            // Start the app
            app.Run();
        }
    }
}
