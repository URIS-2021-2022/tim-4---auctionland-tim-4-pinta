using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model klase
    /// </summary>
    public class KlasaEntity
    {
        /// <summary>
        /// ID klase
        /// </summary>
        [Key]
        public Guid KlasaID { get; set; }

        /// <summary>
        /// Oznaka klase
        /// </summary>
        public string KlasaOznaka { get; set; }

        /// <summary>
        /// Parcele
        /// </summary>
        public List<ParcelaEntity> Parcele { get; set; }
    }
}
