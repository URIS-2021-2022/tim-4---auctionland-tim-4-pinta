using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    public class UgovorOZakupuUpdateDto
    {
        public Guid UgovorId { get; set; }
        public Guid DokumentId { get; set; }
        public Guid TipId { get; set; }

        public string ZavodniBroj { get; set; }
        public DateTime DatumZavodjenja { get; set; }
        public DateTime RokZaVracanjeZemljista { get; set; }
        public string MestoPotpisivanja { get; set; }
        public DateTime DatumPotpisa { get; set; }
    }
}
