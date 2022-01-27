using Korisnik.Data;
using Korisnik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        public KorisnikController(IKorisnikRepository korisnikRepository)
        {
            this.korisnikRepository = korisnikRepository;
           
        }



        [HttpGet]
        public ActionResult<List<KorisnikModel>> GetKorisniks()
        {
            List<KorisnikModel> korisniks = korisnikRepository.GetKorisniks();
            if (korisniks == null || korisniks.Count == 0)
            {
                return NoContent();
            }
            return Ok(korisniks);
        }

        [HttpGet("{KorisnikId}")]
        public ActionResult<KorisnikModel> GetKorisnik(int korisnikId)
        {
            KorisnikModel korisnik = korisnikRepository.GetKorisniksById(korisnikId);
            if (korisnik == null)
            {
                return NotFound();
            }
            return Ok(korisnik);
        }

        [HttpPost]
        public ActionResult<KorisnikModel> CreateKorisnik([FromBody] KorisnikModel korisnik)
        {
            try
            {
               
                KorisnikModel korisnik1 = korisnikRepository.CreateKorisniks(korisnik);
               
                return korisnik1;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{KorisnikId}")]
        public IActionResult DeleteKorisnik(int korisnikId)
        {
            try
            {
                KorisnikModel korisnik = korisnikRepository.GetKorisniksById(korisnikId);
                if (korisnik == null)
                {
                    return NotFound();
                }
                korisnikRepository.DeleteKorisnik(korisnikId);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        

    }
}
