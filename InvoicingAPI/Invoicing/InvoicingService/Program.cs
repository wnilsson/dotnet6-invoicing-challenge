using System;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using InvoicingService.Filters;
using InvoicingService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InvoicingService
{
    /// <summary/>
    public class Program
    {
        /// <summary/>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers(x => x.Filters.Add(new ExceptionHandlerFilter())); // Add global filters
            builder.Services.AddEndpointsApiExplorer();
            // Register the Swagger generator, defining 1 or more Swagger documents 
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
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
                x.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddMvcCore().AddApiExplorer();

            // Inject functional 
            builder.Services.AddScoped<IInvoiceHealthService, InvoiceHealthService>();
            // ToDo inject EF and repo

            // Scan these assemblies for auto mapper profiles
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoicing Web API V1");
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            
            app.Run();
        }
    }
}
