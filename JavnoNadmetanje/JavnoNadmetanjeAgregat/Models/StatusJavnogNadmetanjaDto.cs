using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za status javnog nadmetanja
    /// </summary>
    public class StatusJavnogNadmetanjaDto
    {
        /// <summary>
        /// Naziv statusa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv statusa")]
        public String NazivStatusaJavnogNadmetanja { get; set; }

    }
}
