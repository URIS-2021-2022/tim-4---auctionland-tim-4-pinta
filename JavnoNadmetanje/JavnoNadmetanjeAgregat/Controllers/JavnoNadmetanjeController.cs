using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using System.Linq;
using System.Threading.Tasks;
using JavnoNadmetanjeAgregat.ServiceCalls;
using System.Net;
using System.Net.Http;

namespace JavnoNadmetanjeAgregat.Controllers
{
    // kontroler sadrzi skup akcija tj endpointa
    //metoda koja radi sa endpointom je akcija
    //atribut ApiController prosiruje kontroler sa max funkcionalnost
    //sve sto pisemo u endpointu treba da bude na nekoj ruti
    //ControllerBase dobijamo dodatne funkcionalnost koje kasnije olaksavaju manipulaciju sa status kodova sto ovo cini kontrolerom
    /// <summary>
    /// Sadrzi CRUD operacije za javno nadmetanje
    /// </summary>
    [ApiController]
    [Route("api/javnaNadmetanja")]
    [Produces("application/json", "application/xml")]
    public class JavnoNadmetanjeController : ControllerBase

    {
        private readonly IKatastarskaOpstinaService katastarskaOpstinaService;
        private readonly IKupacService kupacService;
        private readonly IAdresaService adresaService;
        private readonly IParcelaService parcelaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;
        private readonly IGatewayService gatewayService;
        private readonly IKorisnikSistemaService korisnikSistemaService;

        private readonly IJavnoNadmetanjeRepository javnoNadmetanjeRepository;
        private readonly ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        //injektovanje zavisnosti- kad se kreira obj kontrolera mora da se prosledi nesto sto implementira interfejs tj confirmation

        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, LinkGenerator linkGenerator, IMapper mapper, IKatastarskaOpstinaService katastarskaOpstinaService, IKupacService kupacService, IParcelaService parcelaService, IAdresaService adresaService, ILoggerService loggerService, IGatewayService gatewayService, IKorisnikSistemaService korisnikSistemaService, ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.katastarskaOpstinaService = katastarskaOpstinaService;
            this.kupacService = kupacService;
            this.parcelaService = parcelaService;
            this.adresaService = adresaService;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.gatewayService = gatewayService;
            this.korisnikSistemaService = korisnikSistemaService;
            this.tipJavnogNadmetanjaRepository = tipJavnogNadmetanjaRepository;
            logDto = new LogDto();
            logDto.NameOfTheService = "JavnoNadmetanje";
        }

        

    /// <summary>
    /// Vraca sva javna nadmetanja na osnovu odredjenih filtera
    /// </summary>
    /// <returns>Lista javnih nadmetanja</returns>
    /// <response code = "200">Vraca listu javnih nadmetanja</response>
    /// <response code = "404">Nije pronadjeno nijedno javno nadmetanje</response>
    [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnoNadmetanje()
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] != "menadzer"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih javnih nadmetanja";

            List<JavnoNadmetanjeEntity> javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanje();
            if (javnoNadmetanje == null || javnoNadmetanje.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }

            List<JavnoNadmetanjeDto> javnoNadmetanjeDto = mapper.Map<List<JavnoNadmetanjeDto>>(javnoNadmetanje);
            foreach (JavnoNadmetanjeDto j in javnoNadmetanjeDto)
            {

                j.KatastarskaOpstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(j.KatastarskaOpstinaID).Result;
                j.Kupac = kupacService.GetKupacByIdAsync(j.KupacID).Result;
                j.Adresa = adresaService.GetAdresaByIdAsync(j.AdresaID).Result;
                j.Parcela = parcelaService.GetParcelaByIdAsync(j.ParcelaID).Result;

            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(javnoNadmetanjeDto);
        }

        /// <summary>
        /// Vraca jedno javno nadmetanje na osnovu ID-ja
        /// </summary>
        /// <param name="javnoNadmetanjeID">ID javno nadmetanje</param>
        /// <returns>Trazeno javnoNadmetanje</returns>
        /// <response code = "200">Vraca trazeno javno nadmetanje</response>
        /// <response code = "404">Trazeno javno nadmetanje nije pronadjena</response>
        [HttpGet("{javnoNadmetanjeID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] != "menadzer"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje javnog nadmetanja po ID-ju";
            JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
            if (javnoNadmetanje == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }

            KatastarskaOpstinaJavnoNadmetanjeDto katastarskaOpstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(javnoNadmetanje.KatastarskaOpstinaID).Result;
            KupacJavnoNadmetanjeDto kupac = kupacService.GetKupacByIdAsync(javnoNadmetanje.KupacID).Result;
            ParcelaJavnoNadmetanjeDto parcela = parcelaService.GetParcelaByIdAsync(javnoNadmetanje.ParcelaID).Result;
            AdresaJavnoNadmetanjeDto adresa = adresaService.GetAdresaByIdAsync(javnoNadmetanje.AdresaID).Result;

            JavnoNadmetanjeDto javnoNadmetanjeDto = mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje);
            javnoNadmetanjeDto.KatastarskaOpstina = katastarskaOpstina;
            javnoNadmetanjeDto.Kupac = kupac;
            javnoNadmetanjeDto.Parcela = parcela;
            javnoNadmetanjeDto.Adresa = adresa;
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(javnoNadmetanjeDto);

        }
        /// <summary>
        /// Kreira novu javno nadmetanje
        /// </summary>
        /// <param name="javnoNadmetanje">Model javno nadmetanje</param>
        /// <returns>Potvrda o kreiranom javnom nadmetanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog javnog nadmetanja \
        /// POST /api/javnoNadmetanje \
        /// { \
        /// "Datum": "27-01-2021", \
        /// "VremePocetka": "27-01-2021", \
        /// "VremeKraja": "29-01-2021", \
        /// "PocetnaCenaPoHektaru": 100, \
        /// "PeriodZakupa": 2, \
        /// "Izuzeto": true, \
        /// "Krug": 1, \
        /// "VisinaDopunePoreza": 10, \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirano javno nadmetanje</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<JavnoNadmetanjeDto> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeCreationDto javnoNadmetanje)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje novog javnog nadmetanja";
            try
            {
                JavnoNadmetanjeEntity obj = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                JavnoNadmetanjeEntity j = javnoNadmetanjeRepository.CreateJavnoNadmetanje(obj);
                javnoNadmetanjeRepository.SaveChanges();
                String location = linkGenerator.GetPathByAction("GetJavnoNadmetanje", "JavnoNadmetanje", new { javnoNadmetanjeID = j.JavnoNadmetanjeID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<JavnoNadmetanjeDto>(j));
               // return Created("", mapper.Map<JavnoNadmetanjeDto>(j));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        /// <summary>
        /// Vrsi brisanje jednog javnog nadmetanja na osnovu ID-ja
        /// </summary>
        /// <param name="javnoNadmetanjeID">ID javnoNadmetanje</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Javno nadmetanje uspesno obrisano</response>
        /// <response code="404">Nije pronadjeno javno nadmetanje za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja javnog nadmetanja</response>
        [HttpDelete("{javnoNadmetanjeID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje javnog nadmetanja";
            try
            {
                JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeID);
                if (javnoNadmetanje == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                javnoNadmetanjeRepository.DeleteJavnoNadmetanje(javnoNadmetanjeID);
                javnoNadmetanjeRepository.SaveChanges();

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azurira jedno javno nadmetanje
        /// </summary>
        /// <param name="javnoNadmetanje">Model javnog nadmetanja koja se azurira</param>
        /// <returns>Potvrdu o modifikovanom javnom nadmetanju</returns>
        /// <response code="200">Vraca azurirano javno nadmetanje</response>
        /// <response code="400">Javno nadmetanje koje se azurira nije pronadjeno</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<JavnoNadmetanjeDto> UpdateJavnoNadmetanje(JavnoNadmetanjeUpdateDto javnoNadmetanje)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje javnog nadmetanja";

            try
            {
                var oldJavnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeID);
                if (oldJavnoNadmetanje == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                JavnoNadmetanjeEntity javnoNadmetanjeEntity = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                mapper.Map(javnoNadmetanjeEntity, oldJavnoNadmetanje); //Update objekta koji treba da sačuvamo u bazi                

                javnoNadmetanjeRepository.SaveChanges(); //Perzistiramo promene

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<JavnoNadmetanjeDto>(oldJavnoNadmetanje));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

        }

        /// <summary>
        /// Vraca opcije za rad sa javnim nadmetanjima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetJavnoNadmetanjeOptions()
        {

            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa javnim nadmetanjima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();

        }
    }
}
