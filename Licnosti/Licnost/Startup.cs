using Licnost.Data;
using Licnost.Entities;
//using Licnost.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
using Microsoft.EntityFrameworkCore.SqlServer;
using Licnost.ServiceCalls;

namespace Licnost
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
          

            services.AddScoped<ILicnostRepository, LicnostRepository>();
            services.AddScoped<IKomisijaRepository, KomisijaRepository>();
            services.AddScoped<IClanKomisijeRepository, ClanKomisijeRepository>();
            services.AddScoped<IGatewayService, GatewayService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IKorisnikSistemaService, KorisnikSistemaService>();
            


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            

            services.AddSwaggerGen(setupAction=>
            {
                setupAction.SwaggerDoc("LicnostOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Licnost API",
                        Version = "1",
                        Description="Pomocu ovog API-ja ..."
                    });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";//refleksija
                //omogucava da manipulisemo sa putanjom
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);

                
                
            });
            
            services.AddDbContextPool<LicnostContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LicnostDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
           // app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/LicnostOpenApiSpecification/swagger.json", "Licnost API");
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
