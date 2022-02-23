using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;
using Parcela.ServiceCals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za delove parcela
    /// </summary>
    [ApiController]
    [Route("api/deloviParcela")]
    [Produces("application/json", "application/xml")]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly IParcelaRepository parcelaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public DeoParceleController(IDeoParceleRepository deoParceleRepository, IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.parcelaRepository = parcelaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve delove parcela
        /// </summary>
        /// <returns>Lista deolova parcela</returns>
        /// <response code = "200">Vraca listu delova parcela</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije pronadjen nijedan deo parcele</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DeoParceleDto>> GetDeloviParcela ()
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
            logDto.Message = "Vracanje svih delova parcela";

            List<DeoParceleEntity> deloviParcela = deoParceleRepository.GetDeloviParcela();
            if (deloviParcela == null || deloviParcela.Count == 0)
            {              
                logDto.Level = "Warn";  
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            List<DeoParceleDto> deloviParcelaDto = new List<DeoParceleDto>();
            foreach (DeoParceleEntity dp in deloviParcela)
            {
                DeoParceleDto deoParceleDto = mapper.Map<DeoParceleDto>(dp);
                deoParceleDto.Parcela = mapper.Map<ParcelaDto>(parcelaRepository.GetParcelaById(dp.ParcelaID));
                deloviParcelaDto.Add(deoParceleDto);
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(deloviParcelaDto);
        }

        /// <summary>
        /// Vraca jedan deo parcele na osnovu ID-ja
        /// </summary>
        /// <param name="deoParceleID">ID dela parcele</param>
        /// <returns>Trazeni deo parcele</returns>
        /// <response code = "200">Vraca trazeni deo parcele</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazeni deo parcele nije pronadjen</response>
        [HttpGet("{deoParceleID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DeoParceleDto> GetDeoParcele(Guid deoParceleID)
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
            logDto.Message = "Vracanje dela parcele po ID-ju";

            DeoParceleEntity deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
            if (deoParcele == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            DeoParceleDto deoParceleDto = mapper.Map<DeoParceleDto>(deoParcele);
            deoParceleDto.Parcela = mapper.Map<ParcelaDto>(parcelaRepository.GetParcelaById(deoParcele.ParcelaID));
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(deoParceleDto);
        }

        /// <summary>
        /// Kreira novi deo parcele
        /// </summary>
        /// <param name="deoParcele">Model dela parcele</param>
        /// <returns>Potvrda o kreiranom delu parcele</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dela parcele \
        /// POST /api/deloviParcela \
        /// { \
        /// "redniBroj": 1, \
        /// "povrsinaDelaParcele": 1000, \
        /// "parcelaID": "03b0da3e-2604-4a9a-9e80-ad192727a3e9"
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirani deo parcele</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja dela parcele</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleDto> CreateDeoParcele([FromBody] DeoParceleCreateDto deoParcele)
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
            logDto.Message = "Dodavanje novog dela parcele";

            try
            {
                DeoParceleEntity deop = mapper.Map<DeoParceleEntity>(deoParcele);
                DeoParceleEntity dp = deoParceleRepository.CreateDeoParcele(deop);
                deoParceleRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetDeoParcele", "DeoParcele", new { deoParceleID = dp.DeoParceleID });
                DeoParceleDto deoParceleDto = mapper.Map<DeoParceleDto>(dp);
                deoParceleDto.Parcela = mapper.Map<ParcelaDto>(parcelaRepository.GetParcelaById(dp.ParcelaID));
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, deoParceleDto);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog dela parcele  na osnovu ID-ja
        /// </summary>
        /// <param name="deoParceleID">Id dela parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Deo parcele uspesno obrisan</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjen deo parcele za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja dela parcele</response>
        [HttpDelete("{deoParceleID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDeoParcele(Guid deoParceleID)
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
            logDto.Message = "Brisanje dela parcele";

            try
            {
                DeoParceleEntity deoParcele = deoParceleRepository.GetDeoParceleById(deoParceleID);
                if (deoParcele == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                deoParceleRepository.DeleteDeoParcele(deoParceleID);
                deoParceleRepository.SaveChanges();
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
        /// Azurira jedan deo parcele
        /// </summary>
        /// <param name="deoParcele">Model dela parcele koji se azurira</param>
        /// <returns>Potvrda o modifikovanom delu parcele</returns>
        /// <remarks>
        /// Primer zahteva za modifikovanje dela parcele \
        /// PUT /api/deloviParcela \
        /// { \
        /// "deoParceleID": "1c7ea607-8ddb-493a-87fa-4bf5893e965b"
        /// "redniBroj": 1, \
        /// "povrsinaDelaParcele": 1000, \
        /// "parcelaID": "03b0da3e-2604-4a9a-9e80-ad192727a3e9"
        /// } 
        /// </remarks>
        /// <response code="200">Vraca azurirani deo parcele</response>
        /// <response code="400">Deo parcele koji se azurira nije pronadjen</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja dela parcele</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleDto> UpdateDeoParcele(DeoParceleUpdateDto deoParcele)
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
            logDto.Message = "Modifikovanje dela parcele";

            try
            {
                var oldDeoParcele = deoParceleRepository.GetDeoParceleById(deoParcele.DeoParceleID);
                if (oldDeoParcele == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                DeoParceleEntity deoParceleEntity = mapper.Map<DeoParceleEntity>(deoParcele);

                oldDeoParcele.PovrsinaDelaParcele = deoParceleEntity.PovrsinaDelaParcele;
                oldDeoParcele.RedniBroj = deoParceleEntity.PovrsinaDelaParcele;
                oldDeoParcele.ParcelaID = deoParceleEntity.ParcelaID;

                deoParceleRepository.SaveChanges();
                DeoParceleDto deoParceleDto = mapper.Map<DeoParceleDto>(oldDeoParcele);
                deoParceleDto.Parcela = mapper.Map<ParcelaDto>(parcelaRepository.GetParcelaById(oldDeoParcele.ParcelaID));
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(deoParceleDto);
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa delovima parcela
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetDeoParceleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa delovima parcela";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
