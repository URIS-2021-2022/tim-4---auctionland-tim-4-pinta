using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/obliciSvojine")]
    public class OblikSvojineController : ControllerBase
    {
        private readonly IOblikSvojineRepository oblikSvojineRepository;
        private readonly LinkGenerator linkGenerator;

        public OblikSvojineController(IOblikSvojineRepository oblikSvojineRepository, LinkGenerator linkGenerator)
        {
            this.oblikSvojineRepository = oblikSvojineRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<OblikSvojineModel>> GetObradivosti()
        {
            List<OblikSvojineModel> obliciSvojine = oblikSvojineRepository.GetObliciSvojine();
            if (obliciSvojine == null || obliciSvojine.Count == 0)
            {
                return NoContent();
            }
            return Ok(obliciSvojine);
        }

        [HttpGet("{oblikSvojineID}")]
        public ActionResult<OblikSvojineModel> GetOblikSvojine(Guid oblikSvojineID)
        {
            OblikSvojineModel oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
            if (oblikSvojine == null)
            {
                return NotFound();
            }
            return Ok(oblikSvojine);
        }

        [HttpPost]
        public ActionResult<OblikSvojineModel> CreateOblikSvojine([FromBody] OblikSvojineModel oblikSvojine)
        {
            try
            {
                OblikSvojineModel os = oblikSvojineRepository.CreateOblikSvojine(oblikSvojine);
                string location = linkGenerator.GetPathByAction("GetOblikSvojine", "OblikSvojine", new { oblikSvojineID = os.OblikSvojineID });
                return Created(location, os);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{oblikSvoijneID")]
        public IActionResult DeleteOblikSvojine(Guid oblikSvojineID)
        {
            try
            {
                OblikSvojineModel oblikSvojine = oblikSvojineRepository.GetOblikSvojineById(oblikSvojineID);
                if (oblikSvojine == null)
                {
                    return NotFound();
                }
                oblikSvojineRepository.DeleteOblikSvojine(oblikSvojineID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetOblikSvojineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
