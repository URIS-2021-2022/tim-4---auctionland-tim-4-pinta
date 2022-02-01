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

        public ObradivostController(IObradivostRepository obradivostRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.obradivostRepository = obradivostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
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
            List<ObradivostEntity> obradivosti = obradivostRepository.GetObradivosti();
            if (obradivosti == null || obradivosti.Count == 0)
            {
                return NoContent();
            }
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
            ObradivostEntity obradivost = obradivostRepository.GetObradivostById(obradivostID);
            if (obradivost == null)
            {
                return NotFound();
            }
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
            try
            {
                ObradivostEntity obr = mapper.Map<ObradivostEntity>(obradivost);
                ObradivostEntity o = obradivostRepository.CreateObradivost(obr);
                string location = linkGenerator.GetPathByAction("GetObradivost", "Obradivost", new { obradivostID = o.ObradivostID });
                return Created(location, mapper.Map<ObradivostDto>(o));
            }
            catch
            {
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
            try
            {
                ObradivostEntity obradivost = obradivostRepository.GetObradivostById(obradivostID);
                if (obradivost == null)
                {
                    return NotFound();
                }
                obradivostRepository.DeleteObradivost(obradivostID);
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
        public ActionResult<ObradivostDto> UpdateObradivost(ObradivostEntity obradivost)
        {
            try
            {
                if (obradivostRepository.GetObradivostById(obradivost.ObradivostID) == null)
                {
                    return NotFound();
                }
                ObradivostEntity o = obradivostRepository.UpdateObradivost(obradivost);
                return Ok(mapper.Map<ObradivostDto>(o));
            }
            catch (Exception)
            {
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
            return Ok();
        }
    }
}
