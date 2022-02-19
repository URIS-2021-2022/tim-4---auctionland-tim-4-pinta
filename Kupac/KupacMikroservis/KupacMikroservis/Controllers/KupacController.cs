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

namespace KupacMikroservis.Controllers
{
    /// <summary>
    /// Sadrzi CRUD operacije za kupce
    /// </summary>
    [ApiController]
    [Route("api/kupac")]
    public class KupacController : ControllerBase
    {
        // private readonly IKupacRepository kupacRepository;
        private readonly IPravnoLiceRepository pLiceRepository;
        private readonly IFizickoLiceRepository fLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        private readonly IAdresaService adresaService;
        private readonly IUplataService uplataService;

        public KupacController(IPravnoLiceRepository pLiceRepository, IFizickoLiceRepository fLiceRepository, IAdresaService adresaService, IUplataService uplataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.pLiceRepository = pLiceRepository;
            this.fLiceRepository = fLiceRepository;
            this.adresaService = adresaService;
            this.uplataService = uplataService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraca kupce
        /// </summary>
        [HttpGet]
        public ActionResult<List<KupacDTO>> GetKupci()
        {

            List<PravnoLiceEntity> plica = pLiceRepository.GetPravnaLica();
            List<FizickoLiceEntity> flica = fLiceRepository.GetFizickaLica();

            List<KupacEntity> kupci = plica.ConvertAll(x => (KupacEntity)x);

            List<KupacEntity> kupciFizLica = flica.ConvertAll(x => (KupacEntity)x);

            kupci.AddRange(kupciFizLica);

            foreach (var item in kupci) Console.WriteLine(item);

            if (kupci == null || kupci.Count == 0)
            {
                return NoContent();
            }

            List<KupacDTO> kupciDtos = new List<KupacDTO>();

            foreach(KupacEntity k in kupci)
            {
                AdresaKupcaDTO adresa = adresaService.GetAdresaKupcaAsync(k.AdresaID).Result;
                UplataKupcaDTO uplata = uplataService.GetUplataKupcaAsync(k.UplataID).Result;
                KupacDTO kupacDto = mapper.Map<KupacDTO>(k);
                kupacDto.Adresa = adresa;
                kupacDto.Uplata = uplata;
                kupciDtos.Add(kupacDto);
            }


            return Ok(kupciDtos);
        }

        /// <summary>
        /// Vraca kupca po ID
        /// </summary>
        [HttpGet("{KupacId}")]
        public ActionResult<KupacDTO> GetKupac(Guid kupacID)
        {

            KupacEntity kupacModel;

            kupacModel = (KupacEntity)fLiceRepository.GetFizickoLiceById(kupacID);

            if (kupacModel == null)
            {
                kupacModel = (KupacEntity)pLiceRepository.GetPravnoLiceById(kupacID);
            }

            if (kupacModel == null)
            {
                return NotFound();
                
            }

            AdresaKupcaDTO adresa = adresaService.GetAdresaKupcaAsync(kupacModel.AdresaID).Result;
            UplataKupcaDTO uplata = uplataService.GetUplataKupcaAsync(kupacModel.UplataID).Result;
            KupacDTO kupacDto = mapper.Map<KupacDTO>(kupacModel);
            kupacDto.Adresa = adresa;
            kupacDto.Uplata = uplata;

            return Ok(kupacDto);
        }

        /// <summary>
        /// Dodaje novog kupca
        /// </summary>
        [HttpPost]
        public ActionResult<KupacDTO> CreateKupac([FromBody] KupacCreateDTO kupac)   
        {
            try
            {
               KupacEntity kp = mapper.Map<KupacEntity>(kupac);

                KupacEntity kpCreated;

                if (kp.IsFizickoLice == true)
                {
                    kpCreated = fLiceRepository.CreateFizickoLice((FizickoLiceEntity)kp);
                }
                else
                {
                    kpCreated = pLiceRepository.CreatePravnoLice((PravnoLiceEntity)kp);
                }
            
                

                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { KupacId = kp.KupacId });
                return Created(location, mapper.Map<KupacDTO>(kpCreated));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }


        /// <summary>
        /// Brise kupca
        /// </summary>
        [HttpDelete("{KupacId}")]
        public IActionResult DeleteKupac(Guid kupacID)
        {
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

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azurira kupca
        /// </summary>
        [HttpPut]
        public ActionResult<KupacDTO> UpdateKupac(KupacUpdateDTO kupac)
        {
            try
            {
                if (kupac.IsFizickoLice == true)
                {
                    var oldFizLice = fLiceRepository.GetFizickoLiceById(kupac.KupacId);
                    if (oldFizLice == null)
                    {
                        return NotFound(); 
                    }
                    KupacEntity kpEntity = mapper.Map<KupacEntity>(kupac);

                    FizickoLiceEntity fLice = (FizickoLiceEntity)kpEntity;

                    mapper.Map(fLice, oldFizLice);             

                    fLiceRepository.SaveChanges(); 
                    return Ok(mapper.Map<KupacDTO>(kpEntity));

                }
                else
                {
                    var oldPrLice = pLiceRepository.GetPravnoLiceById(kupac.KupacId);
                    if (oldPrLice == null)
                    {
                        return NotFound();
                    }
                    KupacEntity kpEntity = mapper.Map<KupacEntity>(kupac);

                    PravnoLiceEntity pLice = (PravnoLiceEntity)kpEntity;

                    mapper.Map(pLice, oldPrLice);

                    pLiceRepository.SaveChanges();
                    return Ok(mapper.Map<KupacDTO>(kpEntity));
                }

               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Vraca HTTP opcije
        /// </summary>
        [HttpOptions]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}