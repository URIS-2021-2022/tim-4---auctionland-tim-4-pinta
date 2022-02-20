using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za kupca javnog nadmetanja
    /// </summary>
    public class KupacJavnoNadmetanjeDto
    {
        /// <summary>
        /// Naziv kupca parcele
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Prvi broj telefona kupca javnog nadmetanja
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Drugi broj telefona kupca javnog nadmetanja
        /// </summary>
        public string BrojTelefona2 { get; set; }

        /// <summary>
        /// Email kupca javnog nadmetanja
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ostvarena povrsina kupca javnog nadmetanja
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Broj racuna kupca javnog nadmetanja
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Da li kupac javnog nadmetanja ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum pocetka zabrane kupca javnog nadmetanja
        /// </summary>
        public DateTime DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane u godinama
        /// </summary>
        public int DuzinaTrajanjaZabraneUGodinama { get; set; }

        /// <summary>
        /// Datum prestanka zabrane kupca javnog nadmetanja
        /// </summary>
        public DateTime DatumPrestankaZabrane { get; set; }
    }
}
