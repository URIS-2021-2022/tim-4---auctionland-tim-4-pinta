using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Data;
using UgovorOZakupuAgregat.Models;
using UgovorOZakupuAgregat.Entities;
using Microsoft.AspNetCore.Authorization;

namespace UgovorOZakupuAgregat.Controllers
{
    [ApiController]
    [Route("api/ugovori")]
    [Produces("application/json", "application/xml")]
    public class UgovorOZakupuController : ControllerBase
    {
        private readonly IUgovorOZakupuRepository ugovorRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public UgovorOZakupuController(IUgovorOZakupuRepository ugovorRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ugovorRepository = ugovorRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve ugovore na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu ugovora</returns>
        /// <response code="200">Vraća listu ugovora</response>
        /// <response code="404">Nije pronađena ni jedan jedini ugovor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<UgovorOZakupuDto>> GetUgovori()
        {
             List<UgovorOZakupu> ugovori = ugovorRepository.GetUgovori();
            if (ugovori == null || ugovori.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UgovorOZakupuDto>>(ugovori));
        }



        /// <summary>
        /// Vraća jedan ugovor na osnovu ID-ja ugovora.
        /// </summary>
        /// <param name="ugovorId">ID ugovora</param>
        /// <returns></returns>
        /// <response code="200">Vraća tražen ugovor</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{ugovorId}")]
        public ActionResult<UgovorOZakupuDto> GetUgovor(Guid ugovorId)
        {
            var ugovor = ugovorRepository.GetUgovorById(ugovorId);
            if (ugovor == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UgovorOZakupuDto>(ugovor));
        }


        /// <summary>
        /// Kreira novi ugovor
        /// </summary>
        /// <param name="ugovor">Model ugovora</param>
        /// <returns>Potvrdu o kreiranom novom ugovoru</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove ličnosti \
        /// POST /api/Ugovor \
        /// {     \
        ///     " DokumentId= Guid.Parse("D1209104-7358-4C22-9F4F-415203563A25")", \
        ///     "TipId= Guid.Parse("E1F134E5-25F9-4B00-8B96-A809D11CD33B")", \
        ///     "RokId= Guid.Parse("234D1ADA-07B8-4789-9C87-86B83118FED0")", \
        ///     " ZavodniBroj="11a", \
        ///     " DatumZavodjenja= "24-01-2021", \
        ///     " RokZaVracanjeZemljista= "24-05-2021", \
        ///     "  MestoPotpisivanja="Novi Sad"", \
        ///     "  DatumPotpisa ="25-01-2021", \
        ///}
        ///      
        /// </remarks>
        ///  <response code="201">Vraća kreiran ugovor</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja ugovora</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<UgovorOZakupuDto> CreateUgovor([FromBody] UgovorOZakupuDto ugovor)
        {
            try
            {
                UgovorOZakupu ugovorEntity = mapper.Map<UgovorOZakupu>(ugovor);
                UgovorOZakupu ugovorCreate = ugovorRepository.CreateUgovor(ugovorEntity);
                ugovorRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetLicnost", "Licnost", new { licnostId = licnostCreate.LicnostId });
                // return Created(location, mapper.Map<LicnostDto>(licnostCreate));
                return Created("", mapper.Map<UgovorOZakupuDto>(ugovorCreate));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jednog ugovora na osnovu ID-ja ugovora.
        /// </summary>
        /// <param name="ugovorId">ID ugovora</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ugovor uspešno obrisan</response>
        /// <response code="404">Nije pronađen ugovor za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ugovora</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{ugovorId}")]
        public IActionResult DeleteUgovor(Guid ugovorId)
        {
            try
            {
                UgovorOZakupu ugovorModel = ugovorRepository.GetUgovorById(ugovorId);
                if (ugovorModel == null)
                {
                    return NotFound();
                }
                ugovorRepository.DeleteUgovor(ugovorId);
                ugovorRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednan ugovor.
        /// </summary>
        /// <param name="ugovor">Model ugovora koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom ugovoru.</returns>
        /// <response code="200">Vraća ažuriran ugovor</response>
        /// <response code="400">Ugovor koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ugovora</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<UgovorOZakupuDto> UpdateUgovor(UgovorOZakupuUpdateDto ugovor)
        {
            try
            {
                var stariUgovor = ugovorRepository.GetUgovorById(ugovor.UgovorId);
                if (ugovorRepository.GetUgovorById(ugovor.UgovorId) == null)
                {
                    return NotFound();
                }
                UgovorOZakupu ugovorEntity = mapper.Map<UgovorOZakupu>(ugovor);
                mapper.Map(ugovorEntity, stariUgovor);
                ugovorRepository.SaveChanges();
                return Ok(mapper.Map<UgovorOZakupuDto>(stariUgovor));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
            
        }


        /// <summary>
        /// Vraća opcije za rad sa ugovorima
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetUgovorOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
