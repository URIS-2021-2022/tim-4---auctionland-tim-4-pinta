using KupacMikroservis.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using KupacMikroservis.Entities;
using Microsoft.EntityFrameworkCore;
using KupacMikroservis.ServiceCalls;

namespace KupacMikroservis
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

            services.AddControllers();

            services.AddScoped<IPrioritetRepository, PrioritetRepository>();
            services.AddScoped<IKupacRepository, KupacRepository>();
            services.AddScoped<IFizickoLiceRepository, FizickoLiceRepository>();
            services.AddScoped<IPravnoLiceRepository, PravnoLiceRepository>();
            services.AddScoped<IOvlascenoLiceRepository, OvlascenoLiceRepository>();
            services.AddScoped<IKontaktOsobaRepository, KontaktOsobaRepository>();
            services.AddScoped<IAdresaService,AdresaService>();
            services.AddScoped<IUplataService, UplataService>();
            services.AddScoped<ServiceCalls.ILogger, Logger>();
            services.AddScoped<IGateway, Gateway>();
            services.AddScoped<IKorisnikSistemaService, KorisnikSistemaService>();

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
                setupAction.SwaggerDoc("KupacMikroservisOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "KupacMikroservis API",
                        Version = "1",
                        Description = "API sluzi za rad sa objektima tipa Kupac",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Stefan Fink",
                            Email = "stefan.fink123@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/")
                    });

               
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

        
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

               
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
            //    services.AddSwaggerGen(c =>
            //   {
            //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "KupacMikroservis", Version = "v1" });
            //   });

            services.AddDbContextPool<KupacContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KupacDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                //  app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KupacMikroservis v1"));
            }
            else 
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Došlo je do greške.");
                    });
                });
            }
            app.UseAuthentication();
          
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/KupacMikroservisOpenApiSpecification/swagger.json", "Kupac Mikroservis API");
                setupAction.RoutePrefix = "swagger";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
