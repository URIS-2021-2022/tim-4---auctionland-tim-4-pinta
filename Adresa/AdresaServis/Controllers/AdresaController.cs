using AdresaServis.Data;
using AdresaServis.Entities;
using AdresaServis.Models;
using AdresaServis.ServiceCalls;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdresaServis.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za adrese
    /// </summary>
    [ApiController]
    [Route("api/adrese")]
    [Produces("application/json", "application/xml")]
    public class AdresaController : ControllerBase
    {
        private readonly IAdresaRepository adresaRepository;
        private readonly IDrzavaRepository drzavaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="adresaRepository"></param>
        /// <param name="drzavaRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public AdresaController(IAdresaRepository adresaRepository, IDrzavaRepository drzavaRepository, LinkGenerator linkGenerator, IMapper mapper, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService)
        {
            this.adresaRepository = adresaRepository;
            this.drzavaRepository = drzavaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Adresa";
        }

        /// <summary>
        /// Vraca sve adrese
        /// </summary>
        /// <returns>Lista adresa</returns>
        /// <response code = "200">Vraca listu adresa</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije pronadjena nijedna adresa</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AdresaDto>> GetAdrese()
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
            logDto.Message = "Vracanje svih adresa";

            List<AdresaEntity> adrese = adresaRepository.GetAdrese();
            if (adrese == null || adrese.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            List<AdresaDto> adreseDto = new List<AdresaDto>();
            foreach(AdresaEntity a in adrese)
            {
                AdresaDto adresaDto = mapper.Map<AdresaDto>(a);
                adresaDto.Drzava = mapper.Map<DrzavaDto>(drzavaRepository.GetDrzavaById(a.DrzavaID));
                adreseDto.Add(adresaDto);
            }

            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(adreseDto);
        }

        /// <summary>
        /// Vraca jednu adresu na osnovu ID-ja
        /// </summary>
        /// <param name="adresaID">ID adrese</param>
        /// <returns>Trazena adresa</returns>
        /// <response code = "200">Vraca trazenu adresu</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazena adresa nije pronadjena</response>
        [HttpGet("{adresaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AdresaDto> GetAdresa(Guid adresaID)
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
            logDto.Message = "Vracanje adrese po ID-ju";

            AdresaEntity adresa = adresaRepository.GetAdresaById(adresaID);
            if (adresa == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            AdresaDto adresaDto = mapper.Map<AdresaDto>(adresa);
            adresaDto.Drzava = mapper.Map<DrzavaDto>(drzavaRepository.GetDrzavaById(adresa.DrzavaID));
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(adresaDto);
        }

        /// <summary>
        /// Kreira novu adresu
        /// </summary>
        /// <param name="adresa">Model adrese</param>
        /// <returns>Potvrdu o kreiranoj adresi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove adrese \
        /// POST /api/adrese \
        /// { \
        /// "ulica": "Fruskogorska", \
        /// "broj": "20", \
        /// "mesto": "Beograd", \
        /// "postanskiBroj": "11000", \
        /// "drzavaID": "fd5e46de-290f-4844-a004-4a94ae24f654" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu adresu</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja adrese</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AdresaDto> CreateAdresa([FromBody] AdresaCreateDto adresa)
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
            logDto.Message = "Dodavanje nove adrese";

            try
            {
                AdresaEntity adr = mapper.Map<AdresaEntity>(adresa);
                AdresaEntity a = adresaRepository.CreateAdresa(adr);
                adresaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetAdresa", "Adresa", new { adresaID = a.AdresaID });
                AdresaDto adresaDto = mapper.Map<AdresaDto>(a);
                adresaDto.Drzava = mapper.Map<DrzavaDto>(drzavaRepository.GetDrzavaById(a.DrzavaID));
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, adresaDto);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne adrese na osnovu ID-ja
        /// </summary>
        /// <param name="adresaID">ID adrese</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Adresa uspesno obrisana</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjena adresa za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja adrese</response>
        [HttpDelete("{adresaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAdresa(Guid adresaID)
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
            logDto.Message = "Brisanje adrese";

            try
            {
                AdresaEntity adresa = adresaRepository.GetAdresaById(adresaID);
                if (adresa == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                adresaRepository.DeleteAdresa(adresaID);
                adresaRepository.SaveChanges();
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
        /// Azurira jednu adresu
        /// </summary>
        /// <param name="adresa">Model adrese koja se azurira</param>
        /// <returns>Potvrdu o modifikovanoj adresi</returns>
        /// <remarks>
        /// Primer zahteva za modifikovanje adrese \
        /// PUT /api/adrese \
        /// { \
        /// "adresaID": "723123b1-3ab1-4741-9437-c8a1d6ad20da", \
        /// "ulica": "Fruskogorska", \
        /// "broj": "20", \
        /// "mesto": "Beograd", \
        /// "postanskiBroj": "11000", \
        /// "drzavaID": "fd5e46de-290f-4844-a004-4a94ae24f654" \
        /// } 
        /// </remarks>
        /// <response code="200">Vraca azuriranu adresu</response>
        /// <response code="400">Adresa koja se azurira nije pronadjena</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja adrese</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AdresaDto> UpdateAdresa(AdresaUpdateDto adresa)
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
            logDto.Message = "Modifikovanje adrese";

            try
            {
                var oldAdresa = adresaRepository.GetAdresaById(adresa.AdresaID);
                if (oldAdresa == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                AdresaEntity adresaEntity = mapper.Map<AdresaEntity>(adresa);

                oldAdresa.Ulica = adresaEntity.Ulica;
                oldAdresa.Broj = adresaEntity.Broj;
                oldAdresa.Mesto = adresaEntity.Mesto;
                oldAdresa.PostanskiBroj = adresaEntity.PostanskiBroj;
                oldAdresa.DrzavaID = adresaEntity.DrzavaID;

                adresaRepository.SaveChanges();
                AdresaDto adresaDto = mapper.Map<AdresaDto>(oldAdresa);
                adresaDto.Drzava = mapper.Map<DrzavaDto>(drzavaRepository.GetDrzavaById(oldAdresa.DrzavaID));
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(adresaDto);
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa adresama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetAdresaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa adresama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
