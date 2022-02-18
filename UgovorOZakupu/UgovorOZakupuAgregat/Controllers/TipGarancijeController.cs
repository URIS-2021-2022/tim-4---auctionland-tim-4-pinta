using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Data;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;



namespace UgovorOZakupuAgregat.Controllers
{
    [ApiController]
    [Route("api/tipoviGarancije")]
    [Produces("application/json", "application/xml")]
    public class TipGarancijeController : ControllerBase
    {
        private readonly ITipGarancijeRepository tipGarancijeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public TipGarancijeController(ITipGarancijeRepository tipGarancijeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipGarancijeRepository = tipGarancijeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve tipove garancije na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu tipova garancije</returns>
        /// <response code="200">Vraća listu tipova garancije</response>
        /// <response code="404">Nije pronađena ni jedan jedini tip garancije</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<TipGarancijeDto>> GetTipovi()
        {
            List<TipGarancije> tipoviGarancije = tipGarancijeRepository.GetTipovi();
            if (tipoviGarancije == null || tipoviGarancije.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TipGarancijeDto>>(tipoviGarancije));
        }



        /// <summary>
        /// Vraća jedan tip garancije na osnovu ID-ja tipa garancije.
        /// </summary>
        /// <param name="tipId">ID tipa garancije</param>
        /// <returns></returns>
        /// <response code="200">Vraća tražen tip garancije</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{tipId}")]
        public ActionResult<TipGarancijeDto> GetTipGarancije(Guid tipId)
        {
            var tipGarancije = tipGarancijeRepository.GetTipGarancijeById(tipId);
            if (tipGarancije == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipGarancijeDto>(tipGarancije));
        }


        /// <summary>
        /// Kreira novi tip garancije
        /// </summary>
        /// <param name="tipGarancije">Model tipa garancije</param>
        /// <returns>Potvrdu o kreiranom novom tipu garancije</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa garancije \
        /// POST /api/tipoviGarancije \
        /// {     \
        ///     "ZavodniBroj = "1234a", \
        ///     "Datum = "25-01-2021", \
        ///     "DatumDonosenjaDokumenta = "25-01-2021"\
        ///     
        ///}     
        /// </remarks>
        ///  <response code="201">Vraća kreiran tip garancije</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja  tipa garancije</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<TipGarancijeDto> CreateTipGarancije([FromBody] TipGarancijeDto tipGarancije)
        {
            try
            {
                TipGarancije tipGarancijeEntity = mapper.Map<TipGarancije>(tipGarancije);
                TipGarancije tipGarancijeCreate = tipGarancijeRepository.CreateTipGarancije(tipGarancijeEntity);
                tipGarancijeRepository.SaveChanges();
                return Created("", mapper.Map<TipGarancijeDto>(tipGarancijeCreate));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jednog tipa garancije na osnovu ID-ja tipa garancije.
        /// </summary>
        /// <param name="tipGarancijeId">ID tipa garancije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip garancije uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip garancije za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja tipa garancije</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{tipGarancijeId}")]
        public IActionResult DeleteTipGarancije(Guid tipGarancijeId)
        {
            try
            {
                TipGarancije tipGarancijeModel = tipGarancijeRepository.GetTipGarancijeById(tipGarancijeId);
                if (tipGarancijeModel == null)
                {
                    return NotFound();
                }
                tipGarancijeRepository.DeleteTipGarancije(tipGarancijeId);
                tipGarancijeRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednan tip garancije.
        /// </summary>
        /// <param name="tipGarancije">Model tipa garancije koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom tipu garancije.</returns>
        /// <response code="200">Vraća ažuriran tip garancije</response>
        /// <response code="400">Tip garancije koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa garancije</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<TipGarancijeDto> UpdateTipGarancije(TipGarancijeUpdateDto tipGarancije)
        {
            try
            {
                var stariTip = tipGarancijeRepository.GetTipGarancijeById(tipGarancije.TipId);
                if (tipGarancijeRepository.GetTipGarancijeById(tipGarancije.TipId) == null)
                {
                    return NotFound();
                }
                TipGarancije tipGarancijeEntity = mapper.Map<TipGarancije>(tipGarancije);
                mapper.Map(tipGarancijeEntity, stariTip);
                tipGarancijeRepository.SaveChanges();
                return Ok(mapper.Map<TipGarancijeDto>(stariTip));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

        }


        /// <summary>
        /// Vraća opcije za rad sa tipovima garancije
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetTipGarancijeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
