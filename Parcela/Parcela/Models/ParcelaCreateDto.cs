using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje parcele
    /// </summary>
    public class ParcelaCreateDto
    {
        /// <summary>
        /// Povrsina parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti povrsinu parcele")]
        public int Povrsina { get; set; }

        /// <summary>
        /// Broj parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj parcele")]
        public String BrojParcele { get; set; }

        /// <summary>
        /// Broj lista nepokretnosti za parcelu
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj lista neporetnosti")]
        public String BrojListaNepokretnosti { get; set; }

        /// <summary>
        /// Stvarno stanje kulture na parceli
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti stvarno stanje kulture")]
        public String KulturaStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje klase na parceli
        /// </summary>
        [Required(ErrorMessage = "Obavezno je stvarno stanje klase")]
        public String KlasaStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje obradivosti za parcelu
        /// </summary>
        [Required(ErrorMessage = "Obavezno je stvarno stanje obradivosti")]
        public String ObradivostStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje zasticene zone za parcelu
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti stvarno stanje zasticene zone")]
        public String ZasticenaZonaStvarnoStanje { get; set; }

        /// <summary>
        /// Stvarno stanje odvodnjavanja za parcelu
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti stvarno stanje odvodnjavanja")]
        public String OdvodnjavanjeStvarnoStanje { get; set; }

        /// <summary>
        /// ID zasticene zone
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id zasticene zone")]
        public Guid ZasticenaZonaID { get; set; }

        /// <summary>
        /// ID odvodnjavanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id odvodnjavanja")]
        public Guid OdvodnjavanjeID { get; set; }

        /// <summary>
        /// ID obradivosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id obradivosti")]
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// ID oblika svojine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id oblika svojine")]
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// ID kulture
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id kulture")]
        public Guid KulturaID { get; set; }

        /// <summary>
        /// ID klase
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id klase")]
        public Guid KlasaID { get; set; }

        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id katastarske opstine")]
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// ID kupca parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id kupca")]
        public Guid KupacID { get; set; }
    }
}
