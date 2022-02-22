using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Net;
using UgovorOZakupuAgregat.Data;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;
using UgovorOZakupuAgregat.ServiceCalls;

namespace UgovorOZakupuAgregat.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za rokove dospeća
    /// </summary>
    [ApiController]
    [Route("api/rokoviDospeca")]
    [Produces("application/json", "application/xml")]
    public class RokoviDospecaController : ControllerBase
    {
        private readonly IRokoviDospecaRepository rokoviRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly LogDto logDto;

        public RokoviDospecaController(IRokoviDospecaRepository rokoviRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKorisnikSistemaService korisnikSistemaService)
        {
            this.rokoviRepository = rokoviRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.korisnikSistemaService = korisnikSistemaService;
            logDto = new LogDto();
            logDto.NameOfTheService = "UgovorOZakupu";
        }

        /// <summary>
        /// Vraća sve rokove dospeća na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu rokova dospeća</returns>
        /// <response code="200">Vraća listu rokova dospeća</response>
        /// <response code="404">Nije pronađena ni jedan jedini rok dospeća</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<RokoviDospecaDto>> GetRokovi()
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
            logDto.Message = "Vracanje svih rokova dospeca";

            List<RokoviDospeca> rokovi = rokoviRepository.GetRokovi();
            if (rokovi == null || rokovi.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<RokoviDospecaDto>>(rokovi));
        }

        /// <summary>
        /// Vraća jedan rok dospeća na osnovu ID-ja roka dospeća.
        /// </summary>
        /// <param name="rokId">ID roka dospeća</param>
        /// <returns></returns>
        /// <response code="200">Vraća tražen rok dospeća</response>
        [HttpGet("{rokId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<RokoviDospecaDto> GetRok(Guid rokId)
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
            logDto.Message = "Vracanje roka dospeća po ID-ju";

            RokoviDospeca rok = rokoviRepository.GetRokById(rokId);
            if (rok == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<RokoviDospecaDto>(rok));
        }

        /// <summary>
        /// Kreira novi rok dospeća
        /// </summary>
        /// <param name="rok">Model roka dospeća</param>
        /// <returns>Potvrdu o kreiranom novom roku dospeća</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog roka dospeća \
        /// POST /api/Dokument \
        /// {     \
        ///     "UgovorId =407C6E21-0765-44E9-A34B-B2C387814E55", \
        ///     " RokDospeca=1" \
        /// }
        /// </remarks>
        ///  <response code="201">Vraća kreiran rok dospeća</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja roka dospeća</response>
        [HttpPost]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
       
        public ActionResult<RokoviDospecaDto> CreateRok([FromBody] RokoviDospecaDto rok)
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
            logDto.Message = "Dodavanje novog roka dospeća";
            try
            {
                RokoviDospeca rokDospecaEntity = mapper.Map<RokoviDospeca>(rok);
                RokoviDospeca rokDospecaCreate = rokoviRepository.CreateRok(rokDospecaEntity);
                rokoviRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetRok", "RokoviDospeca", new { rokId = rokDospecaCreate.RokId});
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<RokoviDospecaDto>(rokDospecaCreate));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog roka dospeća na osnovu ID-ja roka dospeća.
        /// </summary>
        /// <param name="rokId">ID roka dospeća</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Rok dospeća uspešno obrisan</response>
        /// <response code="404">Nije pronađen rok dospeća za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja roka dospeća</response>
        [HttpDelete("{rokId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteRok(Guid rokId)
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
            logDto.Message = "Brisanje roka dospeća";
            try
            {
                RokoviDospeca rokModel = rokoviRepository.GetRokById(rokId);
                if (rokModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                rokoviRepository.DeleteRok(rokId);
                rokoviRepository.SaveChanges();
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
        /// Ažurira jednan rok dospeća.
        /// </summary>
        /// <param name="rok">Model roka dospeća koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom roku dospeća.</returns>
        /// <response code="200">Vraća ažuriran rok dospeća</response>
        /// <response code="404">Rok dospeća koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dokumenta</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<RokoviDospecaDto> UpdateRok(RokoviDospecaUpdateDto rok)
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
            logDto.Message = "Ažuriranje roka dospeća";

            try
            {
                var stariRok = rokoviRepository.GetRokById(rok.RokId);
                if (stariRok == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                RokoviDospeca rokEntity = mapper.Map<RokoviDospeca>(rok);
                mapper.Map(rokEntity, stariRok);
                rokoviRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<RokoviDospecaDto>(rokEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa rokovima dospeća
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetRokOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa rokovima dospeca";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}