using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Entities
{
    /// <summary>
    /// Model drzave
    /// </summary>
    public class DrzavaEntity
    {
        /// <summary>
        /// ID drzave
        /// </summary>
        [Key]
        public Guid DrzavaID { get; set; }

        /// <summary>
        /// Naziv drzave
        /// </summary>
        public string NazivDrzave { get; set; }

        /// <summary>
        /// Adrese u drzavi
        /// </summary>
        public List<AdresaEntity> Adrese { get; set; }
    }
}
