using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public interface IStatusOfComplaintRepository
    {
        List<StatusOfComplaint> GetStatus();
        StatusOfComplaint GetStatusById(Guid Status_id);
        StatusOfComplaint CreateStatus(StatusOfComplaint statusComplaint);
        StatusOfComplaint UpdateStatus(StatusOfComplaint statusComplaint);
        void DeleteStatus(Guid Status_id);
    }
}
