using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Entities
{
    /// <summary>
    ///Entitet tipa javnog nadmetanja
    /// </summary>
    public class TipJavnogNadmetanjaEntity
    {
        [Key]
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
