using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Models
{
    public class LogModel
    {
       
        public string HttpMethod { get; set; }

        public string NameOfTheService { get; set; }

        //status Http zahtjeva tj.nivo
     //   public ILogger HttpLevel;

        //odredjuje tip greske
      //  public HttpContext HttpStatus;
    }
}
