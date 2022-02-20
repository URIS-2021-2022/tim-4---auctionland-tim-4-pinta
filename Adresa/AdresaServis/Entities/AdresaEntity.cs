using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Entities
{
    /// <summary>
    /// Model adrese
    /// </summary>
    public class AdresaEntity
    {
        /// <summary>
        /// ID adrese
        /// </summary>
        [Key]
        public Guid AdresaID { get; set; }

        /// <summary>
        /// Ulica adrese
        /// </summary>
        public string Ulica { get; set; }

        /// <summary>
        /// Broj adrese
        /// </summary>
        public string Broj { get; set; }

        /// <summary>
        /// Mesto adrese
        /// </summary>
        public string Mesto { get; set; }

        /// <summary>
        /// Postanski broj adrese
        /// </summary>
        public string PostanskiBroj { get; set; }

        /// <summary>
        /// ID drzave
        /// </summary>
        [ForeignKey("Drzava")]
        public Guid DrzavaID { get; set; } 

        /// <summary>
        /// Drzava adrese
        /// </summary>
        public DrzavaEntity Drzava { get; set; }
    }
}
