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
    // Omogucava dodavanje dodatnih stvari, npr. status kodova
    [ApiController]
    [Route("api/prioritet")]
    public class PrioritetController : ControllerBase
    {
        private readonly IPrioritetRepository prioritetRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public PrioritetController(IPrioritetRepository prioritetRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.prioritetRepository = prioritetRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<PrioritetDTO>> GetPrioriteti()
        {
            List<PrioritetEntity> prioriteti = prioritetRepository.GetPrioriteti();
            if (prioriteti == null || prioriteti.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PrioritetDTO>>(prioriteti));
        }

        [HttpGet("{PrioritetId}")]
        public ActionResult<PrioritetDTO> GetPrioritet(Guid PrioritetId)
        {
            PrioritetEntity prioritetModel = prioritetRepository.GetPrioritetById(PrioritetId);
            if (prioritetModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PrioritetDTO>(prioritetModel));
        }

        [HttpPost]
        public ActionResult<PrioritetDTO> CreatePrioritet([FromBody] PrioritetCreateDTO prioritet)    //confirmation implementirati
        {
           try
            {
                PrioritetEntity pr = mapper.Map<PrioritetEntity>(prioritet);

                PrioritetEntity prCreated = prioritetRepository.CreatePrioritet(pr);

                string location = linkGenerator.GetPathByAction("GetPrioritet", "Prioritet", new { PrioritetId = pr.PrioritetId });
                return Created(location, mapper.Map<PrioritetDTO>(prCreated));
           }
           catch
           {
               return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

           }
               
        } 

        [HttpDelete("{PrioritetId}")]
        public IActionResult DeletePrioritet(Guid prioritetID)
        {
            try
            {
                PrioritetEntity prioritetModel = prioritetRepository.GetPrioritetById(prioritetID);
                if (prioritetModel == null)
                {
                    return NotFound();
                }
                prioritetRepository.DeletePrioritet(prioritetID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<PrioritetDTO> UpdatePrioritet(PrioritetUpdateDTO prioritet)
        {
            try
            {
              
                if (prioritetRepository.GetPrioritetById(prioritet.PrioritetId) == null)
                {
                    return NotFound(); 
                }
                PrioritetEntity prEntity = mapper.Map<PrioritetEntity>(prioritet);
                PrioritetEntity prUpdated = prioritetRepository.CreatePrioritet(prEntity);

                return Ok(mapper.Map<PrioritetDTO>(prUpdated));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetPrioritetOptions()
       {
           Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
           return Ok();
       }
    }
}
