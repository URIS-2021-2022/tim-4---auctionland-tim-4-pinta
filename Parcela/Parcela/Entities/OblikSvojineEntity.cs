using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Predstavlja model za oblik svojine
    /// </summary>
    public class OblikSvojineEntity
    {
        /// <summary>
        /// ID oblika svojine
        /// </summary>
        [Key]
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// Naziv tipa oblika svojine
        /// </summary>
        public String OblikSvojineNaziv { get; set; }

        public List<ParcelaEntity> Parcele { get; set; }
    }
}
