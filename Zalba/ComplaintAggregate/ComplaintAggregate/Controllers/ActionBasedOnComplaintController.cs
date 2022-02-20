using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComplaintAggregate.Controllers
{
    [ApiController]
    [Route("api/radnja")]
    [Produces("application/json", "application/xml")]
    public class ActionBasedOnComplaintController:ControllerBase
    {
            private readonly IActionBasedOnComplaintRepository actionBasedOnComplaintRepository;
            private readonly LinkGenerator linkGenerator;
            private readonly IMapper mapper;


            public ActionBasedOnComplaintController(IActionBasedOnComplaintRepository actionBasedOnComplaintRepository, LinkGenerator linkGenerator, IMapper mapper)
            {
                this.actionBasedOnComplaintRepository = actionBasedOnComplaintRepository;
                this.linkGenerator = linkGenerator;
                this.mapper = mapper;
            }

            [HttpGet]
            [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<ActionBasedOnComplaintDTO>> GetActions()
            {

            var actions = actionBasedOnComplaintRepository.GetActions();
                if (actions == null || actions.Count == 0)
                {
                    return NoContent();
                }
                return Ok(mapper.Map<List<ActionBasedOnComplaintDTO>>(actions));
            }


            [HttpGet("{radnjaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> GetActionById(Guid action)
            {
                var complainAggregate = actionBasedOnComplaintRepository.GetActionById(action);
                if (complainAggregate == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<ActionBasedOnComplaintDTO>(complainAggregate));
            }

            [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> CreateAction([FromBody] ActionBasedOnComplaintDTO action)
            {
                try
                {
                    ActionBasedOnComplaint comp = mapper.Map<ActionBasedOnComplaint>(action);

                    ActionBasedOnComplaint confirmation = actionBasedOnComplaintRepository.CreateAction(comp);
                    actionBasedOnComplaintRepository.SaveChanges();

                    string location = linkGenerator.GetPathByAction("GetActionById", "ActionBasedOnComplaint", new { Radnja_id = confirmation.Radnja_na_osnovu_zalbe_ID });
                    return Created(location, mapper.Map<ActionBasedOnComplaintDTO>(confirmation));
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                }
            }

            [HttpDelete("{DradnjaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteTypeOfComplaint(Guid action)
            {
                try
                {
                    var complaintModel = actionBasedOnComplaintRepository.GetActionById(action);
                    if (complaintModel == null)
                    {
                        return NotFound();
                    }
                    actionBasedOnComplaintRepository.DeleteAction(action);
                    actionBasedOnComplaintRepository.SaveChanges();
                    // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                    return NoContent();
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
                }
            }

            [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> UpdateTypeOfComplaint(ActionBasedOnComplaintDTO action)
            {
                try
                {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var actions = actionBasedOnComplaintRepository.GetActionById(action.Radnja_na_osnovu_zalbe_ID);
                    if (actions == null)
                    {
                        return NotFound();
                    }
                    ActionBasedOnComplaint cmp = mapper.Map<ActionBasedOnComplaint>(action);
                    mapper.Map(cmp, actions);
                    actionBasedOnComplaintRepository.SaveChanges();
                    return Ok(mapper.Map<ActionBasedOnComplaintDTO>(actions));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
                }
            }

            [HttpOptions]
            [AllowAnonymous]
            public IActionResult GetActionBasedOnComplaintsOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
                return Ok();
            }

        }
    }



