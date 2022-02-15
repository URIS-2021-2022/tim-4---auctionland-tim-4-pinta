using AutoMapper;
using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Entities;
using JavnoNadmetanjeAgregat.Models;
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

        public StatusJavnogNadmetanjaController(IStatusJavnogNadmetanjaRepository statusJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.statusJavnogNadmetanjaRepository = statusJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
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
            List<StatusJavnogNadmetanjaEntity> statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanja();
            if (statusJavnogNadmetanja == null || statusJavnogNadmetanja.Count == 0)
            {
                return NoContent();
            }
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
            StatusJavnogNadmetanjaEntity statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
            if (statusJavnogNadmetanja == null)
            {
                return NotFound();
            }
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
            try
            {
                StatusJavnogNadmetanjaEntity obj = mapper.Map<StatusJavnogNadmetanjaEntity>(statusJavnogNadmetanja);
                StatusJavnogNadmetanjaEntity s = statusJavnogNadmetanjaRepository.CreateStatusJavnogNadmetanja(obj);
                //string location = linkGenerator.GetPathByAction("GetStatusJavnogNadmetanja", "StatusJavnogNadmetanja", new { statusJavnogNadmetanjaID = s.StatusJavnogNadmetanjaID });
                //return Created(location,mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanja))
                //;
                return Created("", mapper.Map<StatusJavnogNadmetanjaDto>(s));
            }
            catch
            {
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
            try
            {
                StatusJavnogNadmetanjaEntity statusJavnogNadmetanja = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanjaID);
                if (statusJavnogNadmetanja == null)
                {
                    return NotFound();
                }
                statusJavnogNadmetanjaRepository.DeleteStatusJavnogNadmetanja(statusJavnogNadmetanjaID);
                return NoContent();
            }
            catch
            {
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
        public ActionResult<StatusJavnogNadmetanjaDto> UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja)
        {
            try
            {
                var oldStatusJavnoNadmetanje = statusJavnogNadmetanjaRepository.GetStatusJavnogNadmetanjaById(statusJavnogNadmetanja.StatusJavnogNadmetanjaID);
                if (oldStatusJavnoNadmetanje == null)
                {
                    return NotFound();
                }
                StatusJavnogNadmetanjaEntity statusJavnogNadmetanjaEntity = mapper.Map<StatusJavnogNadmetanjaEntity>(statusJavnogNadmetanja);
                mapper.Map(statusJavnogNadmetanjaEntity, oldStatusJavnoNadmetanje); //Update objekta koji treba da sačuvamo u bazi                

                statusJavnogNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<StatusJavnogNadmetanjaDto>(statusJavnogNadmetanjaEntity));
            }
            catch (Exception)
            {
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
            return Ok();
        }

    }
}
