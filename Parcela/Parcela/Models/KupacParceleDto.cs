using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za kupca parcele
    /// </summary>
    public class KupacParceleDto
    {
        /// <summary>
        /// Naziv kupca parcele
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Prvi broj telefona kupca parcele
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Drugi broj telefona kupca parcele
        /// </summary>
        public string BrojTelefona2 { get; set; }

        /// <summary>
        /// Email kupca parcele
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ostvarena povrsina kupca parcele
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Broj racuna kupca parcele
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Da li kupac parcele ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum pocetka zabrane kupca parcele
        /// </summary>
        public DateTime DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane u godinama
        /// </summary>
        public int DuzinaTrajanjaZabraneUGodinama { get; set; }

        /// <summary>
        /// Datum prestanka zabrane kupca parcele
        /// </summary>
        public DateTime DatumPrestankaZabrane { get; set; }
    }
}
