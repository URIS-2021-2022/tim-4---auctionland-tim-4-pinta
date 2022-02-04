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
    [Authorize]
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
            List<KorisnikModel> korisniks = korisnikRepository.GetKorisniks();
            if (korisniks == null || korisniks.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KorisnikDto>>(korisniks));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{KorisnikId}")]
        public ActionResult<KorisnikDto> GetKorisnik(int korisnikId)
        {
            KorisnikModel korisnik = korisnikRepository.GetKorisniksById(korisnikId);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /*
        [HttpPut]
        public ActionResult<ExamRegistrationConfirmationDto> UpdateExamRegistration(ExamRegistrationUpdateDto examRegistration)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (examRegistrationRepository.GetExamRegistrationById(examRegistration.ExamRegistrationId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ExamRegistration examRegistrationEntity = mapper.Map<ExamRegistration>(examRegistration);
                ExamRegistrationConfirmation confirmation = examRegistrationRepository.UpdateExamRegistration(examRegistrationEntity);
                return Ok(mapper.Map<ExamRegistrationConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        */
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
        /*
        // Validira da student ne moze da prijavi ispit u visoj godini nego sto je prijavljen
        private bool ValidateExamRegistration(ExamRegistrationCreationDto examRegistration)
        {
            if (examRegistration.StudentEnrolledYear < examRegistration.SubjectTerm)
            {
                return false;
            }
            if (examRegistration.StudentEnrolledYear == examRegistration.SubjectTerm && examRegistration.StudentCurrentSemester < examRegistration.SubjectSemester)
            {
                return false;
            }
            return true;
        }
        */
        
    }
}
