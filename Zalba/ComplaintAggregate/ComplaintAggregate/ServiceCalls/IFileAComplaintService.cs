using ComplaintAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComplaintAggregate.ServiceCalls
{
    public interface IFileAComplaintService
    {
        public bool FileAComplaint(Guid kupacId);
        public bool ConnectLogger(LogModel model);

        Task<HttpStatusCode> AuthorizeAsync(string token);


    }
}
