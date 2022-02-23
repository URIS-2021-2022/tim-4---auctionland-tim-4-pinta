using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class JavnoNadmetanjeUpdateDto
    {
        /// <summary>
        /// ID javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ID")]
        public Guid JavnoNadmetanjeID { get; set; }
        /// <summary>
        /// Datum odrzavanja javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti datum")]
        public DateTime Datum { get; set; }
        /// <summary>
        /// Vreme pocetka javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti vreme pocetka")]
        public DateTime VremePocetka { get; set; }
        /// <summary>
        /// Vreme kraja javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti vreme kraja")]
        public DateTime VremeKraja { get; set; }
        /// <summary>
        /// Pocetna cena po hektaru javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti pocetnu cenu po hektaru")]
        public int PocetnaCenaPoHektaru { get; set; }
        /// <summary>
        /// Period zakupa javnog nadmetanja
        /// </summary>
        public int PeriodZakupa { get; set; }
        /// <summary>
        /// Izuzetos javnog nadmetanja
        /// </summary>
        public Boolean Izuzeto { get; set; }
        /// <summary>
        /// Tip javnog nadmetanja
        /// </summary>
        public Guid TipID { get; set; }
        public Guid StatusID { get; set; }
        public int Krug { get; set; }
        /// <summary>
        /// Visina dopune depozita javnog nadmetanja
        /// </summary>
        public int VisinaDopuneDepozita { get; set; }

        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// ID kupca parcele
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelaID { get; set; }

        /// <summary>
        /// ID adrese
        /// </summary>
        public Guid AdresaID { get; set; }
    }
}

