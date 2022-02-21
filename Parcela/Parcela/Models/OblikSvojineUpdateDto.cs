using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje oblika svojine
    /// </summary>
    public class OblikSvojineUpdateDto
    {
        /// <summary>
        /// ID oblika svojine
        /// </summary>
        public Guid OblikSvojineID { get; set; }

        /// <summary>
        /// Naziv oblika svojine
        /// </summary>
        public String OblikSvojineNaziv { get; set; }
    }
}
