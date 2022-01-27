using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Entities
{
    public class Complaint
    {
        public Guid ZalbaID { get; set; }
        public DateTime Datum_podnosenja_zalbe { get; set; }
        public string Razlog_zalbe { get; set; }
        public string Obrazlozenje { get; set; }
        public DateTime Datum_rijesenja { get; set; }
        public int Broj_rijesenja { get; set; }
        public int Broj_nadmetanja { get; set; }
    }
}
