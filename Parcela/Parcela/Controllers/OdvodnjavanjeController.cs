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
    /// Sadrzi CRUD operacije za rad sa odvodnjavanjima
    /// </summary>
    [ApiController]
    [Route("api/odvodnjavanja")]
    [Produces("application/json", "application/xml")]
    public class OdvodnjavanjeController : ControllerBase
    {
        private readonly IOdvodnjavanjeRepository odvodnjavanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OdvodnjavanjeController(IOdvodnjavanjeRepository odvodnjavanjeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.odvodnjavanjeRepository = odvodnjavanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sva odvodnjavanja
        /// </summary>
        /// <returns>Lista odvodnjavanja</returns>
        /// <response code = "200">Vraca listu odvodnjavanja</response>
        /// <response code = "404">Nije prondadjeno nijedno odvodnjavanje</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OdvodnjavanjeDto>> GetOdvodnjavanja()
        {
            List<OdvodnjavanjeEntity> odvodnjavanja = odvodnjavanjeRepository.GetOdvodnjavanja();
            if (odvodnjavanja == null || odvodnjavanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OdvodnjavanjeDto>>(odvodnjavanja));
        }

        /// <summary>
        /// Vraca jedno odvodnjavanje na osnovu ID-ja
        /// </summary>
        /// <param name="odvodnjavanjeID">ID odvodnjavanja</param>
        /// <returns>Trazeno odvodnjavanje</returns>
        /// <response code = "200">Vraca trazeno odvodnjvanje</response>
        /// <response code = "404">Trazeno odvodnjavanje nije prondjeno</response>
        [HttpGet("{odvodnjavanjeID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<OdvodnjavanjeDto> GetOdvodnjavanje(Guid odvodnjavanjeID)
        {
            OdvodnjavanjeEntity odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
            if (odvodnjavanje == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OdvodnjavanjeDto>(odvodnjavanje));
        }

        /// <summary>
        /// Kreira novo odvodnjavanje
        /// </summary>
        /// <param name="odvodnjavanje">Model odvodnjavanja</param>
        /// <returns>Potvrdu o kreiranom odvodnjavanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog odvodnjavanja \
        /// POST /api/odvodnjavanja \
        /// { \
        /// "odvodnjavanjeNaziv": "Odvodnjavanje1", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirano odvodnjavanje</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja odvodnjavanja</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OdvodnjavanjeDto> CreateOdvodnjavanje([FromBody] OdvodnjavanjeDto odvodnjavanje)
        {
            try
            {
                OdvodnjavanjeEntity odv = mapper.Map<OdvodnjavanjeEntity>(odvodnjavanje);
                OdvodnjavanjeEntity o = odvodnjavanjeRepository.CreateOdvodnjavanje(odv);
                string location = linkGenerator.GetPathByAction("GetOdvodnjavanje", "Odvodnjavanje", new { odvodnjavanjeID = o.OdvodnjavanjeID });
                return Created(location, mapper.Map<OdvodnjavanjeDto>(o));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog odvodnajvanja na odnovu ID-ja
        /// </summary>
        /// <param name="odvodnjavanjeID">ID odvodnjavanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Odvodnjavanje uspesno obrisano</response>
        /// <response code="404">Nije pronadjeno odvodnjavanje za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja odvodnjvanja</response>
        [HttpDelete("{odvodnjavanjeID}")]
        public IActionResult DeleteOdvodnjavanje(Guid odvodnjavanjeID)
        {
            try
            {
                OdvodnjavanjeEntity odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
                if (odvodnjavanje == null)
                {
                    return NotFound();
                }
                odvodnjavanjeRepository.DeleteOdvodnjavanje(odvodnjavanjeID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jedno odvodnjavanje
        /// </summary>
        /// <param name="odvodnjavanje">Model odvodnjavanja koje se azurira</param>
        /// <returns>Potvrda o modifikovanom azuriranju</returns>
        /// <response code="200">Vraca azurirano odvodnajvanje</response>
        /// <response code="400">Odvodnjavanje koje se azurira nije pronadjeno</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja odvodnjavanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OdvodnjavanjeDto> UpdateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje)
        {
            try
            {
                if (odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID) == null)
                {
                    return NotFound();
                }
                OdvodnjavanjeEntity o = odvodnjavanjeRepository.UpdateOdvodnjavanje(odvodnjavanje);
                return Ok(mapper.Map<OdvodnjavanjeDto>(o));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa odvodnajvanjima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetOdvodnjavanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
