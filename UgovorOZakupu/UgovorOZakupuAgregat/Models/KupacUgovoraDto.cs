using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO kupca
    /// </summary>
    public class KupacUgovoraDto
    {
        /// <summary>
        /// Naziv lica ugovora
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Prvi broj telefona lica ugovora
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Drugi broj telefona lica ugovora
        /// </summary>
        public string BrojTelefona2 { get; set; }

        /// <summary>
        /// Email lica ugovora
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ostvarena povrsina lica ugovora
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Broj racuna lica ugovora
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Da li lice ugovora ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum pocetka zabrane lica ugovora
        /// </summary>
        public DateTime DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane u godinama
        /// </summary>
        public int DuzinaTrajanjaZabraneUGodinama { get; set; }

        /// <summary>
        /// Datum prestanka zabrane lica ugovora
        /// </summary>
        public DateTime DatumPrestankaZabrane { get; set; }
    }
}
