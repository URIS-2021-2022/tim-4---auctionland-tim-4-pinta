using Korisnik.Data;
using Korisnik.Entities;
using Korisnik.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

namespace Korisnik
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object JwtBearerDefaults { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //setup.ReturnHttpNotAcceptable = true -> Vra�a status 406 (NotAcceptable) ukoliko klijent u Accept header-u zahteva tra�i neki format koji ne podr�avamo (npr. application/xml)
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                //Ovde sa setup.Filters mozemo dodati response tipove za sve kontrolere
             
            }
            ).AddXmlDataContractSerializerFormatters() //Dodajemo podr�ku za XML tako da ukoliko klijent to tra�i u Accept header-u zahteva mo�emo da serializujemo payload u XML u odgovoru.
            .ConfigureApiBehaviorOptions(setupAction => //Deo koji se odnosi na podr�avanje Problem Details for HTTP APIs
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    //Kreiramo problem details objekat
                    ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    //Prosle�ujemo trenutni kontekst i ModelState, ovo prevodi validacione gre�ke iz ModelState-a u RFC format
                    ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState);

                    //Ubacujemo dodatne podatke
                    problemDetails.Detail = "Pogledajte polje errors za detalje.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    //po defaultu se sve vra�a kao status 400 BadRequest, to je ok kada nisu u pitanju validacione gre�ke,
                    //ako jesu ho�emo da koristimo status 422 UnprocessibleEntity
                    //tra�imo info koji status kod da koristimo
                    var actionExecutiongContext = context as ActionExecutingContext;

                    //proveravamo da li postoji neka gre�ka u ModelState-u, a tako�e proveravamo da li su svi prosle�eni parametri dobro parsirani
                    //ako je sve ok parsirano ali postoje gre�ke u validaciji ho�emo da vratimo status 422
                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://google.com"; //ina�e treba da stoji link ka stranici sa detaljima gre�ke
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Do�lo je do gre�ke prilikom validacije.";

                        //sve vra�amo kao UnprocessibleEntity objekat
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    //ukoliko postoji ne�to �to nije moglo da se parsira ho�emo da vra�amo status 400 kao i do sada
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Do�lo je do gre�ke prilikom parsiranja poslatog sadr�aja.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Konfigurisanje Jwt autentifikacije za projekat
            //Registrujemo Jwt autentifikacionu shemu i definisemo sve potrebne Jwt opcije

                        /*
             * Izvor: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#lifetime-and-registration-options
                - Transient objects are always different. The transient OperationId value is different in the IndexModel and in the middleware.
                - Scoped objects are the same for each request but different across each request.
                - Singleton objects are the same for every request.
               Full link: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#overview-of-dependency-injection
             */
            //services.AddSingleton<IExamRegistrationRepository, ExamRegistrationMockRepository>(); //Koristimo mock repozitorijum
            services.AddScoped<IKorisnikRepository, KorisnikRepository>(); //Koristimo konkretni repozitorijum
            services.AddSingleton<IUserRepository, UserMockRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("KorisnikOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Korisnik API",
                        Version = "1",
                       
                        Description = ".",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Marko Markovi�",
                            Email = "marko@mail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/examRegistrationTermsOfService")
                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            //Dodajemo DbContext koji �elimo da koristimo
            services.AddDbContext<KorisnikContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else //Ukoliko se nalazimo u Production modu postavljamo default poruku za gre�ke koje nastaju na servisu
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Do�lo je do neo�ekivane gre�ke. Molimo poku�ajte kasnije.");
                    });
                });
            }

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/KorisnikOpenApiSpecification/swagger.json", "Korisnik API");
                setupAction.RoutePrefix = "swagger"; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
