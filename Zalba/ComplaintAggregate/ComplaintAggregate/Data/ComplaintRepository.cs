using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class ComplaintRepository : IComplaintRepository
    {
        public static List<Complaint> ListOfComplainations { get; set; } = new List<Complaint>();

        public ComplaintRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ListOfComplainations.AddRange(new List<Complaint>
            {
                new Complaint
                {
                    ZalbaID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Datum_podnosenja_zalbe= DateTime.Parse("1-1-2011"),
                    Razlog_zalbe = "razlog",
                    Obrazlozenje = "obrazlozenje",
                    Datum_rijesenja = DateTime.Parse("5-1-2011"),
                    Broj_rijesenja=23,
                    Broj_nadmetanja=32,

                },
                new Complaint
                {
                    ZalbaID = Guid.Parse("ec2e5d91-de9f-4af0-8fae-d8150e338c51"),
                    Datum_podnosenja_zalbe= DateTime.Parse("1-1-2012"),
                    Razlog_zalbe = "razlog2",
                    Obrazlozenje = "obrazlozenje2",
                    Datum_rijesenja = DateTime.Parse("5-1-2012"),
                    Broj_rijesenja=23,
                    Broj_nadmetanja=32,

                }
            });

        }

        public List<Complaint> GetComplaint()
        {
            return (from e in ListOfComplainations
                    select e).ToList();

        }

        public Complaint GetComplaintById(Guid ZalbaId)
        {
            return ListOfComplainations.FirstOrDefault(e => e.ZalbaID == ZalbaId);
        }

        public Complaint CreateComplaint(Complaint complainAggregate)
        {
            complainAggregate.ZalbaID = Guid.NewGuid();
            ListOfComplainations.Add(complainAggregate);
            Complaint complain = GetComplaintById(complainAggregate.ZalbaID);

            return new Complaint
            {
                ZalbaID = complain.ZalbaID,
                Datum_podnosenja_zalbe = complain.Datum_podnosenja_zalbe,
                Razlog_zalbe = complain.Razlog_zalbe,
                Obrazlozenje = complain.Obrazlozenje,
                Datum_rijesenja = complain.Datum_rijesenja,
                Broj_rijesenja = complain.Broj_rijesenja,
                Broj_nadmetanja = complain.Broj_nadmetanja
            };
        }

        public Complaint UpdateComplaint(Complaint complainAggregate)
        {
            Complaint complain = GetComplaintById(complainAggregate.ZalbaID);

            complain.Datum_podnosenja_zalbe = complainAggregate.Datum_podnosenja_zalbe;
            complain.Razlog_zalbe = complainAggregate.Razlog_zalbe;
            complain.Obrazlozenje = complainAggregate.Obrazlozenje;
            complain.Datum_rijesenja = complainAggregate.Datum_rijesenja;
            complain.Broj_rijesenja = complainAggregate.Broj_rijesenja;
            complain.Broj_nadmetanja = complainAggregate.Broj_nadmetanja;


            return new Complaint
            {
                ZalbaID = complain.ZalbaID,
                Datum_podnosenja_zalbe = complain.Datum_podnosenja_zalbe,
                Razlog_zalbe = complain.Razlog_zalbe,
                Obrazlozenje = complain.Obrazlozenje,
                Datum_rijesenja = complain.Datum_rijesenja,
                Broj_rijesenja = complain.Broj_rijesenja,
                Broj_nadmetanja = complain.Broj_nadmetanja
            };
        }

        public void DeleteComplaint(Guid ZalbaID)
        {
            ListOfComplainations.Remove
                (ListOfComplainations.FirstOrDefault(e => e.ZalbaID == ZalbaID));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
