using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    public class RokoviDospeca
    {
        [Key]
        public Guid RokId { get; set; }


        [ForeignKey("UgovorOZakupu")]
        public Guid UgovorId { get; set; }
        public UgovorOZakupu UgovorOZakupu { get; set; }

        public int RokDospeca { get; set; }
    }
}
