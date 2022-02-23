using AutoMapper;
using Uplata.Data;
using Uplata.Entities;
using Uplata.Models;
using Uplata.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using JavnoNadmetanjeAgregat.Models;
using System.Net;

namespace Uplata.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za uplate
    /// </summary>
    [ApiController]
    [Route("api/uplate")]
    [Produces("application/json", "application/xml")] 
    public class UplataController : ControllerBase 
    {
        private readonly IUplataRepository uplataRepository;
        private readonly IKursRepository kursRepository;
        private readonly IJavnoNadmetanjeService javnoNadmetanjeService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;
        private readonly IKorisnikSistemaService korisnikSistemaService;

        public UplataController(IUplataRepository uplataRepository, IJavnoNadmetanjeService javnoNadmetanjeService, IKursRepository kursRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKorisnikSistemaService korisnikSistemaService)
        {
            this.uplataRepository = uplataRepository;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.korisnikSistemaService=korisnikSistemaService;
            this.kursRepository = kursRepository;
            logDto = new LogDto();
            logDto.NameOfTheService = "Uplata";
        }

        /// <summary>
        /// Vraća sve uplate 
        /// </summary>
        /// <returns>Lista uplati</returns>
        /// <response code = "200">Vraca listu uplata</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije pronadjena nijedna uplata</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UplataDto>> GetUplate()
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
            logDto.Message = "Vracanje svih uplata";

            List<UplataEntity> uplate = uplataRepository.GetUplate();
            if (uplate == null || uplate.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }

            List<UplataDto> uplateDto = new List<UplataDto>();
            foreach (UplataEntity u in uplate)
            {
                UplataDto uplataDto = mapper.Map<UplataDto>(u);
                uplataDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(u.JavnoNadmetanjeID, token).Result;
                uplataDto.Kurs = mapper.Map<KursDto>(kursRepository.GetKursByID(u.KursID));
                uplateDto.Add(uplataDto);
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(uplateDto);
        }

        /// <summary>
        /// Vraća jednu uplatu na osnovu ID-ja uplate.
        /// </summary>
        /// <param name="uplataID">ID uplate</param>
        /// <returns>Trazena parcela</returns>
        /// <response code = "200">Vraca trazenu uplatu</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazena uplata nije pronadjena</response>
        [HttpGet("{uplataID}")] //Dodatak na rutu koja je definisana na nivou kontroler
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UplataDto> GetUplate(Guid uplataID)
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

            UplataEntity uplata = uplataRepository.GetUplataByID(uplataID);
            if (uplata == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }

            UplataDto uplataDto = mapper.Map<UplataDto>(uplata);
            uplataDto.Kurs = mapper.Map<KursDto>(kursRepository.GetKursByID(uplata.KursID));
            uplataDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(uplata.JavnoNadmetanjeID, token).Result;


            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(uplataDto);

        }
        /// <summary>
        /// Kreira novu uplatu.
        /// </summary>
        /// <param name="uplata">Model uplate</param>
        /// <returns>Potvrdu o kreiranoj uplati.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove uplate \
        /// POST /api/uplate \
        /// {     \
        ///     "iznos": "150", \
        ///     "datum": "2020-01-01", \
        ///     "svrhaUplate": "ucesce na licitaciji", \
        ///     "pozivNaBroj": "3121-424324523-444", \
        ///     "javnoNadmetanjeID": "7C7764E0-27A2-4123-9EB4-081C4E9BCDBF", \
        ///     "kursID": ""411C4082-CC5E-4F5F-8946-4086EBCA08D0"", \
        ///     "brojRacuna": "155-5528599695-55", \
        ///     }      \
        /// }
        /// </remarks>
        /// <response code = "201">Vraca kreiranu uplatu</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja uplate</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataDto> CreateUplata([FromBody] UplataCreateDto uplata)
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
            logDto.Message = "Dodavanje nove uplate";

            try
            {
                UplataEntity upl = mapper.Map<UplataEntity>(uplata);
                UplataEntity u = uplataRepository.CreateUplata(upl);
                uplataRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetUplata", "Uplata", new { uplataID = u.UplataID });
                UplataDto uplataDto = mapper.Map<UplataDto>(u);
                uplataDto.Kurs = mapper.Map<KursDto>(kursRepository.GetKursByID(uplata.KursID));
                uplataDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(uplata.JavnoNadmetanjeID, token).Result;

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, uplataDto);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Ažurira jednu uplatu.
        /// </summary>
        /// <param name="uplata">Model uplate koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj uplati.</returns>
        /// <response code="200">Vraca azuriranu uuplatu</response>
        /// <response code="400">Uplata koja se azurira nije pronadjena</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja uplate</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataDto> UpdateUplata(UplataDtoUpdate uplata)
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
            logDto.Message = "Modifikacija uplate";

            try
            {
                UplataEntity oldUplata = uplataRepository.GetUplataByID(uplata.UplataID);
                if (oldUplata == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                UplataEntity uplataEntity = mapper.Map<UplataEntity>(uplata);

                oldUplata.Iznos = uplataEntity.Iznos;
                oldUplata.KursID = uplataEntity.KursID;
                oldUplata.PozivNaBroj = uplataEntity.PozivNaBroj;
                oldUplata.SvrhaUplate = uplataEntity.SvrhaUplate;
                oldUplata.Datum = uplataEntity.Datum;
                oldUplata.BrojRacuna = uplataEntity.BrojRacuna;
                oldUplata.JavnoNadmetanjeID = uplataEntity.JavnoNadmetanjeID;

                uplataRepository.SaveChanges();

                UplataDto uplataDto = mapper.Map<UplataDto>(oldUplata);
                uplataDto.Kurs = mapper.Map<KursDto>(kursRepository.GetKursByID(oldUplata.KursID));
                uplataDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(oldUplata.JavnoNadmetanjeID, token).Result;

                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(uplataDto);
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Vrši brisanje jedne uplate na osnovu ID-ja uplate.
        /// </summary>
        /// <param name="uplataID">ID uplate</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uplata uspesno obrisana</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjena uplata za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja uplate</response>
        [HttpDelete("{uplataID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUplata(Guid uplataID)
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
            logDto.Message = "Brisanje uplate";

            try
            {
                UplataEntity uplata = uplataRepository.GetUplataByID(uplataID);
                if (uplata == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                uplataRepository.DeleteUplata(uplataID);
                uplataRepository.SaveChanges();
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
        /// Vraca opcije za rad sa uplatama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetUplataOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa uplatama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
