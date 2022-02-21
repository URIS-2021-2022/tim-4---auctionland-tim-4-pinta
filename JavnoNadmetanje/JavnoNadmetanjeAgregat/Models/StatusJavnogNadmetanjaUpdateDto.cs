using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class StatusJavnogNadmetanjaUpdateDto
    {
        
        /// <summary>
        /// ID statusa javnog nadmetanja
        /// </summary>
        public Guid StatusJavnogNadmetanjaID { get; set; }
        /// <summary>
        ///Naziv statusa javnog nadmetanja
        /// </summary>
        public String NazivStatusaJavnogNadmetanja { get; set; }
    }
}
