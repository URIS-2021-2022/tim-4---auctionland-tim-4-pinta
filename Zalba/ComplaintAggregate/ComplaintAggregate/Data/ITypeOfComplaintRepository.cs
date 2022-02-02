using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
   public  interface ITypeOfComplaintRepository
    {
        List<TypeOfComplaint> GetTypesOfComplaints();
        TypeOfComplaint GetTypesOfComplaintsById(Guid Tip_id);
        TypeOfComplaint CreateTypeOfComplaint(TypeOfComplaint typeOfComplaint);
        TypeOfComplaint UpdateTypeOfComplaint(TypeOfComplaint typeOfComplaint);
        void DeleteTypeOfComplaint(Guid Tip_id);
    }
}
