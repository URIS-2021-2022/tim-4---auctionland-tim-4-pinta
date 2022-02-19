using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    public class DokumentDto
    {
        public string ZavodniBroj { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumDonosenjaDokumenta { get; set; }
    }
}
