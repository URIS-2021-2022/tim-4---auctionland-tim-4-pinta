
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// DTO za uplatu kupca
    /// </summary>
    public class UplataKupcaDto
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
        /// Broj racuna
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>

        public Guid? JavnoNadmetanjeID { get; set; }


     /*   public JavnoNadmetanjeUplateDto JavnoNadmetanje { get; set; }

        /// <summary>
        /// Kurs
        /// </summary>
        public Kurs Kurs { get; set; }

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
     */

    }
}