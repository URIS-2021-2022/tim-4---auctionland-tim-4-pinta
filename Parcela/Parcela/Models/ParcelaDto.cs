using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za parcelu
    /// </summary>
    public class ParcelaDto
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
        /// Zasticena zona
        /// </summary>
        public ZasticenaZonaDto ZasticenaZona { get; set; }

        /// <summary>
        /// ID odvodnjavanja
        /// </summary>
        public Guid OdvodnjavanjeID { get; set; }

        /// <summary>
        /// Odvodnjavanje parcele
        /// </summary>
        public OdvodnjavanjeDto Odvodnjavanje { get; set; }

        /// <summary>
        /// ID obradivosti
        /// </summary>
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// Obradivost parcele
        /// </summary>
        public ObradivostDto Obradivost { get; set; }

        /// <summary>
        /// ID oblika svojine
        /// </summary>
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// Oblik svojine parcele
        /// </summary>
        public OblikSvojineDto OblikSvojine { get; set; }

        /// <summary>
        /// ID kulture
        /// </summary>
        public Guid KulturaID { get; set; }

        /// <summary>
        /// Kultura parcele
        /// </summary>
        public KulturaDto Kultura { get; set; }

        /// <summary>
        /// ID klase
        /// </summary>
        public Guid KlasaID { get; set; }

        /// <summary>
        /// Klasa parcele
        /// </summary>
        public KlasaDto Klasa { get; set; }

        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// Katastarska opstina parcele
        /// </summary>
        public OpstinaParceleDto Opstina { get; set; }

        /// <summary>
        /// ID kupca parcele
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// Kupac parcele
        /// </summary>
        public KupacParceleDto Kupac { get; set; }
    }
}
