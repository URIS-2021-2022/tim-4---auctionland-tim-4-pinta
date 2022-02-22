using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// Model ugovora o zakupu
    /// </summary>
    public class UgovorOZakupuDto
    {
       /// <summary>
       /// ID dokumenta
       /// </summary>
        public Guid DokumentId { get; set; }

        /// <summary>
        /// Odluka (dokument)
        /// </summary>
        public DokumentDto Dokument { get; set; }

        /// <summary>
        /// ID tipa garancije
        /// </summary>
        public Guid TipId { get; set; }
        /// <summary>
        /// Tip garancije
        /// </summary>
        public TipGarancijeDto TipGarancije { get; set; }

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
        /// Datum potpisivanja ugovora
        /// </summary>
        public DateTime DatumPotpisa { get; set; }

        /// <summary>
        /// ID licnosti
        /// </summary>
        public Guid LicnostId { get; set; }
        /// <summary>
        /// Model ličnosti
        /// </summary>
        public LicnostUgovoraDto Licnost { get; set; }


        /// <summary>
        /// ID lica (kupac) ugovora
        /// </summary>
        public Guid KupacId { get; set; }
        /// <summary>
        /// Model lica (kupac) ugovora
        /// </summary>
        public KupacUgovoraDto Kupac { get; set; }

        /// <summary>
        ///ID javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }
        /// <summary>
        /// Model javnog nadmetanja
        /// </summary>
        public JavnoNadmetanjeUgovoraDto JavnoNadmetanje { get; set; }


    }
}
