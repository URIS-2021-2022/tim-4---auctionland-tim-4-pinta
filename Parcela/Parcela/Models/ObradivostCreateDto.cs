using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje obradivosti
    /// </summary>
    public class ObradivostCreateDto
    {
        /// <summary>
        /// Naziv tipa obradivosti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv obradivosti")]
        public String ObradivostNaziv { get; set; }
    }
}
