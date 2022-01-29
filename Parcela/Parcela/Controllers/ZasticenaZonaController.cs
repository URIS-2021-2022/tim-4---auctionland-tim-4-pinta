using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacija za zasticene zone
    /// </summary>
    [ApiController]
    [Route("api/zasticeneZone")]
    [Produces("application/json", "application/xml")]
    public class ZasticenaZonaController : ControllerBase
    {
        private readonly IZasticenaZonaRepository zasticenaZonaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ZasticenaZonaController(IZasticenaZonaRepository zasticenaZonaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zasticenaZonaRepository = zasticenaZonaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraca sve zasticene zone
        /// </summary>
        /// <returns>Lista zasticenih zona</returns>
        /// <response code = "200">Vraca listu zasticenih zona</response>
        /// <response code = "404">Nije pronadjena nijedna zasticena zona</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ZasticenaZonaDto>> GetZasticeneZone()
        {
            List<ZasticenaZonaEntity> zasticeneZone = zasticenaZonaRepository.GetZasticeneZone();
            if (zasticeneZone == null || zasticeneZone.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ZasticenaZonaDto>>(zasticeneZone));
        }

        /// <summary>
        /// Vraca jednu zasticenu zonu na osnovu ID-ja
        /// </summary>
        /// <param name="zasticenaZonaID">ID zasticene zone</param>
        /// <returns>Trazena zasticena zona</returns>
        /// <response code = "200">Vraca trazenu zasticenu zonu</response>
        /// <response code = "404">Trazena zasticena zona nije pronadjena</response>
        [HttpGet("{zasticenaZonaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ZasticenaZonaDto> GetZasticenaZona(Guid zasticenaZonaID)
        {
            ZasticenaZonaEntity zasticenaZona = zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZonaID);
            if (zasticenaZona == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ZasticenaZonaDto>(zasticenaZona));
        }

        /// <summary>
        /// Kreira novu zasticenu zonu
        /// </summary>
        /// <param name="zasticenaZona">Model zasticene zone</param>
        /// <returns>Potvrda o kreiranju zasticene zone</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove zasticene zone \
        /// POST /api/zasticeneZone \
        /// { \
        /// "zasticenaZonaOznaka": 1, \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu zasticenu zonu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja zasticene zone</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZasticenaZonaDto> CreateZasticenaZona([FromBody] ZasticenaZonaDto zasticenaZona)
        {
            try
            {
                ZasticenaZonaEntity zaz = mapper.Map<ZasticenaZonaEntity>(zasticenaZona);
                ZasticenaZonaEntity z = zasticenaZonaRepository.CreateZasticenaZona(zaz);
                string location = linkGenerator.GetPathByAction("GetZasticenaZona", "ZasticenaZona", new { zasticenaZonaID = z.ZasticenaZonaID });
                return Created(location, mapper.Map<ZasticenaZonaDto>(z));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne zasticene zone na osnovu ID-ja
        /// </summary>
        /// <param name="zasticenaZonaID">ID zasticene zone</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Zasticena zona uspesno obrisana</response>
        /// <response code="404">Nije pronadjena zasticena zona za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja zasticene zone</response>
        [HttpDelete("{zasticenaZonaID}")]
        public IActionResult DeleteZasticenaZona(Guid zasticenaZonaID)
        {
            try
            {
                ZasticenaZonaEntity zasticenaZona = zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZonaID);
                if (zasticenaZona == null)
                {
                    return NotFound();
                }
                zasticenaZonaRepository.DeleteZasticenaZona(zasticenaZonaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jednu zasticenu zonu
        /// </summary>
        /// <param name="zasticenaZona">Model zasticene zone koja se azurira</param>
        /// <returns>Potvrda o modifikovanoj zasticenoj zoni</returns>
        /// /// <response code="200">Vraca azuriranu zasticenu zonu</response>
        /// <response code="400">Zasticena zona koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja zasticene zone</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZasticenaZonaDto> UpdateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            try
            {
                if (zasticenaZonaRepository.GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID) == null)
                {
                    return NotFound();
                }
                ZasticenaZonaEntity z = zasticenaZonaRepository.UpdateZasticenaZona(zasticenaZona);
                return Ok(mapper.Map<ZasticenaZonaDto>(z));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa zasticenim zonama 
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetZasticenaZonaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
