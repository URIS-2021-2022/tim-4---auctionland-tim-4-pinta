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
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="drzavaRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public DrzavaController(IDrzavaRepository drzavaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.drzavaRepository = drzavaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Adresa";
        }

        /// <summary>
        /// Vraca sve drzave
        /// </summary>
        /// <returns>Lista drzava</returns>
        /// <response code = "200">Vraca listu drzava</response>
        /// <response code = "404">Nije pronadjena nijedna drzava</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DrzavaDto>> GetDrzave()
        {
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
        /// <response code = "404">Trazena drzava nije pronadjena</response>
        [HttpGet("{drzavaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DrzavaDto> GetDrzava(Guid drzavaID)
        {
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
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja drzave</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrzavaDto> CreateDrzava([FromBody] DrzavaDto drzava)
        {
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
        /// <response code="404">Nije pronadjena drzava za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja drzave</response>
        [HttpDelete("{drzavaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDrzava(Guid drzavaID)
        {
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
        /// <response code="200">Vraca azuriranu drzavu</response>
        /// <response code="400">Drzava koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja drzave</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrzavaDto> UpdateDrzava(DrzavaEntity drzava)
        {
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

                mapper.Map(drzavaEntity, oldDrzava);

                drzavaRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<DrzavaDto>(drzavaEntity));
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
