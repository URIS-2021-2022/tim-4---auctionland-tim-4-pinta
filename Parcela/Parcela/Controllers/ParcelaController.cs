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
using System.Net;

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
        private readonly IKlasaRepository klasaRepository;
        private readonly IKulturaRepository kulturaRepository;
        private readonly IOblikSvojineRepository oblikSvojineRepository;
        private readonly IObradivostRepository obradivostRepository;
        private readonly IOdvodnjavanjeRepository odvodnjavanjeRepository;
        private readonly IZasticenaZonaRepository zasticenaZonaRepository;
        private readonly IKatastarskaOpstinaService katastarskaOpstinaService;
        private readonly IKupacService kupacService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IKorisnikSistemaService korisnikSistemaService;
        private readonly ILoggerService loggerService;
        private readonly LogDto logDto;

        public ParcelaController(IParcelaRepository parcelaRepository, IKlasaRepository klasaRepository, IKulturaRepository kulturaRepository, IOblikSvojineRepository oblikSvojineRepository, IObradivostRepository obradivostRepository, IOdvodnjavanjeRepository odvodnjavanjeRepository, IZasticenaZonaRepository zasticenaZonaRepository, LinkGenerator linkGenerator, IMapper mapper, IKorisnikSistemaService korisnikSistemaService, IKatastarskaOpstinaService katastarskaOpstinaService, IKupacService kupacService, ILoggerService loggerService)
        {
            this.parcelaRepository = parcelaRepository;
            this.klasaRepository = klasaRepository;
            this.kulturaRepository = kulturaRepository;
            this.oblikSvojineRepository = oblikSvojineRepository;
            this.obradivostRepository = obradivostRepository;
            this.odvodnjavanjeRepository = odvodnjavanjeRepository;
            this.zasticenaZonaRepository = zasticenaZonaRepository;
            this.katastarskaOpstinaService = katastarskaOpstinaService;
            this.kupacService = kupacService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.korisnikSistemaService = korisnikSistemaService;
            this.loggerService = loggerService;
            logDto = new LogDto();
            logDto.NameOfTheService = "Parcela";
        }

        /// <summary>
        /// Vraca sve parcele
        /// </summary>
        /// <returns>Lista parcela</returns>
        /// <response code = "200">Vraca listu parcela</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Nije pronadjena nijedna parcela</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ParcelaDto>> GetParcele()
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
            logDto.Message = "Vracanje svih parcela";

            List<ParcelaEntity> parcele = parcelaRepository.GetParcele();
            if (parcele == null || parcele.Count == 0)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NoContent();
            }
            List<ParcelaDto> parceleDto = new List<ParcelaDto>();
            foreach(ParcelaEntity p in parcele)
            {
                ParcelaDto parcelaDto = mapper.Map<ParcelaDto>(p);
                parcelaDto.Klasa = mapper.Map<KlasaDto>(klasaRepository.GetKlasaById(p.KlasaID));
                parcelaDto.Kultura = mapper.Map<KulturaDto>(kulturaRepository.GetKulturaById(p.KulturaID));
                parcelaDto.OblikSvojine = mapper.Map<OblikSvojineDto>(oblikSvojineRepository.GetOblikSvojineById(p.OblikSvojineID));
                parcelaDto.Obradivost = mapper.Map<ObradivostDto>(obradivostRepository.GetObradivostById(p.ObradivostID));
                parcelaDto.Odvodnjavanje = mapper.Map<OdvodnjavanjeDto>(odvodnjavanjeRepository.GetOdvodnjavanjeById(p.OdvodnjavanjeID));
                parcelaDto.ZasticenaZona = mapper.Map<ZasticenaZonaDto>(zasticenaZonaRepository.GetZasticenaZonaById(p.ZasticenaZonaID));
                parcelaDto.Kupac = kupacService.GetKupacByIdAsync(p.KupacID).Result;
                parcelaDto.Opstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(p.KatastarskaOpstinaID).Result;
                parceleDto.Add(parcelaDto);
            }
        
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(parceleDto);
        }

        /// <summary>
        /// Vraca jednu parcelu na osnovu ID-ja
        /// </summary>
        /// <param name="parcelaID">ID parcele</param>
        /// <returns>Trazena parcela</returns>
        /// <response code = "200">Vraca trazenu parcelu</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "404">Trazena parcela nije pronadjena</response>
        [HttpGet("{parcelaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ParcelaDto> GetParcela(Guid parcelaID)
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
            logDto.Message = "Vracanje parcele po ID-ju";

            ParcelaEntity parcela = parcelaRepository.GetParcelaById(parcelaID);
            if(parcela == null)
            {
                logDto.Level = "Warn";
                loggerService.CreateLog(logDto);
                return NotFound();
            }
            ParcelaDto parcelaDto = mapper.Map<ParcelaDto>(parcela);
            parcelaDto.Klasa = mapper.Map<KlasaDto>(klasaRepository.GetKlasaById(parcela.KlasaID));
            parcelaDto.Kultura = mapper.Map<KulturaDto>(kulturaRepository.GetKulturaById(parcela.KulturaID));
            parcelaDto.OblikSvojine = mapper.Map<OblikSvojineDto>(oblikSvojineRepository.GetOblikSvojineById(parcela.OblikSvojineID));
            parcelaDto.Obradivost = mapper.Map<ObradivostDto>(obradivostRepository.GetObradivostById(parcela.ObradivostID));
            parcelaDto.Odvodnjavanje = mapper.Map<OdvodnjavanjeDto>(odvodnjavanjeRepository.GetOdvodnjavanjeById(parcela.OdvodnjavanjeID));
            parcelaDto.ZasticenaZona = mapper.Map<ZasticenaZonaDto>(zasticenaZonaRepository.GetZasticenaZonaById(parcela.ZasticenaZonaID));
            parcelaDto.Kupac = kupacService.GetKupacByIdAsync(parcela.KupacID).Result;
            parcelaDto.Opstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(parcela.KatastarskaOpstinaID).Result;
            logDto.Level = "Info";
            loggerService.CreateLog(logDto);
            return Ok(parcelaDto);
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
        /// "kulturaStvarnoStanje": "Njive", \
        /// "klasaStvarnoStanje": "II", \
        /// "obradivostStvarnoStanje": "Obradivo", \
        /// "zasticenaZonaStvarnoStanje": 1, \
        /// "odvodnjavanjeStvarnoStanje": "Podzemno" \
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
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code = "500">Doslo je do greske na serveru prilikom kreiranja parcele</response>
        [HttpPost]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> CreateParcela([FromBody] ParcelaCreateDto parcela)
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
            logDto.Message = "Dodavanje nove parcele";

            try
            {
                ParcelaEntity par = mapper.Map<ParcelaEntity>(parcela);
                ParcelaEntity p = parcelaRepository.CreateParcela(par);
                parcelaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetParcela", "Parcela", new { parcelaID = p.ParcelaID });
                ParcelaDto parcelaDto = mapper.Map<ParcelaDto>(p);
                parcelaDto.Klasa = mapper.Map<KlasaDto>(klasaRepository.GetKlasaById(p.KlasaID));
                parcelaDto.Kultura = mapper.Map<KulturaDto>(kulturaRepository.GetKulturaById(p.KulturaID));
                parcelaDto.OblikSvojine = mapper.Map<OblikSvojineDto>(oblikSvojineRepository.GetOblikSvojineById(p.OblikSvojineID));
                parcelaDto.Obradivost = mapper.Map<ObradivostDto>(obradivostRepository.GetObradivostById(p.ObradivostID));
                parcelaDto.Odvodnjavanje = mapper.Map<OdvodnjavanjeDto>(odvodnjavanjeRepository.GetOdvodnjavanjeById(p.OdvodnjavanjeID));
                parcelaDto.ZasticenaZona = mapper.Map<ZasticenaZonaDto>(zasticenaZonaRepository.GetZasticenaZonaById(p.ZasticenaZonaID));
                parcelaDto.Kupac = kupacService.GetKupacByIdAsync(p.KupacID).Result;
                parcelaDto.Opstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(p.KatastarskaOpstinaID).Result;
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Created(location, parcelaDto);
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
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="404">Nije pronadjena parcela za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja parcele</response>
        [HttpDelete("{parcelaID}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteParcela(Guid parcelaID)
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
        /// <response code="401">Korisnik nije autorizovan</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja parcele</response>
        [HttpPut]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> UpdateParcela(ParcelaUpdateDto parcela)
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
                ParcelaDto parcelaDto = mapper.Map<ParcelaDto>(oldParcela);
                parcelaDto.Klasa = mapper.Map<KlasaDto>(klasaRepository.GetKlasaById(oldParcela.KlasaID));
                parcelaDto.Kultura = mapper.Map<KulturaDto>(kulturaRepository.GetKulturaById(oldParcela.KulturaID));
                parcelaDto.OblikSvojine = mapper.Map<OblikSvojineDto>(oblikSvojineRepository.GetOblikSvojineById(oldParcela.OblikSvojineID));
                parcelaDto.Obradivost = mapper.Map<ObradivostDto>(obradivostRepository.GetObradivostById(oldParcela.ObradivostID));
                parcelaDto.Odvodnjavanje = mapper.Map<OdvodnjavanjeDto>(odvodnjavanjeRepository.GetOdvodnjavanjeById(oldParcela.OdvodnjavanjeID));
                parcelaDto.ZasticenaZona = mapper.Map<ZasticenaZonaDto>(zasticenaZonaRepository.GetZasticenaZonaById(oldParcela.ZasticenaZonaID));
                parcelaDto.Kupac = kupacService.GetKupacByIdAsync(oldParcela.KupacID).Result;
                parcelaDto.Opstina = katastarskaOpstinaService.GetKatastarskaOpstinaByIdAsync(oldParcela.KatastarskaOpstinaID).Result;
                logDto.Level = "Info";
                loggerService.CreateLog(logDto);
                return Ok(parcelaDto);
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
