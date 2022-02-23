using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za sluzbeni list
    /// </summary>
    public class SluzbeniListDto
    {
        /// <summary>
        /// Opstina sluzbenog lista
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti opstinu")]
        public String Opstina { get; set; }
        /// <summary>
        /// Broj sluzbenog lista
        /// </summary>
         [Required(ErrorMessage = "Obavezno je uneti broj sluzbenog lista")]
        public int BrojSluzbenogLista { get; set; }
        /// <summary>
        /// Datum izdavanja sluzbenog lista
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti datum izdavanja")]
        public DateTime DatumIzdavanjaSluzbenogLista { get; set; }

    }
}
