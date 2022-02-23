using AutoMapper;
using Licnost.Data;
using Licnost.Entities;
using Licnost.Models;
using Licnost.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Net;

namespace Licnost.Controllers
{
    
    [ApiController]
    [Route("api/licnosti")]
    [Produces("application/json", "application/xml")]
    public class LicnostController : ControllerBase
    {
        private readonly ILicnostRepository licnostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IGatewayService gatewayService;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public LicnostController(ILicnostRepository licnostRepository, IGatewayService gatewayService, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.licnostRepository = licnostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.gatewayService = gatewayService;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Licnost";
        }

        /// <summary>
        /// Vraća sve ličnosti na osnovu prosleđenih filtera
        /// </summary>
        /// <param name="licnostIme"> Ime ličnosti</param>
        /// <param name="licnostPrezime"> Prezime ličnosti</param>
        /// <returns>Listu licnosti</returns>
        /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="404">Nije pronađena ni jedna jedina ličnost</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<List<LicnostDto>> GetLicnosti(string licnostIme, string licnostPrezime)
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
            logDto.Message = "Vracanje svih licnosti";

            List<LicnostEntity> licnosti = licnostRepository.GetLicnosti(licnostIme, licnostPrezime);
            if (licnosti == null || licnosti.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<LicnostDto>>(licnosti));
        }



        /// <summary>
        /// Vraća jednu ličnost na osnovu ID-ja ličnosti.
        /// </summary>
        /// <param name="licnostId">ID ličnosti</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu ličnost</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent )]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{licnostId}")]
        public ActionResult<LicnostDto> GetLicnost(Guid licnostId)
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
            logDto.Message = "Vracanje licnosti po ID-ju";

            LicnostEntity licnost = licnostRepository.GetLicnostById(licnostId);
            if (licnost == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<LicnostDto>(licnost));
        }


        /// <summary>
        /// Kreira novu ličnost
        /// </summary>
        /// <param name="licnost">Model ličnosti</param>
        /// <returns>Potvrdu o kreiranoj novoj ličnosti</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove ličnosti \
        /// POST /api/Licnost \
        /// {     \
        ///     "licnostIme": "Marko", \
        ///     "licnostPrezime": "Marković", \
        ///     "funkcija": "Direktor" \
        ///}
        /// </remarks>
        ///  <response code="200">Vraća kreiranu ličnost</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja ličnosti</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<LicnostDto> CreateLicnost([FromBody] LicnostCreateDto licnost)
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
            logDto.Message = "Dodavanje nove licnosti";
            try
            {
                LicnostEntity licnostEntity = mapper.Map<LicnostEntity>(licnost);
                LicnostEntity licnostCreate = licnostRepository.CreateLicnost(licnostEntity);
                licnostRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetLicnost", "Licnost", new { licnostId = licnostCreate.LicnostId });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<LicnostDto>(licnostCreate));
                
            }
            catch
            {

                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jedne ličnosti na osnovu ID-ja ličnosti.
        /// </summary>
        /// <param name="licnostId">ID ličnosti</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ličnost uspešno obrisana</response>
        /// <response code="404">Nije pronađena ličnost za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ličnosti</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{licnostId}")]
        public IActionResult DeleteLicnost(Guid licnostId)
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
            logDto.Message = "Brisanje licnosti";

            try
            {
                LicnostEntity licnostModel = licnostRepository.GetLicnostById(licnostId);
                if (licnostModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                licnostRepository.DeleteLicnost(licnostId);
                licnostRepository.SaveChanges();
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
        /// Ažurira jednu ličnost.
        /// </summary>
        /// <param name="licnost">Model ličnosti koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj ličnosti.</returns>
        /// <response code="200">Vraća ažuriranu ličnost</response>
        /// <response code="400">Ličnost koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ličnosti</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<LicnostDto> UpdateLicnost(LicnostUpdateDto licnost)
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
            logDto.Message = "Modifikovanje licnosti"; 

            try
            {
                var staraLicnost = licnostRepository.GetLicnostById(licnost.LicnostId);
                if (staraLicnost == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                LicnostEntity licnostEntity = mapper.Map<LicnostEntity>(licnost);
                mapper.Map(licnostEntity, staraLicnost);
                licnostRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<LicnostDto>(licnostEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        /// <summary>
        /// Vraća opcije za rad sa ličnostima
        /// </summary>
        /// <returns></returns>
        
        [HttpOptions]
        public IActionResult GetLicnostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa licnostima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}