﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Parcela.Data;
using Parcela.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Parcela
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(setup =>
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters();

            services.AddSingleton<IParcelaRepository, ParcelaRepository>();
            services.AddSingleton<IDeoParceleRepository, DeoParceleRepository>();
            services.AddSingleton<IKlasaRepository, KlasaRepository>();
            services.AddSingleton<IKulturaRepository, KulturaRepository>();
            services.AddSingleton<IOblikSvojineRepository, OblikSvojineRepository>();
            services.AddSingleton<IObradivostRepository, ObradivostRepository>();
            services.AddSingleton<IOdvodnjavanjeRepository, OdvodnjavanjeRepository>();
            services.AddSingleton<IZasticenaZonaRepository, ZasticenaZonaRepository>();
            services.AddSingleton<IUserRepository, UserMockRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ParcelaOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Parcela Agregat API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja može da se vrši dodavanje, modifikacija i brisanje parcela, odnosno njenih delova, kao i pregled svih kreiranih parcela i delova parcela.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Stefan Radulović",
                            Email = "stefanradulovic58@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/")
                    });

                ////Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                //var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                ////Pravimo putanju do XML fajla sa komentarima
                //var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                ////Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                //setupAction.IncludeXmlComments(xmlCommentsPath);
            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parcela", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parcela v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ParcelaOpenApiSpecification/swagger.json", "Parcela Agregat API");
                setupAction.RoutePrefix = ""; 
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}