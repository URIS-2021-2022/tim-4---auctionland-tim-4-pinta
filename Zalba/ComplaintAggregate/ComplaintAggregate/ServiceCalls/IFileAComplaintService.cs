using ComplaintAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.ServiceCalls
{
    public interface IFileAComplaintService
    {
        public bool FileAComplaint(BuyerDTO buyer);
    }
}
