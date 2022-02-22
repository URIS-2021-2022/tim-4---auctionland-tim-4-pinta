using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using ComplaintAggregate.ServiceCalls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComplaintAggregate.Controllers
{
    [ApiController]
    [Route("api/status")]
    [Produces("application/json", "application/xml")]
    public class StatusOfComplaintController:ControllerBase
    {
        private readonly IFileAComplaintService fileService;
        private readonly IStatusOfComplaintRepository complainStatusRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public StatusOfComplaintController(IStatusOfComplaintRepository complainStatusRepository, LinkGenerator linkGenerator, IMapper mapper,IFileAComplaintService fileService)
        {
            this.complainStatusRepository = complainStatusRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<StatusOfComplaintDTO>> GetStatus()
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[0] != "administrator" || split[0] != "menadzer" || split[0] != "licitant"
                || split[0] != "tehnicki sektetar" || split[0] != "prva komisija" || split[0] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            var status = complainStatusRepository.GetStatus();
            if (status == null || status.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatusOfComplaintDTO>>(status));
        }


        [HttpGet("{statusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<StatusOfComplaintDTO> GetStatusById(Guid Status_zalbe)
        {
            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[0] != "administrator" || split[0] != "menadzer" || split[0] != "licitant"
                || split[0] != "tehnicki sektetar" || split[0] != "prva komisija" || split[0] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            var complainAggregate = complainStatusRepository.GetStatusById(Status_zalbe);
            if (complainAggregate == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatusOfComplaintDTO>(complainAggregate));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<StatusOfComplaintDTO> CreateStatus([FromBody] StatusOfComplaintDTO complain)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[0] != "administrator" || split[0] != "menadzer" || split[0] != "licitant"
                || split[0] != "tehnicki sektetar" || split[0] != "prva komisija" || split[0] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            try
            {
                StatusOfComplaint comp = mapper.Map<StatusOfComplaint>(complain);

                StatusOfComplaint confirmation = complainStatusRepository.CreateStatus(comp);
                complainStatusRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
              string location = linkGenerator.GetPathByAction("GetStatus", "StatusOfComplaint", new { Status_zalbe = confirmation.Status_zalbe });
                return Created(location, mapper.Map<StatusOfComplaintDTO>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DStatusId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteStatus(Guid Status_zalbe)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[0] != "administrator" || split[0] != "menadzer" || split[0] != "licitant"
                || split[0] != "tehnicki sektetar" || split[0] != "prva komisija" || split[0] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            try
            {
                var complaintModel = complainStatusRepository.GetStatusById(Status_zalbe);
                if (complaintModel == null)
                {
                    return NotFound();
                }
                complainStatusRepository.DeleteStatus(Status_zalbe);
                complainStatusRepository.SaveChanges();
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
        public ActionResult<StatusOfComplaintDTO> UpdateStatus(StatusOfComplaintDTO status)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[0] != "administrator" || split[0] != "menadzer" || split[0] != "licitant"
                || split[0] != "tehnicki sektetar" || split[0] != "prva komisija" || split[0] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var updateStatus = complainStatusRepository.GetStatusById(status.Status_zalbe);
                if ( updateStatus== null)
                {
                    return NotFound();
                }
                StatusOfComplaint cmp = mapper.Map<StatusOfComplaint>(status);
                mapper.Map(cmp, updateStatus); //update objekta nad kojim su izvrsene promjene
                complainStatusRepository.SaveChanges();
                return Ok(mapper.Map<StatusOfComplaintDTO>(updateStatus));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
