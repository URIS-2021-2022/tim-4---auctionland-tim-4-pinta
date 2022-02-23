using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class SluzbeniListUpdateDto
    {

        /// <summary>
        /// ID sluzbeni list 
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ID")]
        public Guid SluzbeniListID { get; set; }
        /// <summary>
        ///Opstina sluzbenog lista
        /// </summary>
        public String Opstina { get; set; }
        /// <summary>
        /// Broj sluzbenog lista
        /// </summary>
        public int BrojSluzbenogLista { get; set; }
        /// <summary>
        /// Datum izdavanja sluzbenog lista
        /// </summary>
        public DateTime DatumIzdavanjaSluzbenogLista { get; set; }
    }
}
