using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    public class FizickoLiceEntity : KupacEntity
    {

        public string JMBG { get; set; }

        public int KontaktOsoba { get; set; }

    }
}

