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
using UgovorOZakupuAgregat.ServiceCalls;
using System.Net;

namespace UgovorOZakupuAgregat.Controllers
{
    /// <summary>
    /// Sadrži CRUD operacije za ugovore o zakupu
    /// </summary>
    [ApiController]
    [Route("api/ugovori")]
    [Produces("application/json", "application/xml")]
    public class UgovorOZakupuController : ControllerBase
    {
        private readonly IUgovorOZakupuRepository ugovorRepository;
        private readonly IDokumentRepository dokumentRepository;
        private readonly ITipGarancijeRepository tipGarancijeRepository;
        private readonly ILicnostService licnostService;
        private readonly IKupacService kupacService;
        private readonly IJavnoNadmetanjeService javnoNadmetanjeService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public UgovorOZakupuController(IUgovorOZakupuRepository ugovorRepository, IDokumentRepository dokumentRepository, ITipGarancijeRepository tipGarancijeRepository, LinkGenerator linkGenerator, IMapper mapper, ILicnostService licnostService, IKupacService kupacService, IJavnoNadmetanjeService javnoNadmetanjeService, ILoggerService loggerService,IKorisnikSistemaService korisnikSistemaService)
        {
            this.ugovorRepository = ugovorRepository;
            this.dokumentRepository = dokumentRepository;
            this.tipGarancijeRepository = tipGarancijeRepository;
            this.licnostService = licnostService;
            this.kupacService = kupacService;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "UgovorOZakupu";
        }

        /// <summary>
        /// Vraća sve ugovore na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu ugovora</returns>
        /// <response code="200">Vraća listu ugovora</response>
        /// <response code="404">Nije pronađena ni jedan jedini ugovor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<UgovorOZakupuDto>> GetUgovori()
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
            logDto.Message = "Vracanje svih ugovora";

            List<UgovorOZakupu> ugovori = ugovorRepository.GetUgovori();
            if (ugovori == null || ugovori.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            List<UgovorOZakupuDto> ugovoriDto = new List<UgovorOZakupuDto>();
            foreach (UgovorOZakupu u in ugovori)
            {
                UgovorOZakupuDto ugovorDto = mapper.Map<UgovorOZakupuDto>(u);
                ugovorDto.Dokument = mapper.Map<DokumentDto>(dokumentRepository.GetDokumentById(u.DokumentId));
                ugovorDto.TipGarancije = mapper.Map<TipGarancijeDto>(tipGarancijeRepository.GetTipGarancijeById(u.TipId));
                ugovorDto.Licnost = licnostService.GetLicnostByIdAsync(u.LicnostId).Result;
                ugovorDto.Kupac = kupacService.GetKupacByIdAsync(u.KupacId).Result;
                ugovorDto.JavnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(u.JavnoNadmetanjeId).Result;
                ugovoriDto.Add(ugovorDto);
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(ugovoriDto);
            
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{ugovorId}")]
        public ActionResult<UgovorOZakupuDto> GetUgovor(Guid ugovorId)
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
            logDto.Message = "Vracanje ugovora po ID-ju";
            UgovorOZakupu ugovor = ugovorRepository.GetUgovorById(ugovorId);
            if (ugovor == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
           
            LicnostUgovoraDto licnost = licnostService.GetLicnostByIdAsync(ugovor.LicnostId).Result;
            KupacUgovoraDto kupac = kupacService.GetKupacByIdAsync(ugovor.KupacId).Result;
            JavnoNadmetanjeUgovoraDto javnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(ugovor.JavnoNadmetanjeId).Result;
            UgovorOZakupuDto ugovorDto = mapper.Map<UgovorOZakupuDto>(ugovor);
            ugovorDto.Dokument= mapper.Map<DokumentDto>(dokumentRepository.GetDokumentById(ugovor.DokumentId));
            ugovorDto.TipGarancije = mapper.Map<TipGarancijeDto>(tipGarancijeRepository.GetTipGarancijeById(ugovor.TipId));
            ugovorDto.Licnost = licnost;
            ugovorDto.Kupac = kupac;
            ugovorDto.JavnoNadmetanje = javnoNadmetanje;
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(ugovorDto);
           
        }


        /// <summary>
        /// Kreira novi ugovor
        /// </summary>
        /// <param name="ugovor">Model ugovora</param>
        /// <returns>Potvrdu o kreiranom novom ugovoru</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog ugovora \
        /// POST /api/ugovori \
        /// {     \
        ///     "DokumentId= Guid.Parse("D1209104-7358-4C22-9F4F-415203563A25")", \
        ///     "TipId= Guid.Parse("E1F134E5-25F9-4B00-8B96-A809D11CD33B")", \
        ///     "RokId= Guid.Parse("234D1ADA-07B8-4789-9C87-86B83118FED0")", \
        ///     "ZavodniBroj="11a", \
        ///     "DatumZavodjenja= "24-01-2021", \
        ///     "RokZaVracanjeZemljista= "24-05-2021", \
        ///     "MestoPotpisivanja="Novi Sad"", \
        ///     "DatumPotpisa ="25-01-2021", \
        ///}
        ///      
        /// </remarks>
        ///  <response code="201">Vraća kreiran ugovor</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja ugovora</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<UgovorOZakupuDto> CreateUgovor([FromBody] UgovorOZakupuCreateDto ugovor)
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
            logDto.Message = "Dodavanje novog ugovora";

            try
            {
                UgovorOZakupu ugovorEntity = mapper.Map<UgovorOZakupu>(ugovor);
                UgovorOZakupu ugovorCreate = ugovorRepository.CreateUgovor(ugovorEntity);
                ugovorRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetUgovor", "UgovorOZakupu", new { ugovorId = ugovorCreate.UgovorId });
                LicnostUgovoraDto licnost = licnostService.GetLicnostByIdAsync(ugovorCreate.LicnostId).Result;
                KupacUgovoraDto kupac = kupacService.GetKupacByIdAsync(ugovorCreate.KupacId).Result;
                JavnoNadmetanjeUgovoraDto javnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(ugovorCreate.JavnoNadmetanjeId).Result;
                UgovorOZakupuDto ugovorDto = mapper.Map<UgovorOZakupuDto>(ugovorCreate);
                ugovorDto.Dokument = mapper.Map<DokumentDto>(dokumentRepository.GetDokumentById(ugovor.DokumentId));
                ugovorDto.TipGarancije = mapper.Map<TipGarancijeDto>(tipGarancijeRepository.GetTipGarancijeById(ugovor.TipId));
                ugovorDto.Licnost = licnost;
                ugovorDto.Kupac = kupac;
                ugovorDto.JavnoNadmetanje = javnoNadmetanje;
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location,ugovorDto);
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{ugovorId}")]
        public IActionResult DeleteUgovor(Guid ugovorId)
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
            logDto.Message = "Brisanje ugovora";

            try
            {
                UgovorOZakupu ugovorModel = ugovorRepository.GetUgovorById(ugovorId);
                if (ugovorModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                ugovorRepository.DeleteUgovor(ugovorId);
                ugovorRepository.SaveChanges();
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<UgovorOZakupuDto> UpdateUgovor(UgovorOZakupuUpdateDto ugovor)
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
            logDto.Message = "Izmena ugovora";

            try
            {
                var stariUgovor = ugovorRepository.GetUgovorById(ugovor.UgovorId);
                if (ugovorRepository.GetUgovorById(ugovor.UgovorId) == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                UgovorOZakupu ugovorEntity = mapper.Map<UgovorOZakupu>(ugovor);
                mapper.Map(ugovorEntity, stariUgovor);
                ugovorRepository.SaveChanges();
                LicnostUgovoraDto licnost = licnostService.GetLicnostByIdAsync(ugovorEntity.LicnostId).Result;
                KupacUgovoraDto kupac = kupacService.GetKupacByIdAsync(ugovorEntity.KupacId).Result;
                JavnoNadmetanjeUgovoraDto javnoNadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByIdAsync(ugovorEntity.JavnoNadmetanjeId).Result;
                UgovorOZakupuDto ugovorDto = mapper.Map<UgovorOZakupuDto>(ugovorEntity);
                ugovorDto.Dokument = mapper.Map<DokumentDto>(dokumentRepository.GetDokumentById(ugovor.DokumentId));
                ugovorDto.TipGarancije = mapper.Map<TipGarancijeDto>(tipGarancijeRepository.GetTipGarancijeById(ugovor.TipId));
                ugovorDto.Licnost = licnost;
                ugovorDto.Kupac = kupac;
                ugovorDto.JavnoNadmetanje = javnoNadmetanje;
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(ugovorDto);
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
            
        }


        /// <summary>
        /// Vraća opcije za rad sa ugovorima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetUgovorOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa ugovorima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
