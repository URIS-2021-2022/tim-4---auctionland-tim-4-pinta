using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KupacMikroservis.Data;
using KupacMikroservis.Models;

namespace KupacMikroservis.Controllers
{

    [ApiController]
    [Route("api/fizickolice")]
    public class FizickoLiceController : ControllerBase
    {
        private readonly IFizickoLiceRepository fizLiceRepository;
        private readonly LinkGenerator linkGenerator;


        public FizickoLiceController(IFizickoLiceRepository fizLiceRepository, LinkGenerator linkGenerator)
        {
            this.fizLiceRepository = fizLiceRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<FizickoLiceModel>> GetFizickaLica()
        {
            List<FizickoLiceModel> fizLica = fizLiceRepository.GetFizickaLica();
            if (fizLica == null || fizLica.Count == 0)
            {
                return NoContent();
            }
            return Ok(fizLica);
        }

        [HttpGet("{FizickoLiceId}")]
        public ActionResult<FizickoLiceModel> GetFizLice(Guid fizLiceID)
        {
            FizickoLiceModel fizLiceModel = fizLiceRepository.GetFizickoLiceById(fizLiceID);
            if (fizLiceModel == null)
            {
                return NotFound();
            }
            return Ok(fizLiceModel);
        }

        [HttpPost]
        public ActionResult<FizickoLiceModel> CreateFizickoLice([FromBody] FizickoLiceModel fLice)    //confirmation implementirati
        {
            try
            {
                FizickoLiceModel fl = fizLiceRepository.CreateFizickoLice(fLice);

                string location = linkGenerator.GetPathByAction("GetFizickoLice", "FizickoLice", new { KupacId = fl.KupacId });
                return Created(location, fl);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        [HttpDelete("{FizickoLiceId}")]
        public IActionResult DeleteFizickoLice(Guid fizLiceID)
        {
            try
            {
                FizickoLiceModel fizLiceModel = fizLiceRepository.GetFizickoLiceById(fizLiceID);
                if (fizLiceModel == null)
                {
                    return NotFound();
                }
                fizLiceRepository.DeleteFizickoLice(fizLiceID);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


    }
}