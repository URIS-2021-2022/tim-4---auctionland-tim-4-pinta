using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KupacMikroservis.Data;
using KupacMikroservis.Models;
using AutoMapper;
using KupacMikroservis.ServiceCalls;
using System.Net;

namespace KupacMikroservis.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za kupce
    /// </summary>
    [ApiController]
    [Route("api/kupac")]
    [Produces("application/json", "application/xml")]
    public class KupacController : ControllerBase
    {
        
        private readonly IPravnoLiceRepository pLiceRepository;
        private readonly IFizickoLiceRepository fLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IAdresaService adresaService;
        private readonly IUplataService uplataService;

        private readonly IPrioritetRepository prRepository;
        private readonly IOvlascenoLiceRepository olRepository;

        private readonly ILogger logger;
        private readonly LogDto logDTO;
        private readonly IKorisnikSistemaService korisnikSistemaService;


        public KupacController(IPravnoLiceRepository pLiceRepository, IFizickoLiceRepository fLiceRepository, IAdresaService adresaService, IUplataService uplataService, LinkGenerator linkGenerator, IMapper mapper, ILogger logger, IKorisnikSistemaService korisnikSistemaService, IPrioritetRepository prRepository, IOvlascenoLiceRepository olRepository)

        {
            this.pLiceRepository = pLiceRepository;
            this.fLiceRepository = fLiceRepository;
            this.adresaService = adresaService;
            this.uplataService = uplataService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            logDTO = new LogDto();
            logDTO.NameOfTheService = "Kupac";
            this.korisnikSistemaService = korisnikSistemaService;
            this.prRepository = prRepository;
            this.olRepository = olRepository;
        }


        /// <summary>
        /// Vraca kupce
        /// </summary>
        /// <returns>Lista klasa</returns>
        /// <response code = "200">Vraca listu kupaca</response>
        /// <response code = "404">Nije pronadjen nijedan kupac</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<KupacDto>> GetKupci()
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser" && split[1] != "operaternadmetanja" && split[1] != "menadzer")
            {
                return Unauthorized();
            } 

            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje svih kupaca";

            List<PravnoLiceEntity> plica = pLiceRepository.GetPravnaLica();
            List<FizickoLiceEntity> flica = fLiceRepository.GetFizickaLica();

            List<KupacEntity> kupciPrLica = plica.ConvertAll(x => (KupacEntity)x);

            List<KupacEntity> kupciFizLica = flica.ConvertAll(x => (KupacEntity)x);

         

            if ((kupciPrLica.Count == 0) && (kupciFizLica.Count == 0))
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NoContent();
            }

               List<KupacDto> kupciDtos = new List<KupacDto>();

                 foreach(FizickoLiceEntity f in kupciFizLica)
                 {
                    AdresaKupcaDto adresa = adresaService.GetAdresaKupcaAsync(f.AdresaID,token).Result;
                    UplataKupcaDto uplata = uplataService.GetUplataKupcaAsync(f.UplataID,token).Result;
                    PrioritetEntity prioritetKupca = prRepository.GetPrioritetById(f.Prioritet);
                    OvlascenoLiceEntity olKupca = olRepository.GetOvlascenoLiceById(f.OvlascenoLice);
                
                    KupacDto kupacDto = mapper.Map<KupacDto>(f);
                    kupacDto.Adresa = adresa;
                    kupacDto.Uplata = uplata;
                    kupacDto.OvlascenoLiceO = olKupca;
                    kupacDto.PrioritetO = prioritetKupca;

                     kupciDtos.Add(kupacDto);
                 }

            foreach (PravnoLiceEntity p in kupciPrLica)
            {
                AdresaKupcaDto adresa = adresaService.GetAdresaKupcaAsync(p.AdresaID,token).Result;
                UplataKupcaDto uplata = uplataService.GetUplataKupcaAsync(p.UplataID,token).Result;
                PrioritetEntity prioritetKupca = prRepository.GetPrioritetById(p.Prioritet);
                OvlascenoLiceEntity olKupca = olRepository.GetOvlascenoLiceById(p.OvlascenoLice);

                KupacDto kupacDto = mapper.Map<KupacDto>(p);
                kupacDto.Adresa = adresa;
                kupacDto.Uplata = uplata;
                kupacDto.OvlascenoLiceO = olKupca;
                kupacDto.PrioritetO = prioritetKupca;

                kupciDtos.Add(kupacDto);
            }

            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(kupciDtos);
        }

        /// <summary>
        /// Vraca kupca po ID
        /// </summary>
        /// /// <param name="KupacId">ID kupca</param>
        /// <returns>Trazeni kupac</returns>
        /// <response code = "200">Vraca trazenog kupca</response>
        /// <response code = "404">Trazeni kupac nije pronadjen</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        [HttpGet("{KupacId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KupacDto> GetKupac(Guid kupacID)
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser" && split[1] != "operaternadmetanja" && split[1] != "menadzer")
            {
                return Unauthorized();
            } 

            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje kupca po ID";

        if(fLiceRepository.GetFizickoLiceById(kupacID) is null && pLiceRepository.GetPravnoLiceById(kupacID) is null)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NotFound();

            }

            if (fLiceRepository.GetFizickoLiceById(kupacID) is null)
            {
                PravnoLiceEntity pLice = pLiceRepository.GetPravnoLiceById(kupacID);
                AdresaKupcaDto adresa = adresaService.GetAdresaKupcaAsync(pLice.AdresaID,token).Result;
                //UplataKupcaDto uplata = uplataService.GetUplataKupcaAsync(pLice.UplataID,token).Result;
                KupacDto kupacDto = mapper.Map<KupacDto>(pLice);
                kupacDto.Adresa = adresa;
                //kupacDto.Uplata = uplata;

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Ok(kupacDto);
            }
            else
            {
                FizickoLiceEntity fLice = fLiceRepository.GetFizickoLiceById(kupacID);
                AdresaKupcaDto adresa = adresaService.GetAdresaKupcaAsync(fLice.AdresaID,token).Result;
                //UplataKupcaDto uplata = uplataService.GetUplataKupcaAsync(fLice.UplataID,token).Result;
                KupacDto kupacDto = mapper.Map<KupacDto>(fLice);
                kupacDto.Adresa = adresa;
                //kupacDto.Uplata = uplata;

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Ok(kupacDto);
            }

        }

        /// <summary>
        /// Dodaje novog kupca
        /// </summary>
        /// <param name="kupac">Model kupca</param>
        /// <returns>Potvrda o kreiranom kupcu</returns>
        /// <response code = "201">Vraca kreiranog kupca</response>
        /// <response code = "500">Doslo je do greske</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KupacDto> CreateKupac([FromBody] KupacCreateDto kupac)   
        {

            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser" && split[1] != "operaternadmetanja")
            {
                return Unauthorized();
            } 

            logDTO.HttpMethod = "POST";
            logDTO.Message = "Dodavanje novog kupca";

            try
            {
               KupacEntity kp = mapper.Map<KupacEntity>(kupac);

                KupacEntity kpCreated;

                if (kp.IsFizickoLice)
                {
                    
                    FizickoLiceEntity flCreated = new FizickoLiceEntity(kp);
                    kpCreated = fLiceRepository.CreateFizickoLice(flCreated);
                }
                else
                {
                    PravnoLiceEntity plCreated = new PravnoLiceEntity(kp);
                    kpCreated = pLiceRepository.CreatePravnoLice(plCreated);
                }
            
                

                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { KupacId = kp.KupacId });

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Created(location, mapper.Map<KupacDto>(kpCreated));
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }
        
        }


        /// <summary>
        /// Brise kupca
        /// </summary>
        /// <param name="KupacId">ID kupca</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kupac uspesno obrisana</response>
        /// <response code="404">Nije pronadjen kupac</response>
        /// <response code="500">Doslo je do greske</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        [HttpDelete("{KupacId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteKupac(Guid kupacID)
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }
            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser" && split[1] != "operaternadmetanja")
            {
                return Unauthorized();
            } 

            logDTO.HttpMethod = "DELETE";
            logDTO.Message = "Brisanje kupca";

            KupacEntity kupacModel;


            try
            {
                kupacModel = pLiceRepository.GetPravnoLiceById(kupacID);

                
                if (kupacModel == null)
                {
                    kupacModel = fLiceRepository.GetFizickoLiceById(kupacID);
                }

                if (kupacModel == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }

                if (kupacModel.IsFizickoLice)
                {
                    fLiceRepository.DeleteFizickoLice(kupacID);
                }
                else if (!kupacModel.IsFizickoLice)
                {
                    pLiceRepository.DeletePravnoLice(kupacID);
                }
                logDTO.Level = "Info";
                logger.Log(logDTO);
                return NoContent();
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azurira kupca
        /// </summary>
        /// <param name="kupac">Model kupca za azuriranje</param>
        /// <returns>Potvrda o modifikovanom kupcu</returns>
        /// <response code="200">Vraca azuriranog kupca</response>
        /// <response code="400">Kupac nije pronadjen</response>
        /// <response code="500">Doslo je do greske</response>
        /// <response code="401">Korisnik nije autorizovan</response>
        [HttpPut("{KupacId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KupacDto> UpdateKupac([FromBody]KupacUpdateDto kupac)
        {
            string token = Request.Headers["token"].ToString();
            HttpStatusCode res = korisnikSistemaService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            string[] split = token.Split('#');
            if (split[1] != "administrator" && split[1] != "superuser" && split[1] != "operaternadmetanja")
            {
                return Unauthorized();
            } 

            logDTO.HttpMethod = "PUT";
            logDTO.Message = "Azuriranje kupca";

            try
            {
                if (kupac.IsFizickoLice)
                {
                 var oldFizLice = fLiceRepository.GetFizickoLiceById(kupac.KupacId);

                    if (oldFizLice == null)
                    {
                        logDTO.Level = "Warn";
                        logger.Log(logDTO);
                        return NotFound();
                    }


                oldFizLice.KupacId = kupac.KupacId;
                oldFizLice.Naziv = kupac.Naziv;
                oldFizLice.BrojRacuna = kupac.BrojRacuna;
                oldFizLice.BrojTelefona1 = kupac.BrojTelefona1;
                oldFizLice.BrojTelefona2 = kupac.BrojTelefona2;
                oldFizLice.Email = kupac.Email;
                oldFizLice.AdresaID = kupac.AdresaID;
                oldFizLice.UplataID = kupac.UplataID;
                oldFizLice.Prioritet = kupac.Prioritet;
                oldFizLice.DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
                oldFizLice.DuzinaTrajanjaZabraneUGodinama = kupac.DuzinaTrajanjaZabraneUGodinama;
                oldFizLice.DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
                oldFizLice.IsFizickoLice = kupac.IsFizickoLice;
                
           

                    fLiceRepository.SaveChanges();

                    logDTO.Level = "Info";
                    logger.Log(logDTO);
                    return Ok();

                }
                else
                {
                    var oldPrLice = pLiceRepository.GetPravnoLiceById(kupac.KupacId);

               

                if (oldPrLice == null)
                    {
                        logDTO.Level = "Warn";
                        logger.Log(logDTO);
                        return NotFound();
                    }

                oldPrLice.KupacId = kupac.KupacId;
                oldPrLice.Naziv = kupac.Naziv;
                oldPrLice.BrojRacuna = kupac.BrojRacuna;
                oldPrLice.BrojTelefona1 = kupac.BrojTelefona1;
                oldPrLice.BrojTelefona2 = kupac.BrojTelefona2;
                oldPrLice.Email = kupac.Email;
                oldPrLice.AdresaID = kupac.AdresaID;
                oldPrLice.UplataID = kupac.UplataID;
                oldPrLice.Prioritet = kupac.Prioritet;
                oldPrLice.DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
                oldPrLice.DuzinaTrajanjaZabraneUGodinama = kupac.DuzinaTrajanjaZabraneUGodinama;
                oldPrLice.DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
                oldPrLice.IsFizickoLice = kupac.IsFizickoLice;

              

                pLiceRepository.SaveChanges();
                

                    logDTO.Level = "Info";
                    logger.Log(logDTO);
                    return Ok(mapper.Map<KupacDto>(kupac));
                }

               
            }
            catch (Exception)
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Vraca HTTP opcije
        /// </summary>
        [HttpOptions]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}