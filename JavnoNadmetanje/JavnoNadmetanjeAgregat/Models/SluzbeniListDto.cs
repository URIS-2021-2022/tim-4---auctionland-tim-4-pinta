using System;
using System.Collections.Generic;
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
