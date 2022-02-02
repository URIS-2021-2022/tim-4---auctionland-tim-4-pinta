using AutoMapper;
using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class StatusOfComplaintContextRepository : IStatusOfComplaintRepository
    {
        private readonly ComplaintAggregateContext context;
        private readonly IMapper mapper;

        public StatusOfComplaintContextRepository(ComplaintAggregateContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public StatusOfComplaint CreateStatus(StatusOfComplaint statusComplaint)
        {
            var createdEntity = context.Add(statusComplaint);
            return mapper.Map<StatusOfComplaint>(createdEntity.Entity);
        }

        public void DeleteStatus(Guid Status_id)
        {
            var status = GetStatusById(Status_id);
            context.Remove(status);
        }

        public List<StatusOfComplaint> GetStatus()
        {
            return context.Status.ToList();
        }

        public StatusOfComplaint GetStatusById(Guid Status_id)
        {
            return context.Status.FirstOrDefault(e => e.Status_zalbe == Status_id);

        }

        public StatusOfComplaint UpdateStatus(StatusOfComplaint statusComplaint)
        {
            throw new NotImplementedException();
        }
    }
}
