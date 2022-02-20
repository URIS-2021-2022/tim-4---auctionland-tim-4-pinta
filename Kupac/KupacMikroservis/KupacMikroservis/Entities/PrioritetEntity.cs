using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// Model realnog entiteta Prioritet kupca
    /// </summary>
    public class PrioritetEntity
    {
        /// <summary>
        /// ID prioriteta
        /// </summary>
        [Key]
        public Guid PrioritetId { get; set; }

        /// <summary>
        /// Opis, odnosno naziv prioriteta
        /// </summary>
        public string PrioritetOpis { get; set; }

    }
}