using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public interface IComplaintRepository
    {
        List<Complaint> GetComplaint();
        Complaint GetComplaintById(Guid ZalbaId);
        Complaint CreateComplaint(Complaint complainAggregate);
        Complaint UpdateComplaint(Complaint complainAggregate);
        void DeleteComplaint(Guid ZalbaID);

        bool SaveChanges();
    }
}
