using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// Model realnog entiteta Kupac
    /// </summary>
    public class KupacEntity
    {

        /// <summary>
        /// ID kupca
        /// </summary>
        [Key]
        public Guid KupacId { get; set; }

        /// <summary>
        /// Da li je kupac fizicko lice 
        /// </summary>
        public bool IsFizickoLice { get;set;}

        /// <summary>
        /// Naziv kupca
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Broj telefona kupca 1
        /// </summary>
        public string BrojTelefona1 { get; set; }

        /// <summary>
        /// Broj telefona kupca 2
        /// </summary>
        public string BrojTelefona2 { get; set; }

        /// <summary>
        /// Email kupca
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Broj racuna kupca
        /// </summary>
        public string BrojRacuna { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum pocetka zabrane
        /// </summary>
        public DateTime? DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane u godinama
        /// </summary>
        public int DuzinaTrajanjaZabraneUGodinama { get; set; }

        /// <summary>
        /// Datum prestanka zabrane
        /// </summary>
        public DateTime? DatumPrestankaZabrane { get; set; }

        /// <summary>
        /// Prioritet kupca
        /// </summary>
        [ForeignKey("PrioritetEntity")]
        public Guid Prioritet { get; set; }

        /// <summary>
        /// Ovlasceno lice kupca
        /// </summary>
        [ForeignKey("OvlascenoLicEntity")]
        public Guid OvlascenoLice { get; set; }

        /// <summary>
        /// Adresa kupca
        /// </summary>
        public Guid AdresaID { get; set; }

        /// <summary>
        /// Uplata kupca
        /// </summary>
        public Guid UplataID { get; set; }

    }
}