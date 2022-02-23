using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kreiranje odvodnjavanja
    /// </summary>
    public class OdvodnjavanjeCreateDto
    {
        /// <summary>
        /// Naziv tipa odvodnjavanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv odvodanjavanja")]
        public String OdvodnjavanjeNaziv { get; set; }
    }
}
