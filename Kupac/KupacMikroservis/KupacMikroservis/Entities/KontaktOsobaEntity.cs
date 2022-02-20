using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{

    /// <summary>
    /// Modela realnog entiteta Kontakt Osoba
    /// </summary>
    public class KontaktOsobaEntity
    {

        /// <summary>
        /// ID kontakt osobe
        /// </summary>
        [Key]
        public Guid KontaktOsobaId { get; set; }

        /// <summary>
        /// Ime kontakt osobe
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime kontakt osobe
        /// </summary>
        public string Prezime { get; set; }


        /// <summary>
        /// Funkcija kontakt osobe 
        /// </summary>
        public string Funkcija { get; set; }

        /// <summary>
        /// Telefon kontakt osobe
        /// </summary>
        public string Telefon { get; set; }

    }
}