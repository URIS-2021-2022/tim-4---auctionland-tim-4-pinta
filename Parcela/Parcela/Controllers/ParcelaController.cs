using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Parcela.Entities;
using Parcela.ServiceCals;

namespace Parcela.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za parcele
    /// </summary>
    [ApiController]
    [Route("api/parcele")]
    [Produces("application/json", "application/xml")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly IKatastarskaOpstinaService katastarskaOpstinaService;
        private readonly IKupacService kupacService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper, IKatastarskaOpstinaService katastarskaOpstinaService, IKupacService kupacService, ILoggerService loggerService)
        {
            this.parcelaRepository = parcelaRepository;
            this.katastarskaOpstinaService = katastarskaOpstinaService;
            this.kupacService = kupacService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve parcele
        /// </summary>
        /// <returns>Lista parcela</returns>
        /// <response code = "200">Vraca listu parcela</response>
        /// <response code = "404">Nije pronadjena nijedna parcela</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ParcelaDto>> GetParcele()
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje svih parcela";

            List<ParcelaEntity> parcele = parcelaRepository.GetParcele();
            if (parcele == null || parcele.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            List<ParcelaDto> parceleDto = mapper.Map<List<ParcelaDto>>(parcele);
            foreach(ParcelaDto p in parceleDto)
            {
                p.Kupac = kupacService.GetKupacByIdAsync(p.KupacID).Result;
                p.Opstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(p.KatastarskaOpstinaID).Result;
            }
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(parceleDto);
            //return Ok(mapper.Map<List<ParcelaDto>>(parcele));
        }

        /// <summary>
        /// Vraca jednu parcelu na osnovu ID-ja
        /// </summary>
        /// <param name="parcelaID">ID parcele</param>
        /// <returns>Trazena parcela</returns>
        /// <response code = "200">Vraca trazenu parcelu</response>
        /// <response code = "404">Trazena parcela nije pronadjena</response>
        [HttpGet("{parcelaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ParcelaDto> GetParcela(Guid parcelaID)
        {
            logDto.HttpMethod = "GET";
            logDto.Message = "Vracanje parcele po ID-ju";

            ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);
            if(parcela == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            ParcelaDto parcelaDto = mapper.Map<ParcelaDto>(parcela);
            parcelaDto.Kupac = kupacService.GetKupacByIdAsync(parcela.KupacID).Result;
            parcelaDto.Opstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(parcelaDto.KatastarskaOpstinaID).Result;
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(parcelaDto);
            //return Ok(mapper.Map<ParcelaDto>(parcela));
        }

        /// <summary>
        /// Kreira novu parcelu
        /// </summary>
        /// <param name="parcela">Model parcele</param>
        /// <returns>Potvrdu o kreiranoj parceli</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove parcele \
        /// POST /api/parcele \
        /// { \
        /// "povrsina": 3000, \
        /// "brojParcele": "333", \
        /// "brojListaNepokretnosti": "333", \
        /// "kulturaStvarnoStanje": "Kukuruz", \
        /// "klasaStvarnoStanje": "Klasa1", \
        /// "obradivostStvarnoStanje": "Obradivost1", \
        /// "zasticenaZonaStvarnoStanje": "ZasticenaZona1", \
        /// "odvodnjavanjeStvarnoStanje": "Odvodnjavanje1" \
        /// "kulturaID": "a873025a-b4bc-440d-8e65-dc63fb9025d7", \
        /// "klasaID": "a873025a-b4bc-440d-8e65-dc63fb9025d7", \
        /// "obradivostID": "a873025a-b4bc-440d-8e65-dc63fb9025d7", \
        /// "zasticenaZonaID": "a873025a-b4bc-440d-8e65-dc63fb9025d7", \
        /// "odvodnjavanjeID": "a873025a-b4bc-440d-8e65-dc63fb9025d7" \
        /// "opstinaID": "a873025a-b4bc-440d-8e65-dc63fb9025d7", \
        /// "kupacID": "a873025a-b4bc-440d-8e65-dc63fb9025d7" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraca kreiranu parcelu</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja parcele</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> CreateParcela([FromBody] ParcelaCreateDto parcela)
        {
            logDto.HttpMethod = "POST";
            logDto.Message = "Dodavanje nove parcele";

            try
            {
                ParcelaEntity par = mapper.Map<ParcelaEntity>(parcela);
                ParcelaEntity p = parcelaRepository.CreateParcela(par);
                parcelaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetParcela", "Parcela", new { parcelaID = p.ParcelaID });
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, mapper.Map<ParcelaDto>(p));
            }
            catch
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");            
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne parcele na osnovu ID-ja
        /// </summary>
        /// <param name="parcelaID">ID parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Parcela uspesno obrisana</response>
        /// <response code="404">Nije pronadjena parcela za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja parcele</response>
        [HttpDelete("{parcelaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteParcela(Guid parcelaID)
        {
            logDto.HttpMethod = "DELETE";
            logDto.Message = "Brisanje parcele";

            try
            {
                ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);               
                if(parcela == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                parcelaRepository.DeleteParcela(parcelaID);
                parcelaRepository.SaveChanges();
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
        /// Azurira jednu parcelu
        /// </summary>
        /// <param name="parcela">Model parcele koja se azurira</param>
        /// <returns>Potvrdu o modifikovanoj parceli</returns>
        /// <response code="200">Vraca azuriranu parcelu</response>
        /// <response code="400">Parcela koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja parcele</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> UpdateParcela(ParcelaUpdateDto parcela)
        {
            logDto.HttpMethod = "PUT";
            logDto.Message = "Modifikacija parcele";

            try
            {
                ParcelaEntity oldParcela = parcelaRepository.GetParcelaById(parcela.ParcelaID);
                if (oldParcela == null)
                {
                    logDto.Level = "Warn";
                    loggerService.CreateLog(logDto);
                    return NotFound();
                }
                ParcelaEntity parcelaEntity = mapper.Map<ParcelaEntity>(parcela);

                oldParcela.Povrsina = parcelaEntity.Povrsina;
                oldParcela.BrojParcele = parcelaEntity.BrojParcele;
                oldParcela.BrojListaNepokretnosti = parcelaEntity.BrojListaNepokretnosti;
                oldParcela.KulturaStvarnoStanje = parcelaEntity.KulturaStvarnoStanje;
                oldParcela.KlasaStvarnoStanje = parcelaEntity.KlasaStvarnoStanje;
                oldParcela.ObradivostStvarnoStanje = parcelaEntity.ZasticenaZonaStvarnoStanje;
                oldParcela.OdvodnjavanjeStvarnoStanje = parcelaEntity.OdvodnjavanjeStvarnoStanje;
                oldParcela.ZasticenaZonaID = parcelaEntity.ZasticenaZonaID;
                oldParcela.OdvodnjavanjeID = parcelaEntity.OdvodnjavanjeID;
                oldParcela.ObradivostID = parcelaEntity.ObradivostID;
                oldParcela.OblikSvojineID = parcelaEntity.OblikSvojineID;
                oldParcela.KulturaID = parcelaEntity.KulturaID;
                oldParcela.KlasaID = parcelaEntity.KlasaID;
                oldParcela.KatastarskaOpstinaID = parcelaEntity.KatastarskaOpstinaID;
                oldParcela.KupacID = parcelaEntity.KupacID;

                parcelaRepository.SaveChanges();
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(mapper.Map<ParcelaDto>(oldParcela));
            }
            catch (Exception)
            {
                logDto.Level = "Error";
                loggerService.CreateLog(logDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa parcelama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetParcelaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            logDto.HttpMethod = "OPTIONS";
            logDto.Message = "Opcije za rad sa parcelama";
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok();
        }
    }
}
