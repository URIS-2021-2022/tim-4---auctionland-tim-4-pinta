using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

namespace Uplata.Models
{
    /// <summary>
    /// Dto uplate.
    /// </summary>
    public class UplataDto
    {
        /// <summary>
        /// Iznos uplate.
        /// </summary>
        public string Iznos { get; set; }

        /// <summary>
        /// Svrhu uplate.
        /// </summary>
        public string SvrhaUplate { get; set; }

        /// <summary>
        /// Datum uplate.
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Poziv na broj uplate.
        /// </summary>
        public string PozivNaBroj { get; set; }

        /// <summary>
        /// ID kupca tj uplatioca.
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>

        public Guid JavnoNadmetanjeID { get; set; }

    }
}
