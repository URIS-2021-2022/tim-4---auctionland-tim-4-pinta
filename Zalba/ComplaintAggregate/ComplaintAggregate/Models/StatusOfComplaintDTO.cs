using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Models
{
    public class StatusOfComplaintDTO
    {
        public bool Usvojena { get; set; }
        public bool Odbijena { get; set; }
        public bool Otvorena { get; set; }
    }
}
