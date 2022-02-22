
using KatastarskaOpstinaAgregat.Data;
using KatastarskaOpstinaAgregat.Entities;
using KatastarskaOpstinaAgregat.ServiceCalls;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
         
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IKatastarskaOpstinaRepository, KatastarskaOpstinaRepository>();
            services.AddScoped<IGatewayService, GatewayService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IKorisnikService, KorisnikService>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("KatastarskaOpstinaOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Katastarska opstina Agregat API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja može da se vrši dodavanje, modifikacija i brisanje opstina, kao i pregled svih kreiranih opstina.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Dajana Jelic",
                            Email = "dajanajelic05@gmail.com",
                            Url = new Uri(Configuration["Uri:Ftn"])
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri(Configuration["Uri:Ftn"])
                        },
                        TermsOfService = new Uri(Configuration["Uri:Ftn"])
                    });
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
            services.AddDbContextPool<KatastarskaOpstinaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KatastarskaOpstinaDB")));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Došlo je do neočekivane greške. Molimo pokušajte kasnije.");
                    });
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {

                
                setupAction.SwaggerEndpoint("/swagger/KatastarskaOpstinaOpenApiSpecification/swagger.json", "Katastarska opstina Agregat API");

                setupAction.RoutePrefix = "swagger"; 
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });
        }
    }
}
