using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    public class TipGarancije
    {
        [Key]
        public Guid TipId { get; set; }
        public string Naziv { get; set; }
       
    }
}
