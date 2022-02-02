using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;

namespace JavnoNadmetanjeAgregat.Controllers
{
    [ApiController]
    [Route("api/sluzbeniListovi")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<SluzbeniListDto>> GetSluzbeneListove()
        {
            List<SluzbeniListEntity> sluzbeniList = sluzbeniListRepository.GetSluzbeniList();
            if (sluzbeniList == null || sluzbeniList.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<SluzbeniListDto>>(sluzbeniList));
        }

        [HttpGet("{sluzbeniListID}")]
        public ActionResult<SluzbeniListDto> GetSluzbeniList(Guid sluzbeniListID)
        {
            SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListID);
            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList));
        }

        [HttpPost]
        public ActionResult<SluzbeniListDto> CreateSluzbeniList([FromBody] SluzbeniListDto sluzbeniList)
        {
            try
            {
                SluzbeniListEntity obj = mapper.Map<SluzbeniListEntity>(sluzbeniList);
                SluzbeniListEntity s = sluzbeniListRepository.CreateSluzbeniList(obj);
                string location = linkGenerator.GetPathByAction("GetSluzbeniList", "SluzbeniList", new { sluzbeniListID = s.SluzbeniListID });
                return Created(location, mapper.Map<SluzbeniListDto>(s));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{sluzbeniListID}")]
        public IActionResult DeleteSluzbeniList(Guid sluzbeniListID)
        {
            try
            {
                SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListID);
                if (sluzbeniList == null)
                {
                    return NotFound();
                }
                sluzbeniListRepository.DeleteSluzbeniList(sluzbeniListID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        public ActionResult<SluzbeniListDto> UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            try
            {
                if (sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListID) == null)
                {
                    return NotFound();
                }
                SluzbeniListEntity s = sluzbeniListRepository.UpdateSluzbeniList(sluzbeniList);
                return Ok(mapper.Map<SluzbeniListDto>(s));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
