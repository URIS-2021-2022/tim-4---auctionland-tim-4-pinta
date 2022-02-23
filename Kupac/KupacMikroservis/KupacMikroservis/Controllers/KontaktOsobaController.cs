using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KupacMikroservis.Data;
using KupacMikroservis.Models;
using AutoMapper;
using KupacMikroservis.ServiceCalls;
using System.Net;

namespace KupacMikroservis.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za kontakt osobe
    /// </summary>
    [ApiController]
    [Route("api/kontaktosoba")]
    [Produces("application/json", "application/xml")]
    public class KontaktOsobaController : ControllerBase
    {
        private readonly IKontaktOsobaRepository koRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly LogDto logDTO;
        private readonly IKorisnikSistemaService korisnikSistemaService;



        public KontaktOsobaController(IKontaktOsobaRepository koRepository, LinkGenerator linkGenerator, IMapper mapper, ILogger logger, IKorisnikSistemaService korisnikSistemaService)
        {
            this.koRepository = koRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            this.logDTO = new LogDto();
            logDTO.NameOfTheService = "KontaktOsoba";
            this.korisnikSistemaService = korisnikSistemaService;
        }

        /// <summary>
        /// Vraca kontakt osobe
        /// </summary>
        /// <returns>Lista kontakt osoba</returns>
        /// <response code = "200">Vraca listu kontakt osoba</response>
        /// <response code = "404">Nije pronadjena nijedna kontakt osoba</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KontaktOsobaDto>> GetKontaktOsobe()
        {
            string token = Request.Headers["token"].ToString();


            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser")
            {
                return Unauthorized();
            }

            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje svih kontakt osoba";

            List<KontaktOsobaEntity> kontaktosobe = koRepository.GetKontaktOsobe();
            if (kontaktosobe == null || kontaktosobe.Count == 0)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NoContent();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<List<KontaktOsobaDto>>(kontaktosobe));
        }

        /// <summary>
        /// Vraca kontakt osobu po ID
        /// </summary>
        /// <param name="KontaktOsobaId">ID kontakt osobe</param>
        /// <returns>Trazena kontakt osoba</returns>
        /// <response code = "200">Vraca trazenu kontakt osobu</response>
        /// <response code = "404">Trazena kontakt osoba nije pronadjena</response>
        [HttpGet("{KontaktOsobaId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KontaktOsobaDto> GetKontaktOsoba(Guid koID)
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser")
            {
                return Unauthorized();
            }

            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje kontakt osobe po ID";

            KontaktOsobaEntity koModel = koRepository.GetKontaktOsoba(koID);
            if (koModel == null)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NotFound();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<List<KontaktOsobaDto>>(koModel));
        }

        /// <summary>
        /// Dodaje novu kontakt osobu
        /// </summary>
        /// <param name="ko">Model kontakt osobe</param>
        /// <returns>Potvrda o kreiranoj kontakt osobi</returns>
        /// <response code = "201">Vraca kreiranu kontakt osobu</response>
        /// <response code = "500">Doslo je do greske</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KontaktOsobaDto> CreateKontaktOsoba([FromBody] KontaktOsobaCreateDto ko)    //confirmation implementirati
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser")
            {
                return Unauthorized();
            }



            logDTO.HttpMethod = "POST";
            logDTO.Message = "Dodavanje nove kontakt osobe";
            try
            {
                KontaktOsobaEntity koe = mapper.Map<KontaktOsobaEntity>(ko);

                KontaktOsobaEntity koCreated = koRepository.CreateKontaktOsoba(koe);

                string location = linkGenerator.GetPathByAction("GetKontaktOsoba", "KontaktOsoba", new { KontaktOsobaId = ko.KontaktOsobaId });

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Created(location, mapper.Map<KontaktOsobaDto>(koCreated));
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        /// <summary>
        ///Vraca kontakt osobu po ID
        /// </summary>
        /// <param name="KontaktOsobaId">ID kontakt osobe</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kontakt osoba uspesno obrisana</response>
        /// <response code="404">Nije pronadjena kontakt osoba</response>
        /// <response code="500">Doslo je do greske</response>
        [HttpDelete("{KontaktOsobaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKontaktOsoba(Guid koID)
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser")
            {
                return Unauthorized();
            }

            logDTO.HttpMethod = "DELETE";
            logDTO.Message = "Brisanje klase";
            try
            {
                KontaktOsobaEntity koModel =koRepository.GetKontaktOsoba(koID);
                if (koModel == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }
                koRepository.DeleteKontaktOsoba(koID);
                logDTO.Level = "Info";
                logger.Log(logDTO);
                return NoContent();
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira kontakt osobu
        /// </summary>
        /// <param name="ko">Model kontakt osobe za azuriranje</param>
        /// <returns>Potvrda o modifikovanoj kontakt osobi</returns>
        /// <response code="200">Vraca azuriranu kontakt osobu</response>
        /// <response code="400">Kontakt osoba nije pronadjena</response>
        /// <response code="500">Doslo je do greske</response>
        [HttpPut("{KontaktOsobaId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KontaktOsobaDto> UpdateKontaktOsoba(KontaktOsobaUpdateDto ko)
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser")
            {
                return Unauthorized();
            }

            logDTO.HttpMethod = "PUT";
            logDTO.Message = "Brisanje kontakt osobe";

            try
            {

                var oldKOsoba = koRepository.GetKontaktOsoba(ko.KontaktOsobaId);
                if (oldKOsoba == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound(); 
                }

                oldKOsoba.KontaktOsobaId = ko.KontaktOsobaId;
                oldKOsoba.Ime = ko.Ime;
                oldKOsoba.Prezime = ko.Prezime;
                oldKOsoba.Telefon = ko.Telefon;
                
                koRepository.SaveChanges();
                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Ok(mapper.Map<KontaktOsobaDto>(ko));
            }
            catch (Exception)
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca HTTP opcije
        /// </summary>
        [HttpOptions]
        public IActionResult GetKontaktOsobaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}