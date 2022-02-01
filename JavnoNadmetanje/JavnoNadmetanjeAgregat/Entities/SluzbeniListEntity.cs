using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Entities
{
    public class SluzbeniListEntity
    {
        public Guid SluzbeniListID { get; set; }
        public String Opstina { get; set; }
        public int BrojSluzbenogLista { get; set; }
        public DateTime DatumIzdavanjaSluzbenogLista { get; set; }
    }
}
