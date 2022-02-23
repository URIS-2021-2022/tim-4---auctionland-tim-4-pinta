using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za katastarsku opstinu javnog nadmetanja
    /// </summary>
    public class KatastarskaOpstinaJavnoNadmetanjeDto
    {
        /// <summary>
        /// Naziv katastarske opstine
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv katastarske opstine")]
        public string NazivKatastarskeOpstine { get; set; }
    }
}
