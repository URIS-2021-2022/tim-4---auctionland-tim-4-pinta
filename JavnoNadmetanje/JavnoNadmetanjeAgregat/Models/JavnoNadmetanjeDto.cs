using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    //Vracamo podatke o javnom nadmetanju i kreiramo nova javna nadmetanja
    //u kom formatu ce biti podaci koje vracamo

    /// <summary>
    /// DTO za javno nadmetanje
    /// </summary>
    public class JavnoNadmetanjeDto
    {
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
        /// <summary>
        /// Naziv tipa javnog nadmetanja
        /// </summary>
        public TipJavnogNadmetanjaDto Tip { get; set; }
        /// <summary>
        /// Status javnog nadmetanja ID
        /// </summary>
        public Guid StatusID { get; set; }
        /// <summary>
        /// Status javnog nadmetanja ID
        /// </summary>
        public StatusJavnogNadmetanjaDto Status { get; set; }
        /// <summary>
        /// Krug javnog nadmetanja
        /// </summary>
        public int Krug { get; set; }
        /// <summary>
        /// Visina dopune depozita javnog nadmetanja
        /// </summary>
        public int VisinaDopuneDepozita { get; set; }
        /// <summary>
        /// ID Katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }
        /// <summary>
        /// Katastarska opstina javnog nadmetanja
        /// </summary>
        public KatastarskaOpstinaJavnoNadmetanjeDto KatastarskaOpstina { get; set; }
        /// <summary>
        /// ID kupca parcele
        /// </summary>
        public Guid KupacID { get; set; }
        /// <summary>
        /// Kupac javnog nadmetanja
        /// </summary>
        public KupacJavnoNadmetanjeDto Kupac { get; set; }
        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelaID { get; set; }
        /// <summary>
        /// Parcela javnog nadmetanja
        /// </summary>
        public ParcelaJavnoNadmetanjeDto Parcela {get; set;}
        /// <summary>
        /// ID adrese
        /// </summary>
        public Guid AdresaID { get; set; }
        /// <summary>
        /// Adresa javnog nadmetanja
        /// </summary>
        public AdresaJavnoNadmetanjeDto Adresa { get; set; }


    }
}
