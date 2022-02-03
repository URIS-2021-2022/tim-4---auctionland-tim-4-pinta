using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model parcele
    /// </summary>
    public class ParcelaEntity
    {
        /// <summary>
        /// ID parcele
        /// </summary>
        [Key]
        public Guid ParcelaID { get; set; }

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
        [ForeignKey("ZasticenaZona")]
        public Guid ZasticenaZonaID { get; set; }
        public ZasticenaZonaEntity ZasticenaZona { get; set; }

        /// <summary>
        /// ID odvodnjavanja
        /// </summary>
        [ForeignKey("Odvodnjavanje")]
        public Guid OdvodnjavanjeID { get; set; }
        public OdvodnjavanjeEntity Odvodnjavanje { get; set; }

        /// <summary>
        /// ID obradivosti
        /// </summary>
        [ForeignKey("Obradivost")]
        public Guid ObradivostID { get; set; }
        public ObradivostEntity Obradivost { get; set; }

        /// <summary>
        /// ID oblika svojine
        /// </summary>
        [ForeignKey("OblikSvojine")]
        public Guid OblikSvojineID { get; set; }
        public OblikSvojineEntity OblikSvojine { get; set; }


        /// <summary>
        /// ID kulture
        /// </summary>
        [ForeignKey("Kultura")]
        public Guid KulturaID { get; set; }
        public KulturaEntity Kultura { get; set; }


        /// <summary>
        /// ID klase
        /// </summary>
        [ForeignKey("Klasa")]
        public Guid KlasaID { get; set; }
        public KlasaEntity Klasa { get; set; }

        public List<DeoParceleEntity> DeloviParcele { get; set; }

        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// ID kupca parcele
        /// </summary>
        public Guid KupacID { get; set; }
    }
}
