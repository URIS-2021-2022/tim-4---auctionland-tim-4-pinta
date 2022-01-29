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
    /// Sadrzi CRUD operacije za delove parcela
    /// </summary>
    [ApiController]
    [Route("api/deloviParcela")]
    [Produces("application/json", "application/xml")]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public DeoParceleController(IDeoParceleRepository deoParceleRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve delove parcela
        /// </summary>
        /// <returns>Lista deolova parcela</returns>
        /// <response code = "200">Vraca listu delova parcela</response>
        /// <response code = "404">Nije pronadjen nijedan deo parcele</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DeoParceleDto>> GetDeloviParcela ()
        {
            List<DeoParceleEntity> deloviParcela = deoParceleRepository.GetDeloviParcela();
            if (deloviParcela == null || deloviParcela.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<DeoParceleDto>>(deloviParcela));
        }

        /// <summary>
        /// Vraca jedan deo parcele na osnovu ID-ja
        /// </summary>
        /// <param name="deoParceleID">ID dela parcele</param>
        /// <returns>Trazeni deo parcele</returns>
        /// <response code = "200">Vraca trazeni deo parcele</response>
        /// <response code = "404">Trazeni deo parcele nije pronadjen</response>
        [HttpGet("{deoParceleID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DeoParceleDto> GetDeoParcele(Guid deoParceleID)
        {
            DeoParceleEntity deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
            if (deoParcele == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DeoParceleDto>(deoParcele));
        }

        /// <summary>
        /// Kreira novi deo parcele
        /// </summary>
        /// <param name="deoParcele">Model dela parcele</param>
        /// <returns>Potvrda o kreiranom delu parcele</returns>
        /// /// <remarks>
        /// Primer zahteva za kreiranje novog dela parcele \
        /// POST /api/deloviParcela \
        /// { \
        /// "redniBroj": 1, \
        /// "povrsinaDelaParcele": 1000, \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirani deo parcele</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja dela parcele</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleDto> CreateDeoParcele([FromBody] DeoParceleDto deoParcele)
        {
            try
            {
                DeoParceleEntity deop = mapper.Map<DeoParceleEntity>(deoParcele);
                DeoParceleEntity dp = deoParceleRepository.CreateDeoParcele(deop);
                string location = linkGenerator.GetPathByAction("GetDeoParcele", "DeoParcele", new { deoParceleID = dp.DeoParceleID });
                return Created(location, mapper.Map<DeoParceleDto>(dp));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog dela parcele  na osnovu ID-ja
        /// </summary>
        /// <param name="deoParceleID">Id dela parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Deo parcele uspesno obrisan</response>
        /// <response code="404">Nije pronadjen deo parcele za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja dela parcele</response>
        [HttpDelete("{deoParceleID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDeoParcele(Guid deoParceleID)
        {
            try
            {
                DeoParceleEntity deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
                if (deoParcele == null)
                {
                    return NotFound();
                }
                deoParceleRepository.DeleteDeoParcele(deoParceleID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jedan deo parcele
        /// </summary>
        /// <param name="deoParcele">Model dela parcele koji se azurira</param>
        /// <returns>Potvrda o modifikovanom delu parcele</returns>
        /// <response code="200">Vraca azurirani deo parcele</response>
        /// <response code="400">Deo parcele koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja dela parcele</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleDto> UpdateDeoParcele(DeoParceleEntity deoParcele)
        {
            try
            {
                if (deoParceleRepository.GetDeoParceleById(deoParcele.DeoParceleID) == null)
                {
                    return NotFound();
                }
                DeoParceleEntity dp = deoParceleRepository.UpdateDeoParcele(deoParcele);
                return Ok(mapper.Map<DeoParceleDto>(dp));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa delovima parcela
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetDeoParceleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
