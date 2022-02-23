using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Models
{
    /// <summary>
    /// DTO za kreiranje adrese
    /// </summary>
    public class AdresaCreateDto
    {
        /// <summary>
        /// Ulica adrese
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ulicu")]
        public string Ulica { get; set; }

        /// <summary>
        /// Broj adrese
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj")]
        public string Broj { get; set; }

        /// <summary>
        /// Mesto adrese
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti mesto")]
        public string Mesto { get; set; }

        /// <summary>
        /// Postanski broj adrese
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti postanski broj")]
        public string PostanskiBroj { get; set; }

        /// <summary>
        /// ID drzave
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id drzave")]
        public Guid DrzavaID { get; set; }
    }
}
