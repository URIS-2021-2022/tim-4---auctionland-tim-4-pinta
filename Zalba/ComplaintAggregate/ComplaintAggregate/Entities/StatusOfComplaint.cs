using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Entities
{
    public class StatusOfComplaint
    {
        [Key]
        public Guid Status_zalbe { get; set; }
        public bool Usvojena { get; set; }
        public bool Odbijena { get; set; }
        public bool Otvorena { get; set; }

        public List<Complaint> Complaints { get; set; }
    }
}
