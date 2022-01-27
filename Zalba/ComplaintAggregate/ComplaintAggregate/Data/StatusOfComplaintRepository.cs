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

        private void FillData()
        {
            ListOfComplainations.AddRange(new List<StatusOfComplaint>
            {
                new StatusOfComplaint
                {
                    Status_zalbe = Guid.NewGuid(),
                    Usvojena= false,
                    Odbijena=true,
                    Otvorena=false

                },
                new StatusOfComplaint
                {
                    Status_zalbe = Guid.NewGuid(),
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
    }
}
