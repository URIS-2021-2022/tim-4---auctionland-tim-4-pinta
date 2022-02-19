using Microsoft.AspNetCore.Authorization;
using Licnost.Data;
using Licnost.Entities;
using Licnost.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Licnost.Controllers
{

    [ApiController]
    [Route("api/clanovi")]
    [Produces("application/json", "application/xml")]
    public class ClanKomisijeController : ControllerBase
    {
        private readonly IClanKomisijeRepository clanRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ClanKomisijeController(IClanKomisijeRepository clanRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.clanRepository = clanRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve clanove komisije na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu clanova komisije</returns>
        /// <response code="200">Vraća listu clanova komisije</response>
        /// <response code="404">Nije pronađena ni jedan jedini clan komisije</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<ClanKomisijeDto>> GetClanoviKomisije()
        {
            List<ClanKomisije> clanovi = clanRepository.GetClanoviKomisije();
            if (clanovi == null || clanovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ClanKomisijeDto>>(clanovi));
        }



        /// <summary>
        /// Vraća jednog clana komisije na osnovu ID-ja clana.
        /// </summary>
        /// <param name="clanId">ID clana</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženog clana</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{clanId}")]
        public ActionResult<ClanKomisijeDto> GetClanKomisije(Guid clanId)
        {
            ClanKomisije clan = clanRepository.GetClanKomisijeById(clanId); 
            if (clan == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<ClanKomisijeDto>(clan));
        }


        /// <summary>
        /// Kreira novog člana komisije
        /// </summary>
        /// <param name="clan">Model člana komisije</param>
        /// <returns>Potvrdu o kreiranom novom članu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog člana \
        /// POST /api/ClanKomisije \
        /// {     \
        ///     "LicnostId": 1, \
        ///     "KomisijaId": 1 \
        ///     
        ///}
        /// </remarks>
        ///  <response code="200">Vraća kreiranog člana komisije</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja člana komisije</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ClanKomisijeDto> CreateClanKomisije([FromBody] ClanKomisijeDto clan)
        {
            try
            {
                ClanKomisije clanEntity = mapper.Map<ClanKomisije>(clan);
                ClanKomisije clanCreate = clanRepository.CreateClanKomisije(clanEntity);
                clanRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetClanKomisije", "ClanKomisije", new { clanId = clanCreate.ClanKomisijeId});
                return Created("", mapper.Map<ClanKomisijeDto>(clanCreate));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jednog člana komisije na osnovu ID-ja člana.
        /// </summary>
        /// <param name="clanId">ID člana</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Član uspešno obrisan</response>
        /// <response code="404">Nije pronađen član za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja člana</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{clanId}")]
        public IActionResult DeleteClanKomisije(Guid clanId)
        {
            try
            {
                ClanKomisije clanModel = clanRepository.GetClanKomisijeById(clanId); 
                if (clanModel == null)
                {
                    return NotFound();
                }
                clanRepository.DeleteClanKomisije(clanId);
                clanRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednog člana.
        /// </summary>
        /// <param name="clan">Model člana koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom članu.</returns>
        /// <response code="200">Vraća ažuriranog člana</response>
        /// <response code="400">Član koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja člana</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<ClanKomisijeDto> UpdateClanKomisije(ClanKomisijeUpdateDto clan)
        {
            try
            {
                var stariClan = clanRepository.GetClanKomisijeById(clan.ClanKomisijeId);
                if (clanRepository.GetClanKomisijeById(clan.ClanKomisijeId) == null)
                {
                    return NotFound();
                }
                ClanKomisije clanEntity = mapper.Map<ClanKomisije>(clan);
                mapper.Map(clanEntity, stariClan);
                clanRepository.SaveChanges();
                return Ok(mapper.Map<ClanKomisijeDto>(stariClan));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        /// <summary>
        /// Vraća opcije za rad sa ličnostima
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetClankomisijeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}


