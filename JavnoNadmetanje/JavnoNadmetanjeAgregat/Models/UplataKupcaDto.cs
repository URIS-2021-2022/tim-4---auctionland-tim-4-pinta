using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class UplataKupcaDto
    {
        /// <summary>
        /// Iznos uplate
        /// </summary>
        public string Iznos { get; set; }

        /// <summary>
        /// Svrha uplate
        /// </summary>
        public string SvrhaUplate { get; set; }

        /// <summary>
        /// Datum uplate
        /// </summary>
        public string Datum { get; set; }

        /// <summary>
        /// Poziv na broj uplate
        /// </summary>
        public string PozivNaBroj { get; set; }
    }
}
