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
    /// Sadrzi CRUD operacije za rad sa odvodnjavanjima
    /// </summary>
    [ApiController]
    [Route("api/odvodnjavanja")]
    [Produces("application/json", "application/xml")]
    public class OdvodnjavanjeController : ControllerBase
    {
        private readonly IOdvodnjavanjeRepository odvodnjavanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public OdvodnjavanjeController(IOdvodnjavanjeRepository odvodnjavanjeRepository, LinkGenerator linkGenerator, IMapper mapper, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService)
        {
            this.odvodnjavanjeRepository = odvodnjavanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sva odvodnjavanja
        /// </summary>
        /// <returns>Lista odvodnjavanja</returns>
        /// <response code = "200">Vraca listu odvodnjavanja</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije prondadjeno nijedno odvodnjavanje</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OdvodnjavanjeDto>> GetOdvodnjavanja()
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
            logDto.Message = "Vracanje svih odvodnjavanja";

            List<OdvodnjavanjeEntity> odvodnjavanja = odvodnjavanjeRepository.GetOdvodnjavanja();
            if (odvodnjavanja == null || odvodnjavanja.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<OdvodnjavanjeDto>>(odvodnjavanja));
        }

        /// <summary>
        /// Vraca jedno odvodnjavanje na osnovu ID-ja
        /// </summary>
        /// <param name="odvodnjavanjeID">ID odvodnjavanja</param>
        /// <returns>Trazeno odvodnjavanje</returns>
        /// <response code = "200">Vraca trazeno odvodnjvanje</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazeno odvodnjavanje nije prondjeno</response>
        [HttpGet("{odvodnjavanjeID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<OdvodnjavanjeDto> GetOdvodnjavanje(Guid odvodnjavanjeID)
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
            logDto.Message = "Vracanje odvodnjavanja po ID-ju";

            OdvodnjavanjeEntity odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
            if (odvodnjavanje == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<OdvodnjavanjeDto>(odvodnjavanje));
        }

        /// <summary>
        /// Kreira novo odvodnjavanje
        /// </summary>
        /// <param name="odvodnjavanje">Model odvodnjavanja</param>
        /// <returns>Potvrdu o kreiranom odvodnjavanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog odvodnjavanja \
        /// POST /api/odvodnjavanja \
        /// { \
        /// "odvodnjavanjeNaziv": "Podzemno", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreirano odvodnjavanje</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja odvodnjavanja</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OdvodnjavanjeDto> CreateOdvodnjavanje([FromBody] OdvodnjavanjeCreateDto odvodnjavanje)
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
            logDto.Message = "Dodavanje novog odvodnjavanja";

            try
            {
                OdvodnjavanjeEntity odv = mapper.Map<OdvodnjavanjeEntity>(odvodnjavanje);
                OdvodnjavanjeEntity o = odvodnjavanjeRepository.CreateOdvodnjavanje(odv);
                odvodnjavanjeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetOdvodnjavanje", "Odvodnjavanje", new { odvodnjavanjeID = o.OdvodnjavanjeID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<OdvodnjavanjeDto>(o));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog odvodnajvanja na odnovu ID-ja
        /// </summary>
        /// <param name="odvodnjavanjeID">ID odvodnjavanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Odvodnjavanje uspesno obrisano</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjeno odvodnjavanje za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja odvodnjvanja</response>
        [HttpDelete("{odvodnjavanjeID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOdvodnjavanje(Guid odvodnjavanjeID)
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
            logDto.Message = "Brisanje odvodnjavanja";
            try
            {
                OdvodnjavanjeEntity odvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeID);
                if (odvodnjavanje == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                odvodnjavanjeRepository.DeleteOdvodnjavanje(odvodnjavanjeID);
                odvodnjavanjeRepository.SaveChanges();
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
        /// Azurira jedno odvodnjavanje
        /// </summary>
        /// <param name="odvodnjavanje">Model odvodnjavanja koje se azurira</param>
        /// <returns>Potvrda o modifikovanom azuriranju</returns>
        /// <remarks>
        /// Primer zahteva za modifikovanje odvodnjavanja \
        /// PUT /api/odvodnjavanja \
        /// { \
        /// "odvodnjavanjeID": "32cf50d2-ab1a-45fb-a5de-f6c4fd646775", \
        /// "odvodnjavanjeNaziv": "Podzemno", \
        /// } 
        /// </remarks>
        /// <response code="200">Vraca azurirano odvodnajvanje</response>
        /// <response code="400">Odvodnjavanje koje se azurira nije pronadjeno</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja odvodnjavanja</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OdvodnjavanjeDto> UpdateOdvodnjavanje(OdvodnjavanjeUpdateDto odvodnjavanje)
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
            logDto.Message = "Modifikovanje odvodnjavanja";

            try
            {
                var oldOdvodnjavanje = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID);
                if (oldOdvodnjavanje == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                OdvodnjavanjeEntity odvodnjavanjeEntity = mapper.Map<OdvodnjavanjeEntity>(odvodnjavanje);

                oldOdvodnjavanje.OdvodnjavanjeNaziv = odvodnjavanjeEntity.OdvodnjavanjeNaziv;

                odvodnjavanjeRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<OdvodnjavanjeDto>(oldOdvodnjavanje));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa odvodnajvanjima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetOdvodnjavanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa odvodnjavanjima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
