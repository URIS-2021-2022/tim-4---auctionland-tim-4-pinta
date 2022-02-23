using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Obavezno je uneti naziv kupca parcele")]
        public string Naziv { get; set; }

        /// <summary>
        /// Da li je kupac fizicko ili pravno lice
        /// </summary>
        public bool IsFizickoLice { get; set; }

        /// <summary>
        /// Prvi broj telefona kupca parcele
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj telefona")]
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

        /// <summary>
        /// Prioritet kupca
        /// </summary>
        public Guid Prioritet { get; set; }

        /// <summary>
        /// ID ovlascenog lica
        /// </summary>
        public Guid OvlascenoLice { get; set; }

        /// <summary>
        /// ID adrese kupca
        /// </summary>
        public Guid AdresaID { get; set; }

        /// <summary>
        /// ID uplate kupca
        /// </summary>
        public Guid UplataID { get; set; }

        /// <summary>
        /// Adresa kupca
        /// </summary>
        public AdresaKupcaDto Adresa { get; set; }

        /// <summary>
        /// Uplata kupca
        /// </summary>
        public UplataKupcaDto Uplata { get; set; }
    }

}
    

