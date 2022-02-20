using AutoMapper;
using Korisnik.Data;
using Korisnik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")] 
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public KorisnikController(IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;

        }



        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KorisnikDto>> GetKorisniks()
        {

            try
            {

                if (korisnikRepository.Authorize(Request.Headers["token"]))
                {
                    List<KorisnikModel> korisniks = korisnikRepository.GetKorisniks();
                    if (korisniks == null || korisniks.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(mapper.Map<List<KorisnikDto>>(korisniks));

                }

                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{KorisnikId}")]
        public ActionResult<KorisnikDto> GetKorisnik(int korisnikId)
        {
            KorisnikModel korisnik = korisnikRepository.GetKorisnikById(korisnikId);
            if (korisnik == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KorisnikDto>(korisnik));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikDto> CreateKorisnik([FromBody] KorisnikDto korisnik)
        {
            try
            {

                KorisnikModel korisnik1 = mapper.Map<KorisnikModel>(korisnik);
                Random rand = new Random();
                korisnik1.KorisnikId = rand.Next();
                KorisnikModel korisnik2= korisnikRepository.CreateKorisnik(korisnik1);
                korisnikRepository.SaveChanges();
                return Ok(mapper.Map<KorisnikDto>(korisnik2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{KorisnikId}")]
        public IActionResult DeleteKorisnik(int korisnikId)
        {
            try
            {
                KorisnikModel korisnik = korisnikRepository.GetKorisnikById(korisnikId);
                if (korisnik == null)
                {
                    return NotFound();
                }
                korisnikRepository.DeleteKorisnik(korisnikId);
                korisnikRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikDto> UpdateKorisnik(KorisnikModel korisnik)
        {
            try
            {
                KorisnikModel oldKorisnik = korisnikRepository.GetKorisnikById(korisnik.KorisnikId);
                if (oldKorisnik == null)
                {
                    return NotFound(); 
                }
              
                mapper.Map(korisnik, oldKorisnik);
                korisnikRepository.SaveChanges();
                return Ok(mapper.Map<KorisnikDto>(oldKorisnik));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
        
        
    }
}
