using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Models
{
    /// <summary>
    /// DTO za modifikovanje drzave
    /// </summary>
    public class DrzavaUpdateDto
    {
        /// <summary>
        /// ID drzave
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id drzave")]
        public Guid DrzavaID { get; set; }

        /// <summary>
        /// Naziv drzave
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv drzave")]
        public string NazivDrzave { get; set; }
    }
}
