using AutoMapper;
using Licnost.Data;
using Licnost.Entities;
using Licnost.Models;
using Licnost.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Licnost.Controllers
{
    [ApiController]
    [Route("api/komisije")]
    [Produces("application/json", "application/xml")]
    public class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly ILicnostRepository licnostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IGatewayService gatewayService;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public KomisijaController(IKomisijaRepository komisijaRepository, ILicnostRepository licnostRepository, IGatewayService gatewayService, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService, LinkGenerator linkGenerator, IMapper mapper)
        {
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
        /// Vraća sve komisije na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu komisija</returns>
        /// <response code="200">Vraća listu komisija</response>
        /// <response code="404">Nije pronađena ni jedna jedina komisija</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<List<KomisijaDto>> GetKomisije()
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
            logDto.Message = "Vracanje svih komisija";

            List<Komisija> komisije = komisijaRepository.GetKomisije();
            if (komisije == null || komisije.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
                
            }
            List<KomisijaDto> komisijeDto = new List<KomisijaDto>();
            foreach (Komisija k in komisije)
            {
                KomisijaDto komisijaDto = mapper.Map<KomisijaDto>(k);
                komisijaDto.Licnost = mapper.Map<LicnostDto>(licnostRepository.GetLicnostById(k.LicnostId));
                komisijeDto.Add(komisijaDto);
            }
        
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(komisijeDto);
        }



        /// <summary>
        /// Vraća jednu komisiju na osnovu ID-ja komisije.
        /// </summary>
        /// <param name="komisijaId">ID komisije</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu komisiju</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{komisijaId}")]
        public ActionResult<KomisijaDto> GetKomisija(Guid komisijaId)
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
            logDto.Message = "Vracanje komisije po ID-ju";

            Komisija komisija = komisijaRepository.GetKomisijaById(komisijaId);
            if (komisija == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            KomisijaDto komisijaDto = mapper.Map<KomisijaDto>(komisija);
            komisijaDto.Licnost = mapper.Map<LicnostDto>(licnostRepository.GetLicnostById(komisija.LicnostId));
            logDto.Level = "Info";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(komisijaDto);
        }


        /// <summary>
        /// Kreira novu komisiju
        /// </summary>
        /// <param name="komisija">Model komisije</param>
        /// <returns>Potvrdu o kreiranoj novoj komisiji</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove komisije \
        /// POST /api/Komisija \
        /// {     \
        ///     "licnostId": 1 \
        ///}
        /// </remarks>
        ///  <response code="200">Vraća kreiranu komisiju</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja komisije</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<KomisijaDto> CreateKomisija([FromBody] KomisijaDto komisija)
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
            logDto.Message = "Dodavanje nove komisije";

            try
            {
                Komisija komisijaEntity = mapper.Map<Komisija>(komisija);
                Komisija komisijaCreate = komisijaRepository.CreateKomisija(komisijaEntity);
                komisijaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKomisija", "Komisija", new { komisijaId = komisijaCreate.KomisijaId });
                KomisijaDto komisijaDto = mapper.Map<KomisijaDto>(komisijaCreate);
                komisijaDto.Licnost = mapper.Map<LicnostDto>(licnostRepository.GetLicnostById(komisijaCreate.LicnostId));
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, komisijaDto);
                //return Created("", mapper.Map<KomisijaDto>(komisijaCreate));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jedne komisije na osnovu ID-ja komisije.
        /// </summary>
        /// <param name="komisijaId">ID komisije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Komisija uspešno obrisana</response>
        /// <response code="404">Nije pronađena komisija za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja komisije</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{komisijaId}")]
        public IActionResult DeleteKomisija(Guid komisijaId)
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
            logDto.Message = "Brisanje komisije";

            try
            {
                Komisija komisijaModel = komisijaRepository.GetKomisijaById(komisijaId);
                if (komisijaModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                komisijaRepository.DeleteKomisija(komisijaId);
                komisijaRepository.SaveChanges();
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
        /// Ažurira jednu komisiju.
        /// </summary>
        /// <param name="komisija">Model komisije koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj komisiji.</returns>
        /// <response code="200">Vraća ažuriranu komisiju</response>
        /// <response code="400">Komisija koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja komisije</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<KomisijaDto> UpdateKomisija(KomisijaUpdateDto komisija)
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
            logDto.Message = "Modifikovanje komisije";

            try
            {
                var staraKomisija = komisijaRepository.GetKomisijaById(komisija.KomisijaId);
                if (staraKomisija == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                Komisija komisijaEntity = mapper.Map<Komisija>(komisija);
                mapper.Map(komisijaEntity, staraKomisija);
                komisijaRepository.SaveChanges();
                KomisijaDto komisijaDto = mapper.Map<KomisijaDto>(komisijaEntity);
                komisijaDto.Licnost = mapper.Map<LicnostDto>(licnostRepository.GetLicnostById(komisijaEntity.LicnostId));
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(komisijaDto);
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Vraća opcije za rad sa komisijama
        /// </summary>
        /// <returns></returns>
       
        [HttpOptions]
        public IActionResult GetKomisijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa komisijama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}

