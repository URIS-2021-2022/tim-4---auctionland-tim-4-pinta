using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Controllers
{
    [ApiController]
    [Route("api/zalba/radnja")]
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
                List<ActionBasedOnComplaint> ListOfComplaints = actionBasedOnComplaintRepository.GetActions();
                if (ListOfComplaints == null || ListOfComplaints.Count == 0)
                {
                    return NoContent();
                }
                return Ok(mapper.Map<List<ActionBasedOnComplaintDTO>>(ListOfComplaints));
            }


            [HttpGet("{radnjaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ActionBasedOnComplaintDTO> GetActionById(Guid action)
            {
                ActionBasedOnComplaint complainAggregate = actionBasedOnComplaintRepository.GetActionById(action);
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
        public ActionResult<ActionBasedOnComplaintDTO> CreateAction([FromBody] ActionBasedOnComplaintDTO complain)
            {
                try
                {
                    ActionBasedOnComplaint comp = mapper.Map<ActionBasedOnComplaint>(complain);

                    ActionBasedOnComplaint confirmation = actionBasedOnComplaintRepository.CreateAction(comp);

                    string location = linkGenerator.GetPathByAction("GetTypeOfComplaint", "TypesOfComplaints", new { Radnja_id = confirmation.Radnja_na_osnovu_zalbe_ID });
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
                    ActionBasedOnComplaint complaintModel = actionBasedOnComplaintRepository.GetActionById(action);
                    if (complaintModel == null)
                    {
                        return NotFound();
                    }
                    actionBasedOnComplaintRepository.DeleteAction(action);
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
        public ActionResult<ActionBasedOnComplaintDTO> UpdateTypeOfComplaint(ActionBasedOnComplaint action)
            {
                try
                {
                    //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                    if (actionBasedOnComplaintRepository.GetActionById(action.Radnja_na_osnovu_zalbe_ID) == null)
                    {
                        return NotFound();
                    }
                    ActionBasedOnComplaint cmp = mapper.Map<ActionBasedOnComplaint>(action);
                    ActionBasedOnComplaint complaint = actionBasedOnComplaintRepository.UpdateAction(cmp);
                    return Ok(mapper.Map<ActionBasedOnComplaintDTO>(complaint));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
                }
            }

            [HttpOptions]
            public IActionResult GetTypeOfComplaintOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
                return Ok();
            }

        }
    }



