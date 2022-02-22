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
using Licnost.ServiceCalls;
using System.Net;

namespace Licnost.Controllers
{

    [ApiController]
    [Route("api/clanovi")]
    [Produces("application/json", "application/xml")]
    public class ClanKomisijeController : ControllerBase
    {
        private readonly IClanKomisijeRepository clanRepository;
        private readonly IKomisijaRepository komisijaRepository;
        private readonly ILicnostRepository licnostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IGatewayService gatewayService;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public ClanKomisijeController(IClanKomisijeRepository clanRepository, IKomisijaRepository komisijaRepository, ILicnostRepository licnostRepository, IGatewayService gatewayService, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.clanRepository = clanRepository;
            this.komisijaRepository = komisijaRepository;
            this.licnostRepository = licnostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.gatewayService = gatewayService;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Licnost";
        }

        /// <summary>
        /// Vraća sve clanove komisije na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu clanova komisije</returns>
        /// <response code="200">Vraća listu clanova komisije</response>
        /// <response code="404">Nije pronađena ni jedan jedini clan komisije</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<ClanKomisijeDto>> GetClanoviKomisije()
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] != "menadzer"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }


            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih clanova komisije";

            List<ClanKomisije> clanovi = clanRepository.GetClanoviKomisije();
            if (clanovi == null || clanovi.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            List<ClanKomisijeDto> clanoviKomisijeDto = new List<ClanKomisijeDto>();
            foreach (ClanKomisije c in clanovi)
            {
                ClanKomisijeDto clanKomisijeDto = mapper.Map<ClanKomisijeDto>(c);
                clanKomisijeDto.Komisija = mapper.Map<KomisijaDto>(komisijaRepository.GetKomisijaById(c.KomisijaId));
                clanKomisijeDto.Licnost= mapper.Map<LicnostDto>(licnostRepository.GetLicnostById(c.LicnostId));
                clanoviKomisijeDto.Add(clanKomisijeDto);
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(clanoviKomisijeDto);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{clanId}")]
        public ActionResult<ClanKomisijeDto> GetClanKomisije(Guid clanId)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser" && split[1] != "menadzer"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje clana komisije po ID-ju";

            ClanKomisije clan = clanRepository.GetClanKomisijeById(clanId); 
            if (clan == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            ClanKomisijeDto clanDto = mapper.Map<ClanKomisijeDto>(clan);
            clanDto.Licnost = mapper.Map<LicnostDto>(licnostRepository.GetLicnostById(clan.LicnostId));
            logDto.Level = "Info";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ClanKomisijeDto> CreateClanKomisije([FromBody] ClanKomisijeDto clan)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje novog clana komisije";

            try
            {
                ClanKomisije clanEntity = mapper.Map<ClanKomisije>(clan);
                ClanKomisije clanCreate = clanRepository.CreateClanKomisije(clanEntity);
                clanRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetClanKomisije", "ClanKomisije", new { clanId = clanCreate.ClanKomisijeId});
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<ClanKomisijeDto>(clanCreate));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{clanId}")]
        public IActionResult DeleteClanKomisije(Guid clanId)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje clana komisije";

            try
            {
                ClanKomisije clanModel = clanRepository.GetClanKomisijeById(clanId); 
                if (clanModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                clanRepository.DeleteClanKomisije(clanId);
                clanRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<ClanKomisijeDto> UpdateClanKomisije(ClanKomisijeUpdateDto clan)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (token == "" || (split[1] != "administrator" && split[1] != "superuser"))
            {
                return Unauthorized();
            }

            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje clana komisije";

            try
            {
                var stariClan = clanRepository.GetClanKomisijeById(clan.ClanKomisijeId);
                if (stariClan == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                ClanKomisije clanEntity = mapper.Map<ClanKomisije>(clan);
                mapper.Map(clanEntity, stariClan);
                clanRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<ClanKomisijeDto>(clanEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa clanovima komisije";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}


