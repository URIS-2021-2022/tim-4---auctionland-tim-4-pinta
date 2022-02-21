using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    /// <summary>
    /// Predstavlja model entiteta tip garancije
    /// </summary>
    public class TipGarancije
    {
        /// <summary>
        /// ID tipa garancije
        /// </summary>
        [Key]
        public Guid TipId { get; set; }

        /// <summary>
        /// Naziv tipa garancije
        /// </summary>
        public string Naziv { get; set; }
       
    }
}
