using JavnoNadmetanjeAgregat.Data;
using JavnoNadmetanjeAgregat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Controllers
{
    [ApiController]
    [Route("api/sluzbeniListovi")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator linkGenerator)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<SluzbeniListModel>> GetSluzbeneListove()
        {
            List<SluzbeniListModel> sluzbeniList = sluzbeniListRepository.GetSluzbeniList();
            if (sluzbeniList == null || sluzbeniList.Count == 0)
            {
                return NoContent();
            }
            return Ok(sluzbeniList);
        }

        [HttpGet("{sluzbeniListID}")]
        public ActionResult<SluzbeniListModel> GetSluzbeniList(Guid sluzbeniListID)
        {
            SluzbeniListModel sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListID);
            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(sluzbeniList);
        }

        [HttpPost]
        public ActionResult<SluzbeniListModel> CreateSluzbeniList([FromBody] SluzbeniListModel sluzbeniList)
        {
            try
            {
                SluzbeniListModel s = sluzbeniListRepository.CreateSluzbeniList(sluzbeniList);
                string location = linkGenerator.GetPathByAction("GetSluzbeniList", "SluzbeniList", new { sluzbeniListID = s.SluzbeniListID });
                return Created(location, s);
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
                SluzbeniListModel sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListID);
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


    }
}
