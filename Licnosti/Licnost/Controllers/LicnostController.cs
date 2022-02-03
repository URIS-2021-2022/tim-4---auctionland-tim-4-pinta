using AutoMapper;
using Licnost.Data;
using Licnost.Entities;
using Licnost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Controllers
{
    [ApiController]
    [Route("api/licnosti")]
    public class LicnostController : ControllerBase
    {
        private readonly ILicnostRepository licnostRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public LicnostController(ILicnostRepository licnostRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.licnostRepository = licnostRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<LicnostDto>> GetLicnosti(string licnostIme, string licnostPrezime)
        {
            List<LicnostEntity> licnosti = licnostRepository.GetLicnosti(licnostIme, licnostPrezime);
            if (licnosti == null || licnosti.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<LicnostDto>>(licnosti));
        }

       
        [HttpGet("{licnostId}")]
        public ActionResult<LicnostDto> GetLicnost(Guid licnostId)
        {
            LicnostEntity licnost = licnostRepository.GetLicnostById(licnostId);
            if (licnost == null )
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<LicnostDto>>(licnost));
        }

        [HttpPost]
        public ActionResult<LicnostDto> CreateLicnost([FromBody] LicnostCreateDto licnost)
        {
            try
            {
                LicnostEntity licnostEntity = mapper.Map<LicnostEntity>(licnost);
                LicnostEntity licnostCreate = licnostRepository.CreateLicnost(licnostEntity);
                string location = linkGenerator.GetPathByAction("GetLicnost", "Licnost", new { licnostId = licnostCreate.LicnostId });
                return Created(location, mapper.Map<LicnostDto>(licnostCreate));

                

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        [HttpDelete("{licnostId}")]
        public IActionResult DeleteLicnost(Guid licnostId)
        {
            try
            {
                LicnostEntity licnostModel = licnostRepository.GetLicnostById(licnostId);
                if (licnostModel == null)
                {
                    return NotFound();
                }
                licnostRepository.DeleteLicnost(licnostId);
                
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        [HttpPut]
        public ActionResult<LicnostDto> UpdateLicnost(LicnostUpdateDto licnost)
        {
            try
            {
               
                if (licnostRepository.GetLicnostById(licnost.LicnostId) == null)
                {
                    return NotFound();
                }
                LicnostEntity licnostEntity = mapper.Map<LicnostEntity>(licnost);
                LicnostEntity licnostUpdate = licnostRepository.UpdateLicnost(licnostEntity);
                return Ok(mapper.Map<LicnostDto>(licnostUpdate));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetLicnostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
