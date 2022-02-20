using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Entities
{
    public class Complaint
    {
        [Key]
        public Guid ZalbaID { get; set; }
        public DateTime Datum_podnosenja_zalbe { get; set; }
        public string Razlog_zalbe { get; set; }
        public string Obrazlozenje { get; set; }
        public DateTime Datum_rijesenja { get; set; }
        public int Broj_rijesenja { get; set; }
        public int Broj_nadmetanja { get; set; }
 
        [ForeignKey("StatusOfComplaint")]
        public Guid Status_zalbe { get; set; }
        public StatusOfComplaint StatusOfComplaint { get; set; }

        [ForeignKey("TypeOfComplaint")]
        public Guid Tip_id { get; set; }
        public TypeOfComplaint TypeOfComplaint { get; set; }
       
        [ForeignKey("ActionBasedOnComplaint")]
        public Guid Radnja_na_osnovu_zalbe_ID { get; set; }
        public ActionBasedOnComplaint ActionBasedOnComplaint { get; set; }
    }
}
