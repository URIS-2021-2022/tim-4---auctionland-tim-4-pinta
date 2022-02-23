using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje oblika svojine
    /// </summary>
    public class OblikSvojineCreateDto
    {
        /// <summary>
        /// Naziv oblika svojine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv oblika svojine")]
        public String OblikSvojineNaziv { get; set; }
    }
}
