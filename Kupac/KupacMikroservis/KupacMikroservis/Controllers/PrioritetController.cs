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
    /// <summary>
    /// Sadrzi CRUD operacije za prioritete
    /// </summary>
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


        /// <summary>
        /// Vraca prioritete
        /// </summary>
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

        /// <summary>
        /// Vraca prioritet po ID
        /// </summary>
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


        /// <summary>
        /// Dodaje novi prioritet
        /// </summary>
        [HttpPost]
        public ActionResult<PrioritetDTO> CreatePrioritet([FromBody] PrioritetCreateDTO prioritet)   
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

        /// <summary>
        /// Brise prioritet
        /// </summary>
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
               
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira prioritet
        /// </summary>
        [HttpPut]
        public ActionResult<PrioritetDTO> UpdatePrioritet(PrioritetUpdateDTO prioritet)
        {
            try
            {

                var oldPrioritet = prioritetRepository.GetPrioritetById(prioritet.PrioritetId);
                if (oldPrioritet == null)
                {
                    return NotFound();
                }
                PrioritetEntity pEntity = mapper.Map<PrioritetEntity>(prioritet);

                mapper.Map(pEntity, oldPrioritet);

                prioritetRepository.SaveChanges();
                return Ok(mapper.Map<PrioritetDTO>(pEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca HTTP opcije
        /// </summary>
        [HttpOptions]
        public IActionResult GetPrioritetOptions()
       {
           Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
           return Ok();
       }
    }
}
