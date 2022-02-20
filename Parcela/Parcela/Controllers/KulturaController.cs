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
    /// Sadrzi CRUD operacije za kulture
    /// </summary>
    [ApiController]
    [Route("api/kulture")]
    [Produces("application/json", "application/xml")]
    public class KulturaController : ControllerBase
    {
        private readonly IKulturaRepository kulturaRepository;
        private readonly IGatewayService gatewayService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public KulturaController(IKulturaRepository kulturaRepository, IGatewayService gatewayService, ILoggerService loggerService, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kulturaRepository = kulturaRepository;
            this.gatewayService = gatewayService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve kulture
        /// </summary>
        /// <returns>Lista kultura</returns>
        /// <response code = "200">Vraca listu kultura</response>
        /// <response code = "404">Nije pronadjena nijedna kultura</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KulturaDto>> GetKulture()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih kultura";

            List<KulturaEntity> kulture = kulturaRepository.GetKulture();
            if (kulture == null || kulture.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<KulturaDto>>(kulture));
        }

        /// <summary>
        /// Vraca jednu kulturu na osnovu ID-ja
        /// </summary>
        /// <param name="kulturaID">ID kulture</param>
        /// <returns>Trazena kultura</returns>
        /// <response code = "200">Vraca trazenu kulturu</response>
        /// <response code = "404">Trazena kultura nije pronadjena</response>
        [HttpGet("{kulturaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KulturaDto> GetKultura(Guid kulturaID)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje kulture po ID-ju";

            KulturaEntity kultura = kulturaRepository.GetKulturaById(kulturaID);
            if (kultura == null)
            {        
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<KulturaDto>(kultura));
        }

        /// <summary>
        /// Kreira novu kulturu
        /// </summary>
        /// <param name="kultura">Model kulture</param>
        /// <returns>Potvrda o kreiranoj kulturi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove kulture \
        /// POST /api/kulture \
        /// { \
        /// "kulturaNaziv": "Kultura1", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu kulturu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja kulture</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KulturaDto> CreateKultura([FromBody] KulturaDto kultura)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje nove kulture";

            try
            {
                KulturaEntity kul = mapper.Map<KulturaEntity>(kultura);
                KulturaEntity k = kulturaRepository.CreateKultura(kul);
                kulturaRepository.SaveChanges();                    
                string location = linkGenerator.GetPathByAction("GetKultura", "Kultura", new { kulturaID = k.KulturaID });      
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<KulturaDto>(k));
            }
            catch
            { 
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne kulture na osnovu ID-ja
        /// </summary>
        /// <param name="kulturaID">ID kulture</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kultura uspesno obrisana</response>
        /// <response code="404">Nije pronadjena kultura za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja kulture</response>
        [HttpDelete("{kulturaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKultura(Guid kulturaID)
        {
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje kulture";

            try
            {
                KulturaEntity kultura = kulturaRepository.GetKulturaById(kulturaID);
                if (kultura == null)
                {                  
                    logDto.Level = "Warn";  
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                kulturaRepository.DeleteKultura(kulturaID);
                kulturaRepository.SaveChanges();
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
        /// Azurira jednu kulturu
        /// </summary>
        /// <param name="kultura">Model kulture koja se azurira</param>
        /// <returns>Potvrda o modifikovanoj kulturi</returns>
        /// <response code="200">Vraca azuriranu kulturu</response>
        /// <response code="400">Kultura koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja kultura</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KulturaDto> UpdateKultura(KulturaEntity kultura)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje kulture";

            try
            {
                var oldKultura = kulturaRepository.GetKulturaById(kultura.KulturaID);
                if (oldKultura == null)
                {        
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound(); 
                }
                KulturaEntity kulturaEntity = mapper.Map<KulturaEntity>(kultura);

                mapper.Map(kulturaEntity, oldKultura);                

                kulturaRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<KulturaDto>(kulturaEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcija za rad sa kulturama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKulturaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa kulturama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
