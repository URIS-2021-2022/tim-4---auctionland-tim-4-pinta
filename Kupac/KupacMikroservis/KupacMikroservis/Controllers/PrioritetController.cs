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
using KupacMikroservis.ServiceCalls;

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

        private readonly ILogger logger;
        private LogDTO logDTO;


        public PrioritetController(IPrioritetRepository prioritetRepository, LinkGenerator linkGenerator, IMapper mapper,ILogger logger)
        {
            this.prioritetRepository = prioritetRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            logDTO = new LogDTO();
            logDTO.NameOfTheService = "Prioritet";
        }


        /// <summary>
        /// Vraca prioritete
        /// </summary>
        [HttpGet]
        public ActionResult<List<PrioritetDTO>> GetPrioriteti()
        {
            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje svih prioriteta";

            List<PrioritetEntity> prioriteti = prioritetRepository.GetPrioriteti();
            if (prioriteti == null || prioriteti.Count == 0)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NoContent();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<List<PrioritetDTO>>(prioriteti));
        }

        /// <summary>
        /// Vraca prioritet po ID
        /// </summary>
        [HttpGet("{PrioritetId}")]
        public ActionResult<PrioritetDTO> GetPrioritet(Guid PrioritetId)
        {
            logDTO.HttpMethod = "GET";
            logDTO.Message = "Vracanje prioriteta po ID";

            PrioritetEntity prioritetModel = prioritetRepository.GetPrioritetById(PrioritetId);
            if (prioritetModel == null)
            {
                logDTO.Level = "Warn";
                logger.Log(logDTO);
                return NotFound();
            }
            logDTO.Level = "Info";
            logger.Log(logDTO);
            return Ok(mapper.Map<PrioritetDTO>(prioritetModel));
        }


        /// <summary>
        /// Dodaje novi prioritet
        /// </summary>
        [HttpPost]
        public ActionResult<PrioritetDTO> CreatePrioritet([FromBody] PrioritetCreateDTO prioritet)   
        {
            logDTO.HttpMethod = "CREATE";
            logDTO.Message = "Dodavanje novog prioriteta";

            try
            {
                PrioritetEntity pr = mapper.Map<PrioritetEntity>(prioritet);

                PrioritetEntity prCreated = prioritetRepository.CreatePrioritet(pr);

                string location = linkGenerator.GetPathByAction("GetPrioritet", "Prioritet", new { PrioritetId = pr.PrioritetId });

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Created(location, mapper.Map<PrioritetDTO>(prCreated));
           }
           catch
           {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

           }
               
        }

        /// <summary>
        /// Brise prioritet
        /// </summary>
        [HttpDelete("{PrioritetId}")]
        public IActionResult DeletePrioritet(Guid prioritetID)
        {
            logDTO.HttpMethod = "DELETE";
            logDTO.Message = "Brisanje prioriteta";

            try
            {
                PrioritetEntity prioritetModel = prioritetRepository.GetPrioritetById(prioritetID);
                if (prioritetModel == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }
                prioritetRepository.DeletePrioritet(prioritetID);

                logDTO.Level = "Info";
                logger.Log(logDTO);
                return NoContent();
            }
            catch
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Azurira prioritet
        /// </summary>
        [HttpPut]
        public ActionResult<PrioritetDTO> UpdatePrioritet(PrioritetUpdateDTO prioritet)
        {
            logDTO.HttpMethod = "PUT";
            logDTO.Message = "Azuriranje prioriteta";

            try
            {

                var oldPrioritet = prioritetRepository.GetPrioritetById(prioritet.PrioritetId);
                if (oldPrioritet == null)
                {
                    logDTO.Level = "Warn";
                    logger.Log(logDTO);
                    return NotFound();
                }
                PrioritetEntity pEntity = mapper.Map<PrioritetEntity>(prioritet);

                mapper.Map(pEntity, oldPrioritet);

                prioritetRepository.SaveChanges();
                logDTO.Level = "Info";
                logger.Log(logDTO);
                return Ok(mapper.Map<PrioritetDTO>(pEntity));
            }
            catch (Exception)
            {
                logDTO.Level = "Error";
                logger.Log(logDTO);
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
