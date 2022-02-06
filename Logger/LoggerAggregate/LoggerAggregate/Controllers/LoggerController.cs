using LoggerAggregate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerAggregate.Controllers
{
    [ApiController]
    [Route("api/logger")]
    public class LoggerController :ControllerBase
    {
        private static readonly ILogger logger=LogManager.GetCurrentClassLogger();
        
       public LoggerController()
        {
             
        }

        
        [HttpPost]
        public void PostLogger([FromBody] LogModel model)
        {
            
            if (model.Level == "Info")
            {
                logger.Info("Naziv metode: " + model.HttpMethod + "," + "Naziv servisa: " + model.NameOfTheService + ",poruka: " + model.Message);
            }
            else if (model.Level == "Warn")
            {
                logger.Warn("Naziv metode: " + model.HttpMethod + "," + "Naziv servisa: " + model.NameOfTheService + ",poruka: " + model.Message);
            }         
             else  
               logger.Error("Naziv metode: " + model.HttpMethod + "," + "Naziv servisa: " + model.NameOfTheService + ",poruka: " + model.Message);
                       
        }
    }
}
