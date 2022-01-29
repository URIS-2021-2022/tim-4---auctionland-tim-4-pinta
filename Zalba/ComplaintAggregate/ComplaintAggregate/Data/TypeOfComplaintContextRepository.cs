using AutoMapper;
using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class TypeOfComplaintContextRepository : ITypeOfComplaintRepository
    {
        private readonly ComplaintAggregateContext context;
        private readonly IMapper mapper;

        public TypeOfComplaintContextRepository(ComplaintAggregateContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public TypeOfComplaint CreateTypeOfComplaint(TypeOfComplaint typeOfComplaint)
        {
            var createdEntity = context.Add(typeOfComplaint);
            return mapper.Map<TypeOfComplaint>(createdEntity.Entity);
        }

        public void DeleteTypeOfComplaint(Guid Tip_id)
        {
            var tip = GetTypesOfComplaintsById(Tip_id);
            context.Remove(tip);
        }

        public List<TypeOfComplaint> GetTypesOfComplaints()
        {
            return context.Types.ToList();
        }

        public TypeOfComplaint GetTypesOfComplaintsById(Guid Tip_id)
        {
            return context.Types.FirstOrDefault(e => e.Tip_id == Tip_id);

        }

        public TypeOfComplaint UpdateTypeOfComplaint(TypeOfComplaint typeOfComplaint)
        {
            throw new NotImplementedException();
        }
    }
}
