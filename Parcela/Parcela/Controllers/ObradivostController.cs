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
    /// Sadrzi CRUD operacija za obradivosti
    /// </summary>
    [ApiController]
    [Route("api/obradivosti")]
    [Produces("application/json", "application/xml")]
    public class ObradivostController : ControllerBase
    {
        private readonly IObradivostRepository obradivostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public ObradivostController(IObradivostRepository obradivostRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.obradivostRepository = obradivostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve obradivosti
        /// </summary>
        /// <returns>Lista obradivosti</returns>
        /// <response code = "200">Vraca listu obradivosti</response>
        /// <response code = "404">Nije pronadjena nijedna obradivost</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ObradivostDto>> GetObradivosti()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih obradivosti";

            List<ObradivostEntity> obradivosti = obradivostRepository.GetObradivosti();
            if (obradivosti == null || obradivosti.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<ObradivostDto>>(obradivosti));
        }

        /// <summary>
        /// Vraca jednu obradivost na osnovu ID-ja
        /// </summary>
        /// <param name="obradivostID">ID obradivosti</param>
        /// <returns>Trazena obradivost</returns>
        /// <response code = "200">Vraca trazenu obradivost</response>
        /// <response code = "404">Trazena obradivost nije pronadjena</response>
        [HttpGet("{obradivostID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ObradivostDto> GetObradivost(Guid obradivostID)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje obradivosti po ID-ju";

            ObradivostEntity obradivost = obradivostRepository.GetObradivostById(obradivostID);
            if (obradivost == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<ObradivostDto>(obradivost));
        }

        /// <summary>
        /// Kreira novu obradivost
        /// </summary>
        /// <param name="obradivost">Model obradivosti</param>
        /// <returns>Potvrda o kreiranoj obradivosti</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove obradivosti \
        /// POST /api/obradivosti \
        /// { \
        /// "obradivostNaziv": "Obradivost1", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu obradivost</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja obradivosti</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ObradivostDto> CreateObradivost([FromBody] ObradivostDto obradivost)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje nove obradivosti";

            try
            {
                ObradivostEntity obr = mapper.Map<ObradivostEntity>(obradivost);
                ObradivostEntity o = obradivostRepository.CreateObradivost(obr);
                obradivostRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetObradivost", "Obradivost", new { obradivostID = o.ObradivostID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<ObradivostDto>(o));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne obradivosti na osnovu ID-ja
        /// </summary>
        /// <param name="obradivostID">ID obradivosti</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Obradivost uspesno obrisana</response>
        /// <response code="404">Nije pronadjena obradivost za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja obradivosti</response>
        [HttpDelete("{obradivostID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteObradivost(Guid obradivostID)
        {
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje obradivosti";

            try
            {
                ObradivostEntity obradivost = obradivostRepository.GetObradivostById(obradivostID);
                if (obradivost == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                obradivostRepository.DeleteObradivost(obradivostID);
                obradivostRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jednu obradivost
        /// </summary>
        /// <param name="obradivost">Model obradivosti koja se azurira</param>
        /// <returns>Potvrda o modifikovanoj obradivosti</returns>
        /// <response code="200">Vraca azuriranu obradivost</response>
        /// <response code="400">Obradivost koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja obradivosti</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ObradivostDto> UpdateObradivost(ObradivostUpdateDto obradivost)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje obradivosti";

            try
            {
                var oldObradivost = obradivostRepository.GetObradivostById(obradivost.ObradivostID);
                if (oldObradivost == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                ObradivostEntity obradivostEntity = mapper.Map<ObradivostEntity>(obradivost);

                oldObradivost.ObradivostNaziv = obradivostEntity.ObradivostNaziv;

                obradivostRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<ObradivostDto>(oldObradivost));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa obradivostima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetObradivostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa obradivostima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
