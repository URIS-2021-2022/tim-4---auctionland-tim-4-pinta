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
    /// Sadzi CRUD operacije za oblike svojine
    /// </summary>
    [ApiController]
    [Route("api/obliciSvojine")]
    [Produces("application/json", "application/xml")]
    public class OblikSvojineController : ControllerBase
    {
        private readonly IOblikSvojineRepository oblikSvojineRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OblikSvojineController(IOblikSvojineRepository oblikSvojineRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.oblikSvojineRepository = oblikSvojineRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve oblike svojine
        /// </summary>
        /// <returns>Lista oblika svojine</returns>
        /// <response code = "200">Vraca listu oblika svojine</response>
        /// <response code = "404">Nije pronadjen nijedan oblik svojine</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OblikSvojineDto>> GetObradivosti()
        {
            List<OblikSvojineEntity> obliciSvojine = oblikSvojineRepository.GetObliciSvojine();
            if (obliciSvojine == null || obliciSvojine.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OblikSvojineDto>>(obliciSvojine));
        }

        /// <summary>
        /// Vraca jedan oblik svojien na osnovu ID-ja
        /// </summary>
        /// <param name="oblikSvojineID">ID oblika svojine</param>
        /// <returns>Trazen oblik svojine</returns>
        /// <response code = "200">Vraca trazen oblik svojine</response>
        /// <response code = "404">Trazen oblik svojine nije pronadjen</response>
        [HttpGet("{oblikSvojineID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<OblikSvojineDto> GetOblikSvojine(Guid oblikSvojineID)
        {
            OblikSvojineEntity oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
            if (oblikSvojine == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ObradivostDto>(oblikSvojine));
        }

        /// <summary>
        /// Kreira novi oblik svojine
        /// </summary>
        /// <param name="oblikSvojine">oblik svojine</param>
        /// <returns>Potvrda o kreiranom obliku svojine</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog oblika svojine \
        /// POST /api/obliciSvojine \
        /// { \
        /// "oblikSvoijenNazvi": "Oblik svojine 1", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiran oblik svojine</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja oblika svojine</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OblikSvojineDto> CreateOblikSvojine([FromBody] OblikSvojineDto oblikSvojine)
        {
            try
            {
                OblikSvojineEntity obl = mapper.Map<OblikSvojineEntity>(oblikSvojine);
                OblikSvojineEntity os = oblikSvojineRepository.CreateOblikSvojine(obl);
                string location = linkGenerator.GetPathByAction("GetOblikSvojine", "OblikSvojine", new { oblikSvojineID = os.OblikSvojineID });
                return Created(location, mapper.Map<OblikSvojineDto>(os));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog oblika svojine na osnovu ID-ja
        /// </summary>
        /// <param name="oblikSvojineID">ID oblika svojine</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Oblik svojine uspesno obrisan</response>
        /// <response code="404">Nije pronadjen oblik svojine za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja oblika svojine</response>
        [HttpDelete("{oblikSvoijneID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOblikSvojine(Guid oblikSvojineID)
        {
            try
            {
                OblikSvojineEntity oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
                if (oblikSvojine == null)
                {
                    return NotFound();
                }
                oblikSvojineRepository.DeleteOblikSvojine(oblikSvojineID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jedan oblik svojine
        /// </summary>
        /// <param name="oblikSvojine">Model oblika svojine za azuriranje</param>
        /// <returns>Potvrda o modifikovanom obliku svojine</returns>
        /// <response code="200">Vraca azuriran oblik svojine</response>
        /// <response code="400">Oblik svojine koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja oblika svojine</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OblikSvojineDto> UpdateOblikSvojine(OblikSvojineEntity oblikSvojine)
        {
            try
            {
                if (oblikSvojineRepository.GetOblikSvojineById(oblikSvojine.OblikSvojineID) == null)
                {
                    return NotFound();
                }
                OblikSvojineEntity os = oblikSvojineRepository.UpdateOblikSvojine(oblikSvojine);
                return Ok(mapper.Map<OblikSvojineDto>(os));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa oblicima svojine
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetOblikSvojineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
