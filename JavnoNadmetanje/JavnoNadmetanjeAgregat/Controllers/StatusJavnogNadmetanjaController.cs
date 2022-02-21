using AutoMapper;
using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
using JavnoNadmetanjeAgregat.ServiceCalls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za statuse javnog nadmetanja
    /// </summary>
    [ApiController]
    [Route("api/statusiJavnihNadmetanja")]
    [Produces("application/json", "application/xml")]
    public class StatusJavnogNadmetanjaController : ControllerBase
    {
        private readonly IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;


        public StatusJavnogNadmetanjaController(IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.statusJavnogNadmetanjaRepository = statusJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "StatusJavnogNadmetanja";
        }


        /// <summary>
        /// Vraca sve statuse javnog nadmetanja na osnovu odredjenih filtera
        /// </summary>
        /// <returns>Lista statusa javnih nadmetanja</returns>
        /// <response code = "200">Vraca listu statusa javnih nadmetanja</response>
        /// <response code = "404">Nije pronadjen nijedan status javnog nadmetanja</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StatusJavnogNadmetanjaDto>> GetStatusJavnogNadmetanja()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih statusa javnog nadmetanja";
            List<StatusJavnogNadmetanjaEntity> statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanja();
            if (statusJavnogNadmetanja == null || statusJavnogNadmetanja.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<StatusJavnogNadmetanjaDto>>(statusJavnogNadmetanja));
        }

        /// <summary>
        /// Vraca jedan status javnog nadmetanja na osnovu ID-ja
        /// </summary>
        /// <param name="statusJavnogNadmetanjaID">ID status javno nadmetanje</param>
        /// <returns>Trazeni status javnog nadmetanje</returns>
        /// <response code = "200">Vraca trazen status javnog nadmetanja</response>
        /// <response code = "404">Trazen status javnog nadmetanja nije pronadjen</response>
        [HttpGet("{statusJavnogNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StatusJavnogNadmetanjaDto> GetStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje statusa javnog nadmetanja po ID-ju";
            StatusJavnogNadmetanjaEntity statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
            if (statusJavnogNadmetanja == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanja));
        }

        /// <summary>
        /// Kreira novi status javnog nadmetanja
        /// </summary>
        /// <param name="statusJavnogNadmetanja">Model statusa javnog nadmetanja</param>
        /// <returns>Potvrda o kreiranom statusu javnog nadmetanja</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog statusa javnog nadmetanja \
        /// POST /api/statusJavnoNadmetanje \
        /// { \
        /// "NazivStatusaJavnogNadmetanja": "NazivStatusaJavnogNadmetanja1", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiran status javnog nadmetanja</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja statusa javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusJavnogNadmetanjaDto> CreateStatusJavnogNadmetanja([FromBody] StatusJavnogNadmetanjaDto statusJavnogNadmetanja)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje novog statusa javnog nadmetanja";

            try
            {
                StatusJavnogNadmetanjaEntity obj = mapper.Map<StatusJavnogNadmetanjaEntity>(statusJavnogNadmetanja);
                StatusJavnogNadmetanjaEntity s = statusJavnogNadmetanjaRepository.CreateStatusJavnogNadmetanja(obj);
                statusJavnogNadmetanjaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStatusJavnogNadmetanja", "StatusJavnogNadmetanja", new { statusJavnogNadmetanjaID = s.StatusJavnogNadmetanjaID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location,mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanja))
                ;
               // return Created("", mapper.Map<StatusJavnogNadmetanjaDto>(s));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog statusa javnog nadmetanja na osnovu ID-ja
        /// </summary>
        /// <param name="statusJavnogNadmetanjaID">ID status javnog nadmetanje</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Status javnog nadmetanja uspesno obrisano</response>
        /// <response code="404">Nije pronadjen status javnog nadmetanja za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja statusa javnog nadmetanja</response>
        [HttpDelete("{statusJavnogNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID)
        {
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje statusa javnog nadmetanja";

            try
            {
                StatusJavnogNadmetanjaEntity statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
                if (statusJavnogNadmetanja == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                statusJavnogNadmetanjaRepository.DeleteStatusJavnogNadmetanja(statusJavnogNadmetanjaID);
                statusJavnogNadmetanjaRepository.SaveChanges();
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
        /// Azurira jedan status javnog nadmetanja
        /// </summary>
        /// <param name="statusJavnogNadmetanja">Model statusa javnog nadmetanja koje se azurira</param>
        /// <returns>Potvrdu o modifikovanom statusu javnog nadmetanja</returns>
        /// <response code="200">Vraca azuriran status javnog nadmetanja</response>
        /// <response code="400">Status javnog nadmetanja koje se azurira nije pronadjeno</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja statusa javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusJavnogNadmetanjaDto> UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaUpdateDto statusJavnogNadmetanja)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje statusa javnog nadmetanja";

            try
            {
                var oldStatusJavnoNadmetanje = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanja.StatusJavnogNadmetanjaID);
                if (oldStatusJavnoNadmetanje == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                StatusJavnogNadmetanjaEntity statusJavnogNadmetanjaEntity = mapper.Map<StatusJavnogNadmetanjaEntity>(statusJavnogNadmetanja);
                mapper.Map(statusJavnogNadmetanjaEntity, oldStatusJavnoNadmetanje); //Update objekta koji treba da sačuvamo u bazi                

                statusJavnogNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanjaEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");

            }
        }

            /// <summary>
            /// Vraca opcije za rad sa javnim nadmetanjima
            /// </summary>
            /// <returns></returns>
            [HttpOptions]
        public IActionResult GetStatusJavnogNadmetanjaOptions()
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
