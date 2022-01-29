using Licnost.Data;
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
        public LicnostController(ILicnostRepository licnostRepository, LinkGenerator linkGenerator)
        {
            this.licnostRepository = licnostRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<LicnostModel>> GetLicnosti(string licnostIme, string licnostPrezime)
        {
            List<LicnostModel> licnosti = licnostRepository.GetLicnosti(licnostIme, licnostPrezime);
            if (licnosti == null || licnosti.Count == 0)
            {
                return NoContent();
            }
            return Ok(licnosti);
        }

       
        [HttpGet("{licnostId}")]
        public ActionResult<LicnostModel> GetLicnost(Guid licnostId)
        {
            LicnostModel licnost = licnostRepository.GetLicnostById(licnostId);
            if (licnost == null )
            {
                return NoContent();
            }
            return Ok(licnost);
        }

        [HttpPost]
        public ActionResult<LicnostModel> CreateLicnost([FromBody] LicnostModel licnost)
        {
            try
            {
                LicnostModel licnostCreate = licnostRepository.CreateLicnost(licnost);
                string location = linkGenerator.GetPathByAction("GetLicnost", "Licnost", new { licnostId = licnostCreate.LicnostId });
                return Created(location, licnostCreate);
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
                LicnostModel licnostModel = licnostRepository.GetLicnostById(licnostId);
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







    }
}
