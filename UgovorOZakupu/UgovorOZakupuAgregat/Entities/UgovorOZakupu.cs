using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    public class UgovorOZakupu
    {
        [Key]
        public Guid UgovorId { get; set; }


        [ForeignKey("Dokument")]
        public Guid DokumentId { get; set; }
        public Dokument Dokument { get; set; }


        // public Guid LicnostIme { get; set; }
        [ForeignKey("TipGarancije")]
        public Guid TipId { get; set; }
        public TipGarancije TipGarancije { get; set; }

        public string ZavodniBroj { get; set; }
        public DateTime DatumZavodjenja { get; set; }
        public DateTime RokZaVracanjeZemljista { get; set; }
        public string MestoPotpisivanja { get; set; }
        public DateTime DatumPotpisa { get; set; }
    }
}
