using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;
using Parcela.ServiceCals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za klase
    /// </summary>
    [ApiController]
    [Route("api/klase")]
    [Produces("application/json", "application/xml")]
    public class KlasaController : ControllerBase
    {
        private readonly IKlasaRepository klasaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public KlasaController(IKlasaRepository klasaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.klasaRepository = klasaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve klase
        /// </summary>
        /// <returns>Lista klasa</returns>
        /// <response code = "200">Vraca listu klasa</response>
        /// <response code = "404">Nije pronadjena nijedna klasa</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KlasaDto>> GetKlase()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih klasa";

            List<KlasaEntity> klase = klasaRepository.GetKlase();
            if (klase == null || klase.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<KlasaDto>>(klase));
        }

        /// <summary>
        /// Vraca jednu klasu na osnovu ID-ja
        /// </summary>
        /// <param name="klasaID">ID klase</param>
        /// <returns>Trazena klasa</returns>
        /// <response code = "200">Vraca trazenu klasa</response>
        /// <response code = "404">Trazena klasa nije pronadjena</response>
        [HttpGet("{klasaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KlasaDto> GetKlasa(Guid klasaID)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje klase po ID-ju";

            KlasaEntity klasa = klasaRepository.GetKlasaById(klasaID);
            if (klasa == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<KlasaDto>(klasa));
        }

        /// <summary>
        /// Kreira novu klasu
        /// </summary>
        /// <param name="klasa">Model klase</param>
        /// <returns>Potvrda o kreiranoj klasi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove klase \
        /// POST /api/klase \
        /// { \
        /// "klasaOznaka": 1, \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu klasu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja klase</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KlasaDto> CreateKlasa([FromBody] KlasaDto klasa)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje nove klase";

            try
            {
                KlasaEntity kla = mapper.Map<KlasaEntity>(klasa);
                KlasaEntity k = klasaRepository.CreateKlasa(kla);
                klasaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKlasa", "Klasa", new { klasaID = k.KlasaID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<KlasaDto>(klasa));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne klase na osnovu ID-ja
        /// </summary>
        /// <param name="klasaID">ID klase</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Klasa uspesno obrisana</response>
        /// <response code="404">Nije pronadjena klasa za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja klase</response>
        [HttpDelete("{klasaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKlasa(Guid klasaID)
        {
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje klase";

            try
            {
                KlasaEntity klasa = klasaRepository.GetKlasaById(klasaID);
                if (klasa == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                klasaRepository.DeleteKlasa(klasaID);
                klasaRepository.SaveChanges();
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
        /// Azurirana jedna klasa
        /// </summary>
        /// <param name="klasa">model klase koja se azurira</param>
        /// <returns>Potvrda o modifikovanoj klasi</returns>
        /// <response code="200">Vraca azuriranu klasu</response>
        /// <response code="400">Klasa koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja klase</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KlasaDto> UpdateKlasa(KlasaUpdateDto klasa)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje klase";

            try
            {
                var oldKlasa = klasaRepository.GetKlasaById(klasa.KlasaID);
                if (oldKlasa == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                KlasaEntity klasaEntity = mapper.Map<KlasaEntity>(klasa);

                oldKlasa.KlasaOznaka = klasaEntity.KlasaOznaka;

                klasaRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<KlasaDto>(oldKlasa));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa klasama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKlasaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa klasama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
