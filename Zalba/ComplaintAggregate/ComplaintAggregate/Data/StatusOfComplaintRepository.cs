using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class StatusOfComplaintRepository : IStatusOfComplaintRepository
    {
        public static List<StatusOfComplaint> ListOfComplainations { get; set; } = new List<StatusOfComplaint>();

        public StatusOfComplaintRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ListOfComplainations.AddRange(new List<StatusOfComplaint>
            {
                new StatusOfComplaint
                {
                    Status_zalbe = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Usvojena= false,
                    Odbijena=true,
                    Otvorena=false

                },
                new StatusOfComplaint
                {
                    Status_zalbe = Guid.Parse("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                    Usvojena= true,
                    Odbijena=false,
                    Otvorena=true
                }
            });

        }
        public StatusOfComplaint CreateStatus(StatusOfComplaint statusComplaint)
        {
            statusComplaint.Status_zalbe = Guid.NewGuid();
            ListOfComplainations.Add(statusComplaint);
            StatusOfComplaint action = GetStatusById(statusComplaint.Status_zalbe);

            return new StatusOfComplaint
            {
                Status_zalbe = action.Status_zalbe,
                Usvojena = action.Usvojena,
                Odbijena = action.Odbijena,
                Otvorena = action.Otvorena,

            };
        }

        public void DeleteStatus(Guid Status_id)
        {
            ListOfComplainations.Remove
                (ListOfComplainations.FirstOrDefault(e => e.Status_zalbe == Status_id));

        }

        public List<StatusOfComplaint> GetStatus()
        {
            return (from e in ListOfComplainations
                    select e).ToList();
        }

        public StatusOfComplaint GetStatusById(Guid Status_id)
        {
            return ListOfComplainations.FirstOrDefault(e => e.Status_zalbe == Status_id);

        }

        public StatusOfComplaint UpdateStatus(StatusOfComplaint statusComplaint)
        {
            StatusOfComplaint action = GetStatusById(statusComplaint.Status_zalbe);

            action.Status_zalbe = statusComplaint.Status_zalbe;
            action.Usvojena = statusComplaint.Usvojena;
            action.Odbijena = statusComplaint.Odbijena;
            action.Otvorena = statusComplaint.Otvorena;

            return new StatusOfComplaint
            {
                Status_zalbe = action.Status_zalbe,
                Usvojena = action.Usvojena,
                Odbijena = action.Odbijena,
                Otvorena = action.Otvorena,

            };
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
