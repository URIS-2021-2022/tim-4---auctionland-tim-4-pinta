using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za tip javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaDto
    {
        /// <summary>
        /// naziv tip javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv tipa")]
        public String NazivTipaJavnogNadmetanja { get; set; }
    }
}
