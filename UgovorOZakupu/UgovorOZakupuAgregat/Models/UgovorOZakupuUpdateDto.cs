using System;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// Model za ažuriranje ugovora o zakupu
    /// </summary>
    public class UgovorOZakupuUpdateDto
    {
        /// <summary>
        /// ID ugovora o zakupu
        /// </summary>
        public Guid UgovorId { get; set; }

        /// <summary>
        /// ID dokumenta(odluka)
        /// </summary>
        public Guid DokumentId { get; set; }

        /// <summary>
        /// ID tipa garancije
        /// </summary>
        public Guid TipId { get; set; }

        /// <summary>
        /// Zavodni broj ugovora
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum zavođenja ugovora
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
        /// ID ličnosti (ministar)
        /// </summary>
        public Guid LicnostId { get; set; }

        /// <summary>
        /// ID kupca (lice)
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        ///ID javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }
    }
}