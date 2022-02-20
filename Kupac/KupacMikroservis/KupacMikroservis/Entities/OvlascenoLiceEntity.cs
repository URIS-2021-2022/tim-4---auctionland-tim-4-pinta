using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// Model realnog entiteta Ovlasceno lice
    /// </summary>
    public class OvlascenoLiceEntity
    {
        /// <summary>
        /// ID ovlascenog lica
        /// </summary>
        [Key]
        public Guid OvlascenoLiceId { get; set; }

        /// <summary>
        /// Ime ovlascenog lica
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime ovlascenog lica
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// Broj licnog dokumenta
        /// </summary>
        public string BrojLicnogDokumenta { get; set; }

        /// <summary>
        /// Broj table
        /// </summary>
        public string BrojTable { get; set; }

        /// <summary>
        /// Adresa ovlascenog lica
        /// </summary>
        public Guid AdresaID { get; set; }

    }
}