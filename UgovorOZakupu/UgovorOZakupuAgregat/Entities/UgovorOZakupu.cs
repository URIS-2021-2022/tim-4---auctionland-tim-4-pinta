using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    /// <summary>
    /// Predstavlja model entiteta ugovor o zakupu
    /// </summary>
    public class UgovorOZakupu
    {
        /// <summary>
        /// ID ugovora o zakupu
        /// </summary>
        [Key]
        public Guid UgovorId { get; set; }


        /// <summary>
        /// ID odluke (dokumenta) ugovora
        /// </summary>
        [ForeignKey("Dokument")]
        public Guid DokumentId { get; set; }
        /// <summary>
        /// Model dokumenta
        /// </summary>
        public Dokument Dokument { get; set; }

        /// <summary>
        /// ID tipa garancije ugovora
        /// </summary>
        [ForeignKey("TipGarancije")]
        public Guid TipId { get; set; }
        /// <summary>
        /// Model tipa garancije
        /// </summary>
        public TipGarancije TipGarancije { get; set; }

        /// <summary>
        /// Zavodni broj ugovora
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum zavodjenja ugovora
        /// </summary>
        public DateTime DatumZavodjenja { get; set; }

        /// <summary>
        /// Rok za vraćanje zemljišta
        /// </summary>
        public DateTime RokZaVracanjeZemljista { get; set; }

        /// <summary>
        /// Mesto potpisivanja ugovora
        /// </summary>
        public string MestoPotpisivanja { get; set; }

        /// <summary>
        /// Datum potpisa ugovora
        /// </summary>
        public DateTime DatumPotpisa { get; set; }

        /// <summary>
        ///  ID ministra (ličnost) ugovora
        /// </summary>
        public Guid LicnostId { get; set; }

        /// <summary>
        /// ID lica (kupac) ugovora
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        ///ID javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }


    }
}
