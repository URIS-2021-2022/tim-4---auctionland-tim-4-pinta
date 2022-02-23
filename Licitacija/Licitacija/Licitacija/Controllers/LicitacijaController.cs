using AutoMapper;
using Licitacija.Data;
using Licitacija.Entities;
using Licitacija.Models;
using Licitacija.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using Licitacija.Models;
using System.Net;

namespace Licitacija.Controllers
{
    // Omogucava dodavanje dodatnih stvari, npr. status kodova
    [ApiController]
    [Route("api/licitacije")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly IJavnoNadmetanjeService javnoNadmetanjeService;
        private readonly IKupacService kupacService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly LogDto logDto;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, IJavnoNadmetanjeService javnoNadmetanjeService,IKupacService kupacService, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKorisnikSistemaService korisnikSistemaService)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
            this.kupacService = kupacService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.kupacService = kupacService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Licitacija";
        }

        /// <summary>
        /// Vraća sve licitacije 
        /// </summary>
        /// <returns>Lista licitaciji</returns>
        /// <response code="200">Vraca listu licitaciji</response>
        /// <response code="404">Nije pronađena ni jedna jedina licitacija</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LicitacijaDto>> GetLicitacije()
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
            logDto.Message = "Vracanje svih licitacija";

            List<LicitacijaEntity> licitacije = licitacijaRepository.GetLicitacije();
            if (licitacije == null || licitacije.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
           
            List<LicitacijaDto> licitacijeDto = new List<LicitacijaDto>();
            
            foreach(LicitacijaEntity l in licitacije)
            {
                LicitacijaDto licitacijaDto = mapper.Map<LicitacijaDto>(l);
                licitacijaDto.Kupac = kupacService.GetKupacByIdAsync(l.KupacID, token).Result;
                licitacijaDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(l.JavnoNadmetanjeID, token).Result;
                licitacijeDto.Add(licitacijaDto);
            }

            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<LicitacijaDto>>(licitacije));
        }

        /// <summary>
        /// Vraća jednu licitaciju na osnovu ID-ja licitacije.
        /// </summary>
        /// <param name="licitacijaID">ID licitacije</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu licitaciju</response>
        
        [HttpGet("{licitacijaID}")] //Dodatak na rutu koja je definisana na nivou kontroler
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<LicitacijaDto> GetLicitacija(Guid licitacijaID)
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
            logDto.Message = "Vracanje licitacije po ID-ju";

            LicitacijaEntity licitacija = licitacijaRepository.GetLicitacijaByID(licitacijaID);
            if (licitacija == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }

            

            LicitacijaDto licitacijaDto = mapper.Map<LicitacijaDto>(licitacija);
            licitacijaDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(licitacija.JavnoNadmetanjeID, token).Result;
            licitacijaDto.Kupac = kupacService.GetKupacByIdAsync(licitacija.KupacID, token).Result;   
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(licitacijaDto);
        }

        /// <summary>
        /// Kreira novu licitaciju.
        /// </summary>
        /// <param name="licitacija">Model licitacije</param>
        /// <returns>Potvrdu o kreiranoj licitaciji.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove licitacije \
        /// POST /api/licitacije \
        /// {     \
        /// "broj": 3000, \
        /// "godina": 2020, \
        /// "datum": "2020-01-01", \
        /// "ogranicenje": 200, \
        /// "rok": "2020-01-01", \
        /// "dokFizickog ": ""Dokument2",", \
        /// "dokPravnog": ""Dokument2"," \
        /// "korakCene": 200,, \
        /// "javnoNadmetanjeID": "8D452221-F73E-4E35-BA7C-3FDD0D08BE70", \
        /// "kupacID": "1a411c13-a195-48f7-8dbd-67596c3974c0", \
        /// }
        /// </remarks>
        /// <response code="200">Vraća licitaciju</response>
        /// <response code="500">Došlo je do greške na serveru prilikom dodavanja licitacije</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicitacijaDto> CreateLicitacija([FromBody] LicitacijaCreateDto licitacija)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] != "operater" && split[1] != "prvaKomisija"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje nove licitacije";

            try
            {
                LicitacijaEntity lic = mapper.Map<LicitacijaEntity>(licitacija);
                LicitacijaEntity l = licitacijaRepository.CreateLicitacija(lic);
                licitacijaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetLicitacija", "Licitacija", new { licitacijaID = l.LicitacijaID });
                LicitacijaDto licitacijaDto = mapper.Map<LicitacijaDto>(l);

                licitacijaDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(licitacija.JavnoNadmetanjeID, token).Result;
                licitacijaDto.Kupac = kupacService.GetKupacByIdAsync(licitacija.KupacID, token).Result;

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, licitacijaDto);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Ažurira jednu licitaciju.
        /// </summary>
        /// <param name="licitacija">Model licitacije koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj licitaciji.</returns>
        /// <response code="200">Vraća ažuriranu licitaciju</response>
        /// <response code="400">Licitacija koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja licitacije</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicitacijaDto> UpdateLicitacija(LicitacijaUpdateDto licitacija)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] != "prvaKomisija"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }


            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje licitacije";

            try
            {
                LicitacijaEntity oldLicitacija = licitacijaRepository.GetLicitacijaByID(licitacija.LicitacijaID);
                if (oldLicitacija == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                LicitacijaEntity licitacijaEntity = mapper.Map<LicitacijaEntity>(licitacija);

                oldLicitacija.Broj = licitacijaEntity.Broj;
                oldLicitacija.Datum = licitacijaEntity.Datum;
                oldLicitacija.Ogranicenje = licitacijaEntity.Ogranicenje;
                oldLicitacija.Godina = licitacijaEntity.Godina;
                oldLicitacija.Rok = licitacijaEntity.Rok;
                oldLicitacija.DokFizickog = licitacijaEntity.DokFizickog;
                oldLicitacija.DokPravnog = licitacijaEntity.DokPravnog;
                oldLicitacija.KorakCene = licitacijaEntity.KorakCene;
                oldLicitacija.JavnoNadmetanjeID = licitacijaEntity.JavnoNadmetanjeID;
                oldLicitacija.KupacID = licitacijaEntity.KupacID;

                licitacijaRepository.SaveChanges();

                LicitacijaDto licitacijaDto = mapper.Map<LicitacijaDto>(oldLicitacija);
                licitacijaDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(licitacija.JavnoNadmetanjeID, token).Result;
                licitacijaDto.Kupac = kupacService.GetKupacByIdAsync(licitacija.KupacID, token).Result;


                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(licitacijaDto);
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

        }


        /// <summary>
        /// Vrši brisanje jedne licitacije na osnovu ID-ja licitacije.
        /// </summary>
        /// <param name="licitacijaID">ID licitacije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Licitacija uspešno obrisana</response>
        /// <response code="404">Nije pronađena licitacija za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja licitacije</response>
        [HttpDelete("{licitacijaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLicitacija(Guid licitacijaID)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] !="prvaKomisija"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }


            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje licitacije";

            try
            {
                LicitacijaEntity licitacija = licitacijaRepository.GetLicitacijaByID(licitacijaID);
                if (licitacija == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                licitacijaRepository.DeleteLicitacija(licitacijaID);
                licitacijaRepository.SaveChanges();
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
        /// Vraca opcije za rad sa licitacijama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetLicitacijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa licitacijama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
