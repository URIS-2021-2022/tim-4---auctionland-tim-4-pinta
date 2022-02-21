using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Data;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;
using UgovorOZakupuAgregat.ServiceCalls;

namespace UgovorOZakupuAgregat.Controllers
{
    /// <summary>
    /// Sadrži CRUD operacije za dokumente
    /// </summary>
    [ApiController]
    [Route("api/dokumenti")]
    [Produces("application/json", "application/xml")]
    public class DokumentController : ControllerBase
    {
        private readonly IDokumentRepository dokumentRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly LoggerService loggerService;
        private readonly LogDto logDto;

        public DokumentController(IDokumentRepository dokumentRepository, LinkGenerator linkGenerator, IMapper mapper, LoggerService loggerService)
        {
            this.dokumentRepository = dokumentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "UgovorOZakupu";
        }

        /// <summary>
        /// Vraća sve dokumente na osnovu prosleđenih filtera
        /// </summary>
        /// <returns>Listu dokumenata</returns>
        /// <response code="200">Vraća listu dokumenata</response>
        /// <response code="404">Nije pronađena ni jedan jedini dokument</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<DokumentDto>> GetDokumenti()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih dokumenata";

            List<Dokument> dokumenti = dokumentRepository.GetDokumenti();
            if (dokumenti == null || dokumenti.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<List<DokumentDto>>(dokumenti));
        }



        /// <summary>
        /// Vraća jedan dokument na osnovu ID-ja dokumenta.
        /// </summary>
        /// <param name="dokumentId">ID dokumenta</param>
        /// <returns></returns>
        /// <response code="200">Vraća tražen dokument</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{dokumentId}")]
        public ActionResult<DokumentDto> GetDokument(Guid dokumentId)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje dokumenta po ID-ju";

            Dokument dokument = dokumentRepository.GetDokumentById(dokumentId);
            if (dokument == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(mapper.Map<DokumentDto>(dokument));
        }


        /// <summary>
        /// Kreira novi dokument
        /// </summary>
        /// <param name="dokument">Model dokumenta</param>
        /// <returns>Potvrdu o kreiranom novom dokumentu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dokumenta \
        /// POST /api/dokumenti \
        /// {     \
        ///     "ZavodniBroj = "1234a", \
        ///     "Datum = "25-01-2021", \
        ///     "DatumDonosenjaDokumenta = "25-01-2021"\
        ///     
        ///}     
        /// </remarks>
        ///  <response code="201">Vraća kreiran dokument</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja dokumenta</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<DokumentDto> CreateDokument([FromBody] DokumentDto dokument)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje novog dokumenta";

            try
            {
                Dokument dokumentEntity = mapper.Map<Dokument>(dokument);
                Dokument dokumentCreate = dokumentRepository.CreateDokument(dokumentEntity);
                dokumentRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetDokument", "Dokument", new { dokumentId = dokumentCreate.DokumentId});
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<DokumentDto>(dokumentCreate));
                
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// Vrši brisanje jednog dokumenta na osnovu ID-ja dokumenta.
        /// </summary>
        /// <param name="dokumentId">ID dokumenta</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Dokument uspešno obrisan</response>
        /// <response code="404">Nije pronađen dokument za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dokumenta</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{dokumentId}")]
        public IActionResult DeleteDokument(Guid dokumentId)
        {
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje dokumenta";

            try
            {
                Dokument dokumentModel = dokumentRepository.GetDokumentById(dokumentId);
                if (dokumentModel == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                dokumentRepository.DeleteDokument(dokumentId);
                dokumentRepository.SaveChanges();
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
        /// Ažurira jednan dokument.
        /// </summary>
        /// <param name="dokument">Model dokumenta koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom dokumentu.</returns>
        /// <response code="200">Vraća ažuriran dokument</response>
        /// <response code="400">Dokument koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dokumenta</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<DokumentDto> UpdateDokument(DokumentUpdateDto dokument)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikovanje dela parcele";

            try
            {
                var stariDokument = dokumentRepository.GetDokumentById(dokument.DokumentId);
                if (dokumentRepository.GetDokumentById(dokument.DokumentId) == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                Dokument dokumentEntity = mapper.Map<Dokument>(dokument);
                mapper.Map(dokumentEntity, stariDokument);
                dokumentRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<DokumentDto>(dokumentEntity));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }

        }


        /// <summary>
        /// Vraća opcije za rad sa dokumentima
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        [HttpOptions]
        public IActionResult GetDokumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa dokumentima";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
