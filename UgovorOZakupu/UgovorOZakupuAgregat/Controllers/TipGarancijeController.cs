using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Data;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;
using UgovorOZakupuAgregat.ServiceCalls;

namespace UgovorOZakupuAgregat.Controllers
{
    /// <summary>
    /// Sadrži CRUD operacije za tipove garancije
    /// </summary>
    [ApiController]
    [Route("api/tipoviGarancije")]
    [Produces("application/json", "application/xml")]
    public class TipGarancijeController : ControllerBase
    {
        private readonly ITipGarancijeRepository tipGarancijeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly LogDto logDto;

        public TipGarancijeController(ITipGarancijeRepository tipGarancijeRepository, IKorisnikSistemaService korisnikSistemaService, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.tipGarancijeRepository = tipGarancijeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.korisnikSistemaService = korisnikSistemaService;
            logDto = new LogDto();
            logDto.NameOfTheService = "UgovorOZakupu";
        }

        /// <summary>
        /// Vraća sve tipove garancije na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu tipova garancije</returns>
        /// <response code="200">Vraća listu tipova garancije</response>
        /// <response code="404">Nije pronađena ni jedan jedini tip garancije</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<TipGarancijeDto>> GetTipovi()
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
            logDto.Message = "Vracanje svih tipova garancije";

            List<TipGarancije> tipoviGarancije = tipGarancijeRepository.GetTipovi();
            if (tipoviGarancije == null || tipoviGarancije.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<TipGarancijeDto>>(tipoviGarancije));
        }



        /// <summary>
        /// Vraća jedan tip garancije na osnovu ID-ja tipa garancije.
        /// </summary>
        /// <param name="tipId">ID tipa garancije</param>
        /// <returns></returns>
        /// <response code="200">Vraća tražen tip garancije</response>
        [HttpGet("{tipId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipGarancijeDto> GetTipGarancije(Guid tipId)
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
            logDto.Message = "Vracanje tipa garancije po ID-ju";

            TipGarancije tipGarancije = tipGarancijeRepository.GetTipGarancijeById(tipId);
            if (tipGarancije == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
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
        ///      Naziv= "Jemstvo" \
        /// }     
        /// </remarks>
        ///  <response code="201">Vraća kreiran tip garancije</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja  tipa garancije</response>
        [HttpPost]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public ActionResult<TipGarancijeDto> CreateTipGarancije([FromBody] TipGarancijeDto tipGarancije)
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
            logDto.Message = "Dodavanje novog tipa garancije";

            try
            {
                TipGarancije tipGarancijeEntity = mapper.Map<TipGarancije>(tipGarancije);
                TipGarancije tipGarancijeCreate = tipGarancijeRepository.CreateTipGarancije(tipGarancijeEntity);
                tipGarancijeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetTipGarancije", "TipGarancije", new { tipId = tipGarancijeCreate.TipId});
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<TipGarancijeDto>(tipGarancijeCreate));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
        [HttpDelete("{tipGarancijeId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteTipGarancije(Guid tipGarancijeId)
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
            logDto.Message = "Brisanje tipa garancije";

            try
            {
                TipGarancije tipGarancijeModel = tipGarancijeRepository.GetTipGarancijeById(tipGarancijeId);
                if (tipGarancijeModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                tipGarancijeRepository.DeleteTipGarancije(tipGarancijeId);
                tipGarancijeRepository.SaveChanges();
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
        /// Ažurira jednan tip garancije.
        /// </summary>
        /// <param name="tipGarancije">Model tipa garancije koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom tipu garancije.</returns>
        /// <response code="200">Vraća ažuriran tip garancije</response>
        /// <response code="400">Tip garancije koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa garancije</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipGarancijeDto> UpdateTipGarancije(TipGarancijeUpdateDto tipGarancije)
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
            logDto.Message = "Ažuriranje tipa garancije";

            try
            {
                var stariTip = tipGarancijeRepository.GetTipGarancijeById(tipGarancije.TipId);
                if (stariTip == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                TipGarancije tipGarancijeEntity = mapper.Map<TipGarancije>(tipGarancije);
                mapper.Map(tipGarancijeEntity, stariTip);
                tipGarancijeRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<TipGarancijeDto>(tipGarancijeEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

        }


        /// <summary>
        /// Vraća opcije za rad sa tipovima garancije
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetTipGarancijeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa tipovima garancije";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
