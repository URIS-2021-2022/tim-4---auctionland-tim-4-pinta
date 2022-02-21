using AutoMapper;
using ComplaintAggregate.Data;
using ComplaintAggregate.Entities;
using ComplaintAggregate.Models;
using ComplaintAggregate.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/zalba/tip")]
    [Produces("application/json", "application/xml")]
    public class TypeOfComplaintController:ControllerBase
    {
        private readonly IFileAComplaintService fileService;
        private readonly ITypeOfComplaintRepository typeOfComplaintRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public TypeOfComplaintController(ITypeOfComplaintRepository typeOfComplaintRepository, LinkGenerator linkGenerator, IMapper mapper,IFileAComplaintService fileService)
        {
            this.typeOfComplaintRepository = typeOfComplaintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<List<TypeOfComplaintDTO>> GetTypeOfComplaints()
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            var types = typeOfComplaintRepository.GetTypesOfComplaints();
            if (types == null || types.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TypeOfComplaintDTO>>(types));
        }


        [HttpGet("{tipId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<TypeOfComplaintDTO> GetTypeOfComplaintsById(Guid Tip_id)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
            {
                return Unauthorized();
            }
            HttpStatusCode res = fileService.AuthorizeAsync(token).Result;
            if (res.ToString() != "OK")
            {
                return Unauthorized();
            }

            var complainAggregate = typeOfComplaintRepository.GetTypesOfComplaintsById(Tip_id);
            if (complainAggregate == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TypeOfComplaintDTO>(complainAggregate));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult<TypeOfComplaintDTO> CreateTypeOfComplaint([FromBody] TypeOfComplaintDTO types)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
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
                TypeOfComplaint comp = mapper.Map<TypeOfComplaint>(types);

                TypeOfComplaint confirmation = typeOfComplaintRepository.CreateTypeOfComplaint(comp);

                typeOfComplaintRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetTypeOfComplaintsById", "TypeOfComplaint", new { Tip_id = confirmation.Tip_id });
                return Created(location, mapper.Map<TypeOfComplaintDTO>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{DtipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteTypeOfComplaint(Guid Tip_id)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
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
                var complaintModel = typeOfComplaintRepository.GetTypesOfComplaintsById(Tip_id);
                if (complaintModel == null)
                {
                    return NotFound();
                }
                typeOfComplaintRepository.DeleteTypeOfComplaint(Tip_id);
                typeOfComplaintRepository.SaveChanges();
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
        public ActionResult<TypeOfComplaintDTO> UpdateTypeOfComplaint(TypeOfComplaintDTO type)
        {

            string token = Request.Headers["token"].ToString();
            string[] split = token.Split('#');
            if (split[1] != "administrator" || split[1] != "menadzer" || split[1] != "licitant"
                || split[1] != "tehnicki sektetar" || split[1] != "prva komisija" || split[1] != "operator nadmetanja")
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
                var types = typeOfComplaintRepository.GetTypesOfComplaintsById(type.Tip_id);
                if ( types == null)
                {
                    return NotFound();
                }
                TypeOfComplaint cmp = mapper.Map<TypeOfComplaint>(type);
                mapper.Map(cmp, types);
                typeOfComplaintRepository.SaveChanges();
                return Ok(mapper.Map<TypeOfComplaintDTO>(types));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetTypeOfComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
