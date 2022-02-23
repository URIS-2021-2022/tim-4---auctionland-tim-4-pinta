using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za parcelu javnog nadmetanja
    /// </summary>
    public class ParcelaJavnoNadmetanjeDto
    {
        /// <summary>
        /// Povrsina parcele
        /// </summary>
        public int Povrsina { get; set; }

        /// <summary>
        /// Broj parcele
        /// </summary>
        public String BrojParcele { get; set; }

        /// <summary>
        /// Broj lista nepokretnosti za parcelu
        /// </summary>
        public String BrojListaNepokretnosti { get; set; }

        /// <summary>
        /// Stvarno stanje kulture na parceli
        /// </summary>
        public String KulturaStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje klase na parceli
        /// </summary>
        public String KlasaStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje obradivosti za parcelu
        /// </summary>
        public String ObradivostStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje zasticene zone za parcelu
        /// </summary>
        public String ZasticenaZonaStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje odvodnjavanja za parcelu
        /// </summary>
        public String OdvodnjavanjeStvarnoStanje { get; set; }

        /// <summary>
        /// ID zasticene zone
        /// </summary>
        public Guid ZasticenaZonaID { get; set; }

        /// <summary>
        /// ID odvodnjavanja
        /// </summary>
        public Guid OdvodnjavanjeID { get; set; }

        /// <summary>
        /// ID obradivosti
        /// </summary>
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// ID oblika svojine
        /// </summary>
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// ID kulture
        /// </summary>
        public Guid KulturaID { get; set; }

        /// <summary>
        /// ID klase
        /// </summary>
        public Guid KlasaID { get; set; }

        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// Katastarska opstina parcele
        /// </summary>
        public KatastarskaOpstinaJavnoNadmetanjeDto Opstina { get; set; }

        /// <summary>
        /// ID kupca parcele
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// Kupac parcele
        /// </summary>
        public KupacJavnoNadmetanjeDto Kupac { get; set; }
    }
}
