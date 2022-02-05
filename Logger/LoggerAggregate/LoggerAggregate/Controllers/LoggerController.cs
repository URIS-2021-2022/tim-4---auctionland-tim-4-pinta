using LoggerAggregate.Interface;
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
        private readonly ILoggerService loggerService;
        private static ILogger logger=LogManager.GetCurrentClassLogger();
        
       public LoggerController(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        
        [HttpPost]
        public void PostLogger([FromBody] LogModel model)
        {
           

            logger.Info(model.HttpMethod + "," + model.NameOfTheService);
         //   string httpLevel = loggerService.CheckHttpLevel(model.HttpLevel);
           // string httpMethodIdentifier = loggerService.CheckHttpMethod(model.HttpMethodIdentifier);
           // string httpStatus = loggerService.CheckHttpStatus(model.HttpStatus);

            //if (httpLevel == "Warn level")
           // {
            //    logger.Warn(httpLevel + httpMethodIdentifier + httpStatus);

//            }
  //          if (httpLevel == "Debug level")
      //      {
    //            logger.Debug(httpLevel + "," + httpMethodIdentifier + "," + httpStatus);
        //    }
          //  if (httpLevel == "Trace level")
          //  {
           //     logger.Trace(httpLevel + "," + httpMethodIdentifier + "," + httpStatus);
          //  }
           // if (httpLevel == "Info level")
           // {
            //    logger.Info(httpLevel + "," + httpMethodIdentifier + "," + httpStatus);
           // }      
        }
    }
}
