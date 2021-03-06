using AutoMapper;
using KatastarskaOpstinaAgregat.Models;
using KatastarskaOpstinaAgregat.Data;
using KatastarskaOpstinaAgregat.Entities;
using KatastarskaOpstinaAgregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KatastarskaOpstinaAgregat.ServiceCalls;
using System.Net;

namespace KatastarskaOpstinaAgregat.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za adresu
    /// </summary>
    [ApiController]
    [Route("api/katastarskeOpstine")]
    [Produces("application/json", "application/xml")]
    public class KatastarskaOpstinaController : ControllerBase
    {
        private readonly IKatastarskaOpstinaRepository katastarskaOpstinaRepository;
        private readonly IGatewayService gatewayService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;
        private readonly IKorisnikService korisnikSistemaService;
       

        public KatastarskaOpstinaController(IKatastarskaOpstinaRepository katastarskaOpstinaRepository, IGatewayService gatewayService, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKorisnikService korisnikSistemaService)
        {
            this.katastarskaOpstinaRepository = katastarskaOpstinaRepository;
            this.linkGenerator = linkGenerator;
            this.gatewayService = gatewayService;
            this.korisnikSistemaService = korisnikSistemaService;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "KatastarskaOpstina";
        }


        /// <summary>
        /// Vraca sve katastarske opstine na osnovu odredjenih filtera
        /// </summary>
        /// <returns>Lista katastarskih opstina</returns>
        /// <response code = "200">Vraca listu katastarskih opstina</response>
        /// <response code = "404">Nije pronadjena nijedna katastarska opstina</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<KatastarskaOpstinaDto>> GetSluzbeneListove()
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
            logDto.Message = "Vracanje svih opstina";
            List<KatastarskaOpstinaEntity> katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstina();
            if (katastarskaOpstina == null || katastarskaOpstina.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<KatastarskaOpstinaDto>>(katastarskaOpstina));
        }

        /// <summary>
        /// Vraca jednu katastarsku opstinu na osnovu ID-ja
        /// </summary>
        /// <param name="katastarskaOpstinaID">ID katastarska opstina</param>
        /// <returns>Trazena katastarska opstina</returns>
        /// <response code = "200">Vraca trazenu katastarsku opstinu</response>
        /// <response code = "404">Trazena katastarske opstina nije pronadjen</response>
        [HttpGet("{katastarskaOpstinaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KatastarskaOpstinaDto> GetKatastarskaOpstina(Guid katastarskaOpstinaID)
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
            logDto.Message = "Vracanje opstine po ID-ju";
            KatastarskaOpstinaEntity katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);
            if (katastarskaOpstina == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<KatastarskaOpstinaDto>(katastarskaOpstina));
        }

        /// <summary>
        /// Kreira novu katastarsku opstinu
        /// </summary>
        /// <param name="katastarskaOpstina">Model katastarska opstina</param>
        /// <returns>Potvrda o kreiranoj katastarskoj opstini</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove katastarske opstine \
        /// POST /api/katastarskaOpstina \
        /// { \
        /// "NazivKatastarskeOpstine": "Beograd", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu katastarsku opstinu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja katastarske opstine</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KatastarskaOpstinaDto> CreateKatastarskaOpstina([FromBody] KatastarskaOpstinaDto katastarskaOpstina)
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
            logDto.Message = "Dodavanje nove opstine";

            try
            {
                KatastarskaOpstinaEntity obj = mapper.Map<KatastarskaOpstinaEntity>(katastarskaOpstina);
                KatastarskaOpstinaEntity s = katastarskaOpstinaRepository.CreateKatastarskaOpstina(obj);
                katastarskaOpstinaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKatastarskaOpstina", "KatastarskaOpstina", new { katastarskaOpstinaID = s.KatastarskaOpstinaID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<KatastarskaOpstinaDto>(s));
               
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne katastarske opstine na osnovu ID-ja
        /// </summary>
        /// <param name="katastarskaOpstinaID">ID katastarskaOpstina</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Katastarska opstina uspesno obrisano</response>
        /// <response code="404">Nije pronadjena katastarska opstina za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja katastarske opstine</response>
        [HttpDelete("{katastarskaOpstinaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteKatastarskaOpstina(Guid katastarskaOpstinaID)
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
            logDto.Message = "Brisanje opstine";

            try
            {
                KatastarskaOpstinaEntity katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);
                if (katastarskaOpstina == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                katastarskaOpstinaRepository.DeleteKatastarskaOpstina(katastarskaOpstinaID);
                katastarskaOpstinaRepository.SaveChanges();
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
        /// Azurira jednu katastarsku opstinu
        /// </summary>
        /// <param name="katastarskaOpstina">Model katastarske opstine koji se azurira</param>
        /// <returns>Potvrdu o modifikovanoj katastarskoj opstini</returns>
        /// <response code="200">Vraca azuriranu katastarsku opstinu</response>
        /// <response code="400">Katastarska opstina koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja katastarske opstine</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KatastarskaOpstinaDto> UpdateKatastarskaOpstina(KatastarskaOpstinaUpdateDto katastarskaOpstina)
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
            logDto.Message = "Modifikovanje katastarske opstine ";

            try
            {
                var oldKatastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID);
                if (katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID) == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }

                KatastarskaOpstinaEntity katastarskaOpstinaEntity = mapper.Map<KatastarskaOpstinaEntity>(katastarskaOpstina);
                mapper.Map(katastarskaOpstinaEntity, oldKatastarskaOpstina); //Update objekta koji treba da sačuvamo u bazi                

                katastarskaOpstinaRepository.SaveChanges(); //Perzistiramo promene
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);

                return Ok(mapper.Map<KatastarskaOpstinaDto>(katastarskaOpstinaEntity));

            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");


            }
        }

        /// <summary>
        /// Vraca opcije za rad sa katastarskim opstinama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKatastarskaOpstinaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa adresama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }

    }
}
