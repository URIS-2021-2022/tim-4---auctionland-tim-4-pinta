using AdresaServis.Data;
using AdresaServis.Entities;
using AdresaServis.Models;
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
    [ApiController]
    [Route("api/drzave")]
    [Produces("application/json", "application/xml")]
    public class DrzavaController : ControllerBase
    {
        private readonly IDrzavaRepository drzavaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public DrzavaController(IDrzavaRepository drzavaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.drzavaRepository = drzavaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
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
            List<DrzavaEntity> drzave = drzavaRepository.GetDrzave();
            if (drzave == null || drzave.Count == 0)
            {
                return NoContent();
            }
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
            DrzavaEntity drzava = drzavaRepository.GetDrzavaById(drzavaID);
            if (drzava == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DrzavaDto>(drzava));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DrzavaDto> CreateAdresa([FromBody] DrzavaDto drzava)
        {
            try
            {
                DrzavaEntity drz = mapper.Map<DrzavaEntity>(drzava);
                DrzavaEntity d = drzavaRepository.CreateDrzava(drz);
                string location = linkGenerator.GetPathByAction("GetDrzava", "Drzava", new { drzavaID = d.DrzavaID });
                return Created(location, mapper.Map<DrzavaDto>(d));
            }
            catch
            {
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
            try
            {
                DrzavaEntity drzava = drzavaRepository.GetDrzavaById(drzavaID);
                if (drzava == null)
                {
                    return NotFound();
                }
                drzavaRepository.DeleteDrzava(drzavaID);
                return NoContent();
            }
            catch
            {
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
            try
            {
                if (drzavaRepository.GetDrzavaById(drzava.DrzavaID) == null)
                {
                    return NotFound();
                }
                DrzavaEntity d = drzavaRepository.UpdateDrzava(drzava);
                return Ok(mapper.Map<DrzavaDto>(d));
            }
            catch (Exception)
            {
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
            return Ok();
        }
    }
}
