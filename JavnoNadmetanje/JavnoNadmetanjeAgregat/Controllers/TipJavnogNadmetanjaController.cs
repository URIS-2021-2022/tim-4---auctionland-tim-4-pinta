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
    /// Sadrzi CRUD operacije za tipove javnog nadmetanja
    /// </summary>
    [ApiController]
    [Route("api/tipoviJavnihNadmetanja")]
    [Produces("application/json", "application/xml")]
    public class TipJavnogNadmetanjaController : ControllerBase
    {
        private readonly ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public TipJavnogNadmetanjaController(ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipJavnogNadmetanjaRepository = tipJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve tipove javnih nadmetanja na osnovu odredjenih filtera
        /// </summary>
        /// <returns> tipova javnih nadmetanja</returns>
        /// <response code = "200">Vraca listu tipova javnih nadmetanja</response>
        /// <response code = "404">Nije pronadjen nijedan tip javnog nadmetanja</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<TipJavnogNadmetanjaDto>> GetTipoveJavnogNadmetanja()
        {
            List<TipJavnogNadmetanjaEntity> tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanja();
            if (tipJavnogNadmetanja == null || tipJavnogNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TipJavnogNadmetanjaDto>>(tipJavnogNadmetanja));
        }

        /// <summary>
        /// Vraca jedan tip javnog nadmetanja na osnovu ID-ja
        /// </summary>
        /// <param name="tipJavnogNadmetanjaID">ID tipa javnog nadmetanja</param>
        /// <returns>Trazeni tip javnog nadmetanja</returns>
        /// <response code = "200">Vraca trazen tip javnog nadmetanje</response>
        /// <response code = "404">Trazeno javno nadmetanje nije pronadjeno</response>
        [HttpGet("{tipJavnogNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TipJavnogNadmetanjaDto> GetTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipJavnogNadmetanjaEntity tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaID);
            if (tipJavnogNadmetanja == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipJavnogNadmetanjaDto>(tipJavnogNadmetanja));
        }

        /// <summary>
        /// Kreira novi tip javnog nadmetanja
        /// </summary>
        /// <param name="tipJavnogNadmetanja">Model tipa javnog nadmetanja</param>
        /// <returns>Potvrda o kreiranom tipu javnog nadmetanja</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa javnog nadmetanja \
        /// POST /api/tipJavnoNadmetanje \
        /// { \
        /// "NazivTipaJavnogNadmetanja": "TipJavnogNadmetanja1", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiran tip javnog nadmetanja</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja tipa javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipJavnogNadmetanjaDto> CreateTipJavnogNadmetanja([FromBody] TipJavnogNadmetanjaDto tipJavnogNadmetanja)
        {
            try
            {
                TipJavnogNadmetanjaEntity obj = mapper.Map<TipJavnogNadmetanjaEntity>(tipJavnogNadmetanja);
                TipJavnogNadmetanjaEntity t = tipJavnogNadmetanjaRepository.CreateTipJavnogNadmetanja(obj);
                //string location = linkGenerator.GetPathByAction("GetTipJavnogNadmetanja", "TipJavnogNadmetanja", new { tipJavnogNadmetanjaID =t.TipJavnogNadmetanjaID });
                //return Created(location, mapper.Map<TipJavnogNadmetanjaDto>(t));
                return Created("", mapper.Map<TipJavnogNadmetanjaDto>(t));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog tipa javnog nadmetanja na osnovu ID-ja
        /// </summary>
        /// <param name="tipJavnogNadmetanjaID">ID tipa javnog nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip javno nadmetanje uspesno obrisano</response>
        /// <response code="404">Nije pronadjen tip javnog nadmetanja za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja tipa javnog nadmetanja</response>
        [HttpDelete("{tipJavnogNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            try
            {
                TipJavnogNadmetanjaEntity tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanjaID);
                if (tipJavnogNadmetanja == null)
                {
                    return NotFound();
                }
                tipJavnogNadmetanjaRepository.DeleteTipJavnogNadmetanja(tipJavnogNadmetanjaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jedan tip javnog nadmetanja
        /// </summary>
        /// <param name="tipJavnogNadmetanja">Model tipa javnog nadmetanja koja se azurira</param>
        /// <returns>Potvrdu o modifikovanom tipu javnog nadmetanja</returns>
        /// <response code="200">Vraca azuriran tip javnog nadmetanja</response>
        /// <response code="400">Tip javno nadmetanje koje se azurira nije pronadjeno</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja tipa javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipJavnogNadmetanjaDto> TipJavnogNadmetanjaObradivost(TipJavnogNadmetanjaEntity tipJavnogNadmetanja)
        {
            try
            {
                var oldTipJavnoNadmetanje = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaById(tipJavnogNadmetanja.TipJavnogNadmetanjaID);
                if (oldTipJavnoNadmetanje== null)
                {
                    return NotFound();
                }
                TipJavnogNadmetanjaEntity tipJavnogNadmetanjaEntity = mapper.Map<TipJavnogNadmetanjaEntity>(tipJavnogNadmetanja);
                mapper.Map(tipJavnogNadmetanjaEntity, oldTipJavnoNadmetanje); //Update objekta koji treba da sačuvamo u bazi                

                tipJavnogNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<TipJavnogNadmetanjaDto>(tipJavnogNadmetanjaEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa tipovima javnih nadmetanja
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetTipJavnogNadmetanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
