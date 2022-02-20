using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    public class TipGarancijeUpdateDto
    {
        public Guid TipId { get; set; }
        public string Naziv { get; set; }
    }
}
