using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    public class PrioritetEntity
    {
        [Key]
        public Guid PrioritetId { get; set; }

        public string PrioritetOpis { get; set; }

    }
}