using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Entities
{
    //sluzi za cuvanje u statickoj listi
    /// <summary>
    /// Entitet javnog nadmetanja
    /// </summary>
    public class JavnoNadmetanjeEntity
    {
        [Key]
        /// <summary>
        /// ID javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeID { get; set; }
        /// <summary>
        /// Datum odrzavanja javnog nadmetanja
        /// </summary>
        public DateTime Datum { get; set; }
        /// <summary>
        /// Vreme pocetka javnog nadmetanja
        /// </summary>
        public DateTime VremePocetka { get; set; }
        /// <summary>
        /// Vreme kraja javnog nadmetanja
        /// </summary>
        public DateTime VremeKraja { get; set; }
        /// <summary>
        /// Pocetna cena po hektaru javnog nadmetanja
        /// </summary>
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
         
        [ForeignKey("Tip")]
        public Guid TipID { get; set; }
        public TipJavnogNadmetanjaEntity Tip { get; set; }
        /// <summary>
        /// Status javnog nadmetanja
        /// </summary>
        [ForeignKey("Status")]
        public Guid StatusID { get; set; }
        public StatusJavnogNadmetanjaEntity Status { get; set; }

        /// <summary>
        /// Krug javnog nadmetanja
        /// </summary>
        public int Krug { get; set; }
        /// <summary>
        /// Visina dopune depozita javnog nadmetanja
        /// </summary>
        public int VisinaDopuneDepozita { get; set; }
    }
}

