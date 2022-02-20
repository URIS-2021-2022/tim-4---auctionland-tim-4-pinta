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
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Data;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using UgovorOZakupuAgregat.ServiceCalls;

namespace UgovorOZakupuAgregat
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

            //services.AddSingleton<IUserRepository, UserMockRepository>();
            //services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IUgovorOZakupuRepository, UgovorOZakupuRepository>();
            services.AddScoped<IDokumentRepository, DokumentRepository>();
            services.AddScoped<ITipGarancijeRepository, TipGarancijeRepository>();
            services.AddScoped<IRokoviDospecaRepository, RokoviDospecaRepository>();
            services.AddScoped<ILicnostService, LicnostService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("UgovorOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Ugovor o zakupu API",
                        Version = "1",
                        Description = "Pomocu ovog API-ja ..."
                    });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";//refleksija
                //omogucava da manipulisemo sa putanjom
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);



            });
            //services.AddDbContext<LicnostContext>();
            services.AddDbContextPool<UgovorOZakupuContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UgovorDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/UgovorOpenApiSpecification/swagger.json", "Ugovor o zakupu API");
                setupAction.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
