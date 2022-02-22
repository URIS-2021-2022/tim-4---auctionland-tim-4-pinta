using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Entities
{
    /// <summary>
    /// Model entiteta ličnost
    /// </summary>
    public class LicnostEntity
    {
        /// <summary>
        /// ID ličnosti
        /// </summary>
        [Key]
        public Guid LicnostId { get; set; }

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        public string LicnostIme { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        public string LicnostPrezime { get; set; }

        /// <summary>
        /// Funkcija ličnosti
        /// </summary>
        public string LicnostFunkcija { get; set; }
    }
}
