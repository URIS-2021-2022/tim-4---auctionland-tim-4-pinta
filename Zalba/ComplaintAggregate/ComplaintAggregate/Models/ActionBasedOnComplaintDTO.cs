using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Models
{
    public class ActionBasedOnComplaintDTO
    {
        public bool JN_ide_u_krug_sa_novim_uslovima { get; set; }
        public bool JN_ide_u_krug_sa_starim_uslovima { get; set; }
        public bool JN_ne_ide_u_drugi_krug { get; set; }
    }
}
