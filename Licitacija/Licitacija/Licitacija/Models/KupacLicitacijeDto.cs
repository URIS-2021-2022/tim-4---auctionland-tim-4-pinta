using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija.Models
{
    /// <summary>
    /// DTO za kupca licitacije
    /// </summary>
    public class KupacLicitacijeDto
    {
        /// <summary>
        /// Naziv kupca licitacije
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Prvi broj telefona kupca licitacije
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Drugi broj telefona kupca licitacije
        /// </summary>
        public string BrojTelefona2 { get; set; }

        /// <summary>
        /// Email kupca licitacije
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ostvarena povrsina kupca licitacije
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Broj racuna kupca licitacije
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Da li kupac licitacije ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum pocetka zabrane kupca licitacije
        /// </summary>
        public DateTime DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane u godinama
        /// </summary>
        public int DuzinaTrajanjaZabraneUGodinama { get; set; }

        /// <summary>
        /// Datum prestanka zabrane kupca licitacije
        /// </summary>
        public DateTime DatumPrestankaZabrane { get; set; }

        /// <summary>
        /// Prioritet
        /// </summary>
        public Guid Prioritet { get; set; }
        /// <summary>
        /// Ovlasceno lice
        /// </summary>
        public Guid OvlascenoLice { get; set; }
        /// <summary>
        /// ID adrese
        /// </summary>
        public Guid AdresaID { get; set; }
        /// <summary>
        /// ID uplate
        /// </summary>
        public Guid UplataID { get; set; }
    }
}
