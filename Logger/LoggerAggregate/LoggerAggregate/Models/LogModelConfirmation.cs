using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerAggregate.Models
{
    public class LogModelConfirmation
    {
       

        //odredjuje koja je HTTP metoda u pitanju
        public string HttpMethodIdentifier;

        //status Http zahtjeva
        public string HttpLevel;

        //odredjuje tip greske
        public string Error;
    }
}
