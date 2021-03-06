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
    // Omogucava dodavanje dodatnih stvari, npr. status kodova
    [ApiController]
    [Route("api/kursevi")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class KursController : ControllerBase
    {
        private readonly IKursRepository kursRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;
        private readonly IKorisnikSistemaService korisnikSistemaService;

        public KursController(IKursRepository kursRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKorisnikSistemaService korisnikSistemaService)
        {
            this.kursRepository = kursRepository;
            this.linkGenerator = linkGenerator;
            this.korisnikSistemaService = korisnikSistemaService;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Uplata";
        }

        /// <summary>
        /// Vraća sve kurseve 
        /// </summary>
        /// <returns>Lista kurseva</returns>
        /// <response code="200">Vraca listu kurseva</response>
        /// <response code="204">Nije pronadjen ni jedan kurs</response>
        /// <response code="401">Nije autorizovan korisnik</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<KursDto>> GetKurs()
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
            logDto.Message = "Vracanje svih kurseva";

            List<KursEntity> kursevi = kursRepository.GetKurs();
            if (kursevi == null || kursevi.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }

            List<KursDto> kursDto = mapper.Map<List<KursDto>>(kursevi);
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(kursDto);
        }

        /// <summary>
        /// Vraća jedan kurs na osnovu ID-ja kurs.
        /// </summary>
        /// <param name="kursID">ID kursa</param>
        /// <returns>Trazen kurs</returns>
        /// <response code="200">Vraća trazen kurs</response>
        /// <response code="404">Nije pronadjen trazen kurs</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        [HttpGet("{kursID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KursDto> GetKurs(Guid kursID)
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
            logDto.Message = "Vracanje kursa po ID-ju";
            KursEntity kurs = kursRepository.GetKursByID(kursID);
            if (kurs == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }

            KursDto kursDto = mapper.Map<KursDto>(kurs);
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(kursDto);

        }
        /// <summary>
        /// Kreira novi kurs.
        /// </summary>
        /// <param name="kurs">Model kursa</param>
        /// <returns>Potvrdu o kreiranom kursu.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog kursa \
        /// POST /api/kursevi \
        /// {     \
        ///     "vrednostKursa": "117.53", \
        ///     "datum": "2020-01-01", \
        ///     "valuta": "EUR", \
        ///     }      \
        /// }
        /// </remarks>
        /// <response code = "201">Vraca kreiran kurs</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja kursa</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KursDto> CreateKurs([FromBody] KursDto kurs)
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
            logDto.Message = "Dodavanje novog kursa";

            try
            {
                KursEntity ku = mapper.Map<KursEntity>(kurs);
                KursEntity k = kursRepository.CreateKurs(ku);
                kursRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKurs", "Kurs", new { kursID = k.KursID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<KursDto>(k));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Ažurira jedan kurs.
        /// </summary>
        /// <param name="kurs">Model kursa koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom kursu.</returns>
        /// <response code="200">Vraca azurirani kurs</response>
        /// <response code="400">Kurs koji se azurira nije pronadjena</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja uplate</response>    
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [HttpHead]
        public ActionResult<KursDto> UpdateKurs(KursDtoUpdate kurs)
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
            logDto.Message = "Modifikacija kursa";

            try
            {
                KursEntity oldKurs = kursRepository.GetKursByID(kurs.KursID);
                if (oldKurs == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                KursEntity kursEntity = mapper.Map<KursEntity>(kurs);

                oldKurs.VrednostKursa = kursEntity.VrednostKursa;
                oldKurs.Datum = kursEntity.Datum;
                oldKurs.Valuta = kursEntity.Valuta;

                kursRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<KursDto>(oldKurs));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Vrši brisanje jednog kursa na osnovu ID-ja.
        /// </summary>
        /// <param name="kursID">ID kursa</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kurs uspešno obrisan</response>
        /// <response code="404">Nije pronađen kurs za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja kursa</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{kursID}")]
        public IActionResult DeleteKurs(Guid kursID)
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
            logDto.Message = "Brisanje kursa";

            try
            {
                KursEntity kurs = kursRepository.GetKursByID(kursID);
                if (kurs == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                kursRepository.DeleteKurs(kursID);
                kursRepository.SaveChanges();
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
        [AllowAnonymous]
        public IActionResult GetKursOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa kursevima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
