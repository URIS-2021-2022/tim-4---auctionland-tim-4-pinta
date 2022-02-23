using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje obradivosti
    /// </summary>
    public class ObradivostUpdateDto
    {
        /// <summary>
        /// ID obradivosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti id obradivosti")]
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// Naziv tipa obradivosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv obradivosti")]
        public String ObradivostNaziv { get; set; }
    }
}
