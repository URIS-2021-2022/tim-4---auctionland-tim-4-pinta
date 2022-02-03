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

namespace KupacMikroservis.Controllers
{

    [ApiController]
    [Route("api/kupac")]
    public class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KupacController(IKupacRepository kupacRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kupacRepository = kupacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<KupacDTO>> GetKupci()
        {
            List<KupacEntity> kupci = kupacRepository.GetKupci();
            if (kupci == null || kupci.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KupacDTO>>(kupci));
        }

        [HttpGet("{KupacId}")]
        public ActionResult<KupacDTO> GetKupac(Guid kupacID)
        {
            KupacEntity kupacModel = kupacRepository.GetKupacById(kupacID);
            if (kupacModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KupacDTO>(kupacModel));
        }

        [HttpPost]
        public ActionResult<KupacDTO> CreateKupac([FromBody] KupacCreateDTO kupac)    //confirmation implementirati
        {
            try
            {
               KupacEntity kp = mapper.Map<KupacEntity>(kupac);

                KupacEntity kpCreated = kupacRepository.CreateKupac(kp);

                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { KupacId = kp.KupacId });
                return Created(location, mapper.Map<KupacDTO>(kpCreated));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }

        }

        [HttpDelete("{KupacId}")]
        public IActionResult DeleteKupac(Guid kupacID)
        {
            try
            {
                KupacEntity kupacModel = kupacRepository.GetKupacById(kupacID);
                if (kupacModel == null)
                {
                    return NotFound();
                }
                kupacRepository.DeleteKupac(kupacID);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<KupacDTO> UpdateKupac(KupacUpdateDTO kupac)
        {
            try
            {

                if (kupacRepository.GetKupacById(kupac.KupacId) == null)
                {
                    return NotFound();
                }
                KupacEntity kpEntity = mapper.Map<KupacEntity>(kupac);
                KupacEntity kpUpdated = kupacRepository.CreateKupac(kpEntity);

                return Ok(mapper.Map<KupacDTO>(kpUpdated));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}