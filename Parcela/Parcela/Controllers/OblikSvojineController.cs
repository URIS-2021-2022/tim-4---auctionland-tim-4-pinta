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
    /// Sadzi CRUD operacije za oblike svojine
    /// </summary>
    [ApiController]
    [Route("api/obliciSvojine")]
    [Produces("application/json", "application/xml")]
    public class OblikSvojineController : ControllerBase
    {
        private readonly IOblikSvojineRepository oblikSvojineRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public OblikSvojineController(IOblikSvojineRepository oblikSvojineRepository, LinkGenerator linkGenerator, IMapper mapper, IKorisnikSistemaService korisnikSistemaService, ILoggerService loggerService)
        {
            this.oblikSvojineRepository = oblikSvojineRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve oblike svojine
        /// </summary>
        /// <returns>Lista oblika svojine</returns>
        /// <response code = "200">Vraca listu oblika svojine</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije pronadjen nijedan oblik svojine</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OblikSvojineDto>> GetObradivosti()
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
            logDto.Message = "Vracanje svih oblika svojine";

            List<OblikSvojineEntity> obliciSvojine = oblikSvojineRepository.GetObliciSvojine();
            if (obliciSvojine == null || obliciSvojine.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<OblikSvojineDto>>(obliciSvojine));
        }

        /// <summary>
        /// Vraca jedan oblik svojien na osnovu ID-ja
        /// </summary>
        /// <param name="oblikSvojineID">ID oblika svojine</param>
        /// <returns>Trazen oblik svojine</returns>
        /// <response code = "200">Vraca trazen oblik svojine</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazen oblik svojine nije pronadjen</response>
        [HttpGet("{oblikSvojineID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<OblikSvojineDto> GetOblikSvojine(Guid oblikSvojineID)
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
            logDto.Message = "Vracanje oblika svojine po ID-ju";

            OblikSvojineEntity oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
            if (oblikSvojine == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<ObradivostDto>(oblikSvojine));
        }

        /// <summary>
        /// Kreira novi oblik svojine
        /// </summary>
        /// <param name="oblikSvojine">oblik svojine</param>
        /// <returns>Potvrda o kreiranom obliku svojine</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog oblika svojine \
        /// POST /api/obliciSvojine \
        /// { \
        /// "oblikSvoijenNaziv": "Privatna svojina", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiran oblik svojine</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja oblika svojine</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OblikSvojineDto> CreateOblikSvojine([FromBody] OblikSvojineCreateDto oblikSvojine)
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
            logDto.Message = "Dodavanje novog oblika svojine";

            try
            {
                OblikSvojineEntity obl = mapper.Map<OblikSvojineEntity>(oblikSvojine);
                OblikSvojineEntity os = oblikSvojineRepository.CreateOblikSvojine(obl);
                oblikSvojineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetOblikSvojine", "OblikSvojine", new { oblikSvojineID = os.OblikSvojineID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<OblikSvojineDto>(os));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jednog oblika svojine na osnovu ID-ja
        /// </summary>
        /// <param name="oblikSvojineID">ID oblika svojine</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Oblik svojine uspesno obrisan</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjen oblik svojine za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja oblika svojine</response>
        [HttpDelete("{oblikSvoijneID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOblikSvojine(Guid oblikSvojineID)
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
            logDto.Message = "Brisanje oblika svojine";

            try
            {
                OblikSvojineEntity oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
                if (oblikSvojine == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                oblikSvojineRepository.DeleteOblikSvojine(oblikSvojineID);
                oblikSvojineRepository.SaveChanges();
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
        /// Azurira jedan oblik svojine
        /// </summary>
        /// <param name="oblikSvojine">Model oblika svojine za azuriranje</param>
        /// <returns>Potvrda o modifikovanom obliku svojine</returns>
        /// <remarks>
        /// Primer zahteva za modifikovanje oblika svojine \
        /// PUT /api/obliciSvojine \
        /// { \
        /// "oblikSvojineID": "0051339e-4bf1-4d63-89f9-d5f744016a2b", \
        /// "oblikSvoijenNaziv": "Privatna svojina", \
        /// } 
        /// </remarks>
        /// <response code="200">Vraca azuriran oblik svojine</response>
        /// <response code="400">Oblik svojine koji se azurira nije pronadjen</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja oblika svojine</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OblikSvojineDto> UpdateOblikSvojine(OblikSvojineUpdateDto oblikSvojine)
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
            logDto.Message = "Modifikovanje oblika svojine";

            try
            {
                var oldOblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojine.OblikSvojineID);
                if (oldOblikSvojine == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                OblikSvojineEntity oblikSvojineEntity = mapper.Map<OblikSvojineEntity>(oblikSvojine);

                oldOblikSvojine.OblikSvojineNaziv = oblikSvojineEntity.OblikSvojineNaziv;

                oblikSvojineRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<OblikSvojineDto>(oldOblikSvojine));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa oblicima svojine
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetOblikSvojineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa oblicima svojine";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
