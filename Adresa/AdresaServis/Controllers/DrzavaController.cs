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
    /// Sadrzi CRUD operacije za drzave
    /// </summary>
    [ApiController]
    [Route("api/drzave")]
    [Produces("application/json", "application/xml")]
    public class DrzavaController : ControllerBase
    {
        private readonly IDrzavaRepository drzavaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="drzavaRepository">DI za drzavu</param>
        /// <param name="linkGenerator">Link generator</param>
        /// <param name="mapper">Maper</param>
        /// <param name="loggerService">DI za logger servis</param>
        public DrzavaController(IDrzavaRepository drzavaRepository, LinkGenerator linkGenerator, IMapper mapper, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService)
        {
            this.drzavaRepository = drzavaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Adresa";
        }

        /// <summary>
        /// Vraca sve drzave
        /// </summary>
        /// <returns>Lista drzava</returns>
        /// <response code = "200">Vraca listu drzava</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije pronadjena nijedna drzava</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DrzavaDto>> GetDrzave()
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
            logDto.Message = "Vracanje svih drzava";

            List<DrzavaEntity> drzave = drzavaRepository.GetDrzave();
            if (drzave == null || drzave.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<DrzavaDto>>(drzave));
        }

        /// <summary>
        /// Vraca jednu drzavu na osnovu ID-ja
        /// </summary>
        /// <param name="drzavaID">ID drzave</param>
        /// <returns>Trazena drzava</returns>
        /// <response code = "200">Vraca trazenu drzavu</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazena drzava nije pronadjena</response>
        [HttpGet("{drzavaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DrzavaDto> GetDrzava(Guid drzavaID)
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
            logDto.Message = "Vracanje drazve po ID-ju";

            DrzavaEntity drzava = drzavaRepository.GetDrzavaById(drzavaID);
            if (drzava == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<DrzavaDto>(drzava));
        }

        /// <summary>
        /// Kreira novu drzavu
        /// </summary>
        /// <param name="drzava">Model drzave</param>
        /// <returns>Potvrdu o kreiranoj drzavi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove drzave \
        /// POST /api/drzave \
        /// { \
        /// "nazivDrzave": "Srbija" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu drzavu</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja drzave</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrzavaDto> CreateDrzava([FromBody] DrzavaCreateDto drzava)
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
            logDto.Message = "Dodavanje nove drzave";

            try
            {
                DrzavaEntity drz = mapper.Map<DrzavaEntity>(drzava);
                DrzavaEntity d = drzavaRepository.CreateDrzava(drz);
                drzavaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetDrzava", "Drzava", new { drzavaID = d.DrzavaID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<DrzavaDto>(d));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne drzave na osnovu ID-ja
        /// </summary>
        /// <param name="drzavaID">ID drzave</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Drzava uspesno obrisana</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjena drzava za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja drzave</response>
        [HttpDelete("{drzavaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDrzava(Guid drzavaID)
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
            logDto.Message = "Brisanje drzave";

            try
            {
                DrzavaEntity drzava = drzavaRepository.GetDrzavaById(drzavaID);
                if (drzava == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                drzavaRepository.DeleteDrzava(drzavaID);
                drzavaRepository.SaveChanges();
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
        /// Azurira jednu drzavu
        /// </summary>
        /// <param name="drzava">Model drzave koja se azurira</param>
        /// <returns>Potvrdu o modifikovanoj drzavi</returns>
        /// <remarks>
        /// Primer zahteva za modifikovanje drzave \
        /// PUT /api/drzave \
        /// { \
        /// "drazvaID": "fd5e46de-290f-4844-a004-4a94ae24f654" \
        /// "nazivDrzave": "Srbija" \
        /// } 
        /// </remarks>
        /// <response code="200">Vraca azuriranu drzavu</response>
        /// <response code="400">Drzava koja se azurira nije pronadjena</response>
        /// <response code = "401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja drzave</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrzavaDto> UpdateDrzava(DrzavaUpdateDto drzava)
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
            logDto.Message = "Modifikovanje drzave";

            try
            {
                var oldDrzava = drzavaRepository.GetDrzavaById(drzava.DrzavaID);
                if (oldDrzava == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                DrzavaEntity drzavaEntity = mapper.Map<DrzavaEntity>(drzava);

                oldDrzava.NazivDrzave = drzavaEntity.NazivDrzave;

                drzavaRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<DrzavaDto>(oldDrzava));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa drzavama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetDrzavaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa drzavama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
