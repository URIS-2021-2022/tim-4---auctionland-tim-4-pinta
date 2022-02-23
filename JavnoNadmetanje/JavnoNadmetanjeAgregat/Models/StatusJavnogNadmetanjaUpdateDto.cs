using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class StatusJavnogNadmetanjaUpdateDto
    {

        /// <summary>
        /// ID statusa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ID")]
        public Guid StatusJavnogNadmetanjaID { get; set; }
        /// <summary>
        ///Naziv statusa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv statusa")]
        public String NazivStatusaJavnogNadmetanja { get; set; }
    }
}
