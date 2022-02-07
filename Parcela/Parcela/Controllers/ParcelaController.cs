using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Parcela.Entities;
using Parcela.ServiceCals;

namespace Parcela.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za parcele
    /// </summary>
    [ApiController]
    [Route("api/parcele")]
    [Produces("application/json", "application/xml")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly IKatastarskaOpstinaService katastarskaOpstinaService;
        private readonly IKupacService kupacService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper, IKatastarskaOpstinaService katastarskaOpstinaService, IKupacService kupacService)
        {
            this.parcelaRepository = parcelaRepository;
            this.katastarskaOpstinaService = katastarskaOpstinaService;
            this.kupacService = kupacService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve parcele
        /// </summary>
        /// <returns>Lista parcela</returns>
        /// <response code = "200">Vraca listu parcela</response>
        /// <response code = "404">Nije pronadjena nijedna parcela</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ParcelaDto>> GetParcele()
        {
            List<ParcelaEntity> parcele = parcelaRepository.GetParcele();
            if (parcele == null || parcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ParcelaDto>>(parcele));
        }

        /// <summary>
        /// Vraca jednu parcelu na osnovu ID-ja
        /// </summary>
        /// <param name="parcelaID">ID parcele</param>
        /// <returns>Trazena parcela</returns>
        /// <response code = "200">Vraca trazenu parcelu</response>
        /// <response code = "404">Trazena parcela nije pronadjena</response>
        [HttpGet("{parcelaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ParcelaDto> GetParcela(Guid parcelaID)
        {
            ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);
            if(parcela == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ParcelaDto>(parcela));
        }

        /// <summary>
        /// Kreira novu parcelu
        /// </summary>
        /// <param name="parcela">Model parcele</param>
        /// <returns>Potvrdu o kreiranoj parceli</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove parcele \
        /// POST /api/parcele \
        /// { \
        /// "povrsina": 3000, \
        /// "brojParcele": "333", \
        /// "brojListaNepokretnosti": "333", \
        /// "kulturaStvarnoStanje": "Kukuruz", \
        /// "klasaStvarnoStanje": "Klasa1", \
        /// "obradivostStvarnoStanje": "Obradivost1", \
        /// "zasticenaZonaStvarnoStanje": "ZasticenaZona1", \
        /// "odvodnjavanjeStvarnoStanje": "Odvodnjavanje1" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu parcelu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja parcele</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> CreateParcela([FromBody] ParcelaDto parcela)
        {
            try
            {
                ParcelaEntity par = mapper.Map<ParcelaEntity>(parcela);
                ParcelaEntity p = parcelaRepository.CreateParcela(par);
                //string location = linkGenerator.GetPathByAction("GetParcela", "Parcela", new { parcelaID = p.ParcelaID });
                //return Created(location, mapper.Map<ParcelaDto>(p));
                return Created("", mapper.Map<ParcelaDto>(p));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");            
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne parcele na osnovu ID-ja
        /// </summary>
        /// <param name="parcelaID">ID parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Parcela uspesno obrisana</response>
        /// <response code="404">Nije pronadjena parcela za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja parcele</response>
        [HttpDelete("{parcelaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteParcela(Guid parcelaID)
        {
            try
            {
                ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);               
                if(parcela == null)
                {
                    return NotFound();
                }
                parcelaRepository.DeleteParcela(parcelaID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira jednu parcelu
        /// </summary>
        /// <param name="parcela">Model parcele koja se azurira</param>
        /// <returns>Potvrdu o modifikovanoj parceli</returns>
        /// <response code="200">Vraca azuriranu parcelu</response>
        /// <response code="400">Parcela koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja parcele</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> UpdateParcela(ParcelaEntity parcela)
        {
            try
            {
                if (parcelaRepository.GetParcelaById(parcela.ParcelaID) == null)
                {
                    return NotFound(); 
                }
                ParcelaEntity p = parcelaRepository.UpdateParcela(parcela);
                return Ok(mapper.Map<ParcelaDto>(p));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa parcelama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetParcelaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
