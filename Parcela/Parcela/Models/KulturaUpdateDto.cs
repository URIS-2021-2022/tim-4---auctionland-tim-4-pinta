using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje kulture
    /// </summary>
    public class KulturaUpdateDto
    {
        /// <summary>
        /// ID kulture
        /// </summary>
        public Guid KulturaID { get; set; }

        /// <summary>
        /// Naziv kulture
        /// </summary>
        public String KulturaNaziv { get; set; }
    }
}
