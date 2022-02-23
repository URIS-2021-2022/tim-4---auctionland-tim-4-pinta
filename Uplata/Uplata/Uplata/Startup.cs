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
using Uplata.Data;
using Uplata.Entities;
using Uplata.Helpers;
using Uplata.ServiceCalls;

namespace Uplata
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
            // Definise da ce kontroleri biti pristupna tacka ovog mikroservisu/projektu.
            services.AddControllers(setup =>
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters(); //Dodajemo podršku za XML tako da ukoliko klijent to traži u Accept header-u zahteva možemo da serializujemo payload u XML u odgovoru.

            services.AddScoped<IUplataRepository, UplataRepository>();
            services.AddScoped<IUserRepository, UserMockRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IJavnoNadmetanjeService, JavnoNadmetanjeService>();
            services.AddScoped<IKorisnikSistemaService, KorisnikSistemaService>();
            services.AddScoped<IGatewayService, GatewayService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IKursRepository, KursRepository>();



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
                setupAction.SwaggerDoc("UplataOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Uplata API",
                        Version = "1",
                        //Često treba da dodamo neke dodatne informacije
                        Description = "Pomoću ovog API-ja može da se doda uplata, modifikacija uplate kao i pregled kreiranih uplata.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Andrija Stanojkovic",
                            Email = "andrija11@gmail.com",
                            Url = new Uri(Configuration["Uri:Ftn"])
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri(Configuration["Uri:Ftn"])
                        },
                        TermsOfService = new Uri(Configuration["Uri:Ftn"])
                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContextPool<UplataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UplataDB")));

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Omogucava definisanje ruta za pristup svakom API-u
            app.UseRouting();

            // Trenutno ce to ukazivati da se koristi anonimna autentifikacija, ali je to kasnija podloga za definisanje nase
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/UplataOpenApiSpecification/swagger.json", "Uplata API");
                setupAction.RoutePrefix = "swagger"; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            // Podrazumeva da ce svi endpoint-i koji su dostupni u kontrolerima biti dostupni za pristupanje
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
