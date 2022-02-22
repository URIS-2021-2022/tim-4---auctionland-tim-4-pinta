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
        

        public KorisnikController(IKorisnikRepository korisnikRepository, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;

        }



        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                Console.WriteLine(ex);
                throw;

            }
        }

        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{KorisnikId}")]
        public ActionResult<KorisnikDto> GetKorisnik(int korisnikId)
        {

            try
            {

                if (korisnikRepository.Authorize(Request.Headers["token"]))
                { 
                   
                    KorisnikModel korisnik = korisnikRepository.GetKorisnikById(korisnikId);
                    if (korisnik == null)
                    {
                        return NotFound();
                    }
                    return Ok(mapper.Map<KorisnikDto>(korisnik));
                }

                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;

            }
        }

        [HttpHead]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KorisnikDto> CreateKorisnik([FromBody] KorisnikDto korisnik)
        {
            try
            {

                if (korisnikRepository.Authorize(Request.Headers["token"]))
                {

                    KorisnikModel korisnik1 = mapper.Map<KorisnikModel>(korisnik);
                    Random rand = new Random();
                    korisnik1.KorisnikId = rand.Next();
                    KorisnikModel korisnik2 = korisnikRepository.CreateKorisnik(korisnik1);
                    korisnikRepository.SaveChanges();
                    return Ok(mapper.Map<KorisnikDto>(korisnik2));
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{KorisnikId}")]
        public IActionResult DeleteKorisnik(int korisnikId)
        {
            try
            {

                if (korisnikRepository.Authorize(Request.Headers["token"]))
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
                else
                {
                    return Unauthorized();
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpHead]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KorisnikDto> UpdateKorisnik(KorisnikModel korisnik)
        {
            try
            {

                if (korisnikRepository.Authorize(Request.Headers["token"]))
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

                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("authorize/{token}")]
        public ActionResult Authorize(string token)
        {

           if(korisnikRepository.Authorize(token))
           {
                return Ok();
               
           }

            return Unauthorized();
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
