
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// DTO za uplatu kupca
    /// </summary>
    public class UplataKupcaDTO
    {
        /// <summary>
        /// Broj racuna kupca
        /// </summary>
        public string PozivNaBroj { get; set; }

        /// <summary>
        /// Iznos uplate
        /// </summary>
        public string Iznos { get; set; }

        /// <summary>
        /// Svrha uplate
        /// </summary>
        public string Svrha { get; set; }

        /// <summary>
        /// Datum uplate
        /// </summary>
        public string Datum { get; set; }


    }
}