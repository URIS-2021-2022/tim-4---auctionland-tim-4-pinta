using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    public class LogModel
    {
        public string HttpMethod { get; set; }

        public string NameOfTheService { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public LogModel(string HttpMethod, string NameOfTheService, string Level, string Message)
        {
            this.HttpMethod = HttpMethod;
            this.NameOfTheService = NameOfTheService;
            this.Level = Level;
            this.Message = Message;
        }
    }
}
