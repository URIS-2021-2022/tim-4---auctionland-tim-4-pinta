using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public interface IActionBasedOnComplaintRepository
    {
        List<ActionBasedOnComplaint> GetActions();
        ActionBasedOnComplaint GetActionById(Guid Radnja_na_osnovu_zalbe_ID);
        ActionBasedOnComplaint CreateAction(ActionBasedOnComplaint actionBasedOnComplaint);
        ActionBasedOnComplaint UpdateAction(ActionBasedOnComplaint actionBasedOnComplaint);
        void DeleteAction(Guid Radnja_na_osnovu_zalbe_ID);

    }
}
