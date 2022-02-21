using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class TipJavnogNadmetanjaUpdateDto
    {
        /// <summary>
        ///ID tipa javnog nadmetanja
        /// </summary>
        public Guid TipJavnogNadmetanjaID { get; set; }
        /// <summary>
        ///Naziv tipa javnog nadmetanja
        /// </summary>
        public String NazivTipaJavnogNadmetanja { get; set; }
    }
}
