using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using Invoicing.Api.Domain;
using Invoicing.Api.Filters;
using Invoicing.Api.Infrastructure;
using Invoicing.Api.RestClients;
using Invoicing.Api.RestClients.Myob;
using Invoicing.Api.RestClients.Xero;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WatchDog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Invoicing.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
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
            });

            builder.Services.AddFluentValidationAutoValidation();
            
            builder.Services.AddMvcCore().AddApiExplorer();
            
            builder.Services.AddDbContext<InvoicingDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });

            // Scan controller assembly for auto mapper profiles
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Add functional
            builder.Services.AddScoped<ICompanyProviderRepository, CompanyProviderRepository>();
            builder.Services.AddScoped<XeroClient, XeroClient>();
            builder.Services.AddScoped<MyobClient, MyobClient>();
            builder.Services.AddScoped(InvoiceClientImplementationFactory);

            // Build the app and expose web app members
            var app = builder.Build();
    
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dataContext = scope.ServiceProvider.GetRequiredService<InvoicingDbContext>();
                    dataContext.Database.Migrate();
                }
            }

            app.UseWatchDogExceptionLogger();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoicing Web API V1");
            });

            // Add the admin portal
            app.UseWatchDog(opt =>
            {
                opt.WatchPageUsername = app.Configuration["WatchDogUsername"];
                opt.WatchPagePassword = app.Configuration["WatchDogPassword"];
                opt.Blacklist = "InvoiceHealth";
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();

            // Start the app
            app.Run();
        }

        private static Func<string, IInvoiceClient> InvoiceClientImplementationFactory(IServiceProvider serviceProvider)
        {
            return GetInvoiceClient;

            IInvoiceClient GetInvoiceClient(string providerCode)
            {
                switch (providerCode)
                {
                    case "XERO":
                        return serviceProvider.GetService(typeof(XeroClient)) as IInvoiceClient;
                    case "MYOB":
                        return serviceProvider.GetService(typeof(MyobClient)) as IInvoiceClient;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
