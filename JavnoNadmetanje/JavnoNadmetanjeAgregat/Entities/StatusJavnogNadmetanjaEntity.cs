using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Entities
{
    /// <summary>
    /// Entitet statusa javnog nadmetanja
    /// </summary>
    public class StatusJavnogNadmetanjaEntity
    {
        [Key]
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
