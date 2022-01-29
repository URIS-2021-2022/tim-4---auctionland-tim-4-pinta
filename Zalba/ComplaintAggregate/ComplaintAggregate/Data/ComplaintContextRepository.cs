using AutoMapper;
using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class ComplaintContextRepository : IComplaintRepository
    {
        private readonly ComplaintAggregateContext context;
        private readonly IMapper mapper;

        public ComplaintContextRepository(ComplaintAggregateContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Complaint CreateComplaint(Complaint complainAggregate)
        {
            var createdEntity = context.Add(complainAggregate);
            return mapper.Map<Complaint>(createdEntity.Entity);
        }

        public void DeleteComplaint(Guid ZalbaID)
        {
            var zalba = GetComplaintById(ZalbaID);
            context.Remove(zalba);
        }

        public List<Complaint> GetComplaint()
        {
            return context.Complaints.ToList();
        }

        public Complaint GetComplaintById(Guid ZalbaId)
        {
            return context.Complaints.FirstOrDefault(e => e.ZalbaID == ZalbaId);
        }

        public Complaint UpdateComplaint(Complaint complainAggregate)
        {
            throw new NotImplementedException();
        }
    }


     
    }

