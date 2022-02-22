using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    public class KursDtoUpdate
    {
        public Guid KursID { get; set; }
        public double VrednostKursa { get; set; }
        public DateTime Datum { get; set; }
        public string Valuta { get; set; }

    }
}
