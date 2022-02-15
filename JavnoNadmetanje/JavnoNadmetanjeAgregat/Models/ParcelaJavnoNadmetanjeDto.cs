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
    }
}
