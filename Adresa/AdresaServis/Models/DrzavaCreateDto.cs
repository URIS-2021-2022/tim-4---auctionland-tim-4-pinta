using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Models
{
    /// <summary>
    /// DTO za kreiranje drzave
    /// </summary>
    public class DrzavaCreateDto
    {
        /// <summary>
        /// Naziv drzave
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv drzave")]
        public string NazivDrzave { get; set; }
    }
}
