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
        // private readonly IKupacRepository kupacRepository;
        private readonly IPravnoLiceRepository pLiceRepository;
        private readonly IFizickoLiceRepository fLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IAdresaService adresaService;
        private readonly IUplataService uplataService;

        private readonly ILogger logger;
        private LogDTO logDTO;
        private readonly IKorisnikSistemaService korisnikSistemaService;


        public KupacController(IPravnoLiceRepository pLiceRepository, IFizickoLiceRepository fLiceRepository, IAdresaService adresaService, IUplataService uplataService, LinkGenerator linkGenerator, IMapper mapper, ILogger logger, IKorisnikSistemaService korisnikSistemaService)
        {
            this.pLiceRepository = pLiceRepository;
            this.fLiceRepository = fLiceRepository;
            this.adresaService = adresaService;
            this.uplataService = uplataService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            logDTO = new LogDTO();
            logDTO.NameOfTheService = "Kupac";
            this.korisnikSistemaService = korisnikSistemaService;
        }


        /// <summary>
        /// Vraca kupce
        /// </summary>
        /// <returns>Lista klasa</returns>
        /// <response code = "200">Vraca listu kupaca</response>
        /// <response code = "404">Nije pronadjen nijedan kupac</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KupacDTO>> GetKupci()
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

            List<KupacEntity> kupci = plica.ConvertAll(x => (KupacEntity)x);

            List<KupacEntity> kupciFizLica = flica.ConvertAll(x => (KupacEntity)x);

            kupci.AddRange(kupciFizLica);

            foreach (var item in kupci) Console.WriteLine(item);

            if (kupci == null || kupci.Count == 0)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NoContent();
            }

            //   List<KupacDTO> kupciDtos = new List<KupacDTO>();

            List<KupacDTO> kupciDtos = mapper.Map<List<KupacDTO>>(kupci);

            /*     foreach(KupacEntity k in kupci)
                 {
                    AdresaKupcaDTO adresa = adresaService.GetAdresaKupcaAsync(k.AdresaID).Result;
                     UplataKupcaDTO uplata = uplataService.GetUplataKupcaAsync(k.UplataID).Result;
                     KupacDTO kupacDto = mapper.Map<KupacDTO>(k);
                     kupacDto.Adresa = adresa;
                     kupacDto.Uplata = uplata;
                     kupciDtos.Add(kupacDto);
                 } */

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
        [HttpGet("{KupacId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KupacDTO> GetKupac(Guid kupacID)
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

            KupacEntity kupacModel;

            kupacModel = (KupacEntity)fLiceRepository.GetFizickoLiceById(kupacID);

            if (kupacModel == null)
            {
                kupacModel = (KupacEntity)pLiceRepository.GetPravnoLiceById(kupacID);
            }

            if (kupacModel == null)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NotFound();
                
            }

            AdresaKupcaDTO adresa = adresaService.GetAdresaKupcaAsync(kupacModel.AdresaID).Result;
           UplataKupcaDTO uplata = uplataService.GetUplataKupcaAsync(kupacModel.UplataID).Result;
            KupacDTO kupacDto = mapper.Map<KupacDTO>(kupacModel);
            kupacDto.Adresa = adresa;
            kupacDto.Uplata = uplata;

            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(kupacDto);
        }

        /// <summary>
        /// Dodaje novog kupca
        /// </summary>
        /// <param name="kupac">Model kupca</param>
        /// <returns>Potvrda o kreiranom kupcu</returns>
        /// <response code = "201">Vraca kreiranog kupca</response>
        /// <response code = "500">Doslo je do greske</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KupacDTO> CreateKupac([FromBody] KupacCreateDTO kupac)   
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

                if (kp.IsFizickoLice == true)
                {
                    
                    FizickoLiceEntity flCreated = kp as FizickoLiceEntity;
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
                return Created(location, mapper.Map<KupacDTO>(kpCreated));
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
        [HttpDelete("{KupacId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }

                if (kupacModel.IsFizickoLice == true)
                {
                    fLiceRepository.DeleteFizickoLice(kupacID);
                }
                else if (kupacModel.IsFizickoLice == false)
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
        [HttpPut("{KupacId}")]
        public ActionResult<KupacDTO> UpdateKupac([FromBody]KupacUpdateDTO kupac)
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
                if (kupac.IsFizickoLice == true)
                {
                 var oldFizLice = fLiceRepository.GetFizickoLiceById(kupac.KupacId);
                
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
                

                if (oldFizLice == null)
                    {
                        logDTO.Level = "Warn";
                        logger.Log(logDTO);
                        return NotFound(); 
                    }
                    KupacEntity kpEntity = mapper.Map<KupacEntity>(kupac);

                    FizickoLiceEntity fLice = (FizickoLiceEntity)kpEntity;

                    mapper.Map(fLice, oldFizLice);             

                    fLiceRepository.SaveChanges();
                    logDTO.Level = "Info";
                    logger.Log(logDTO);
                    return Ok(mapper.Map<KupacDTO>(kpEntity));

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

                /*    KupacEntity kpEntity = mapper.Map<KupacEntity>(kupac);

                    PravnoLiceEntity pLice = new PravnoLiceEntity(kpEntity);

                mapper.Map(pLice, oldPrLice);

             
               */

                pLiceRepository.SaveChanges();
                

                    logDTO.Level = "Info";
                    logger.Log(logDTO);
                    return Ok(mapper.Map<KupacDTO>(kupac));
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