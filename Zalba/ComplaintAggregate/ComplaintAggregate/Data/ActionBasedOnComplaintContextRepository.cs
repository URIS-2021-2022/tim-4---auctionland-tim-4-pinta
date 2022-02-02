using AutoMapper;
using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class ActionBasedOnComplaintContextRepository : IActionBasedOnComplaintRepository
    {

        private readonly ComplaintAggregateContext context;
        private readonly IMapper mapper;

        public ActionBasedOnComplaintContextRepository(ComplaintAggregateContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ActionBasedOnComplaint CreateAction(ActionBasedOnComplaint actionBasedOnComplaint)
        {
            var createdEntity = context.Add(actionBasedOnComplaint);
            return mapper.Map<ActionBasedOnComplaint>(createdEntity.Entity);
        }

        public void DeleteAction(Guid Radnja_na_osnovu_zalbe_ID)
        {
            var radnja = GetActionById(Radnja_na_osnovu_zalbe_ID);
            context.Remove(radnja);
        }

        public ActionBasedOnComplaint GetActionById(Guid Radnja_na_osnovu_zalbe_ID)
        {
            return context.Actions.FirstOrDefault(e => e.Radnja_na_osnovu_zalbe_ID == Radnja_na_osnovu_zalbe_ID);

        }

        public List<ActionBasedOnComplaint> GetActions()
        {
            return context.Actions.ToList();
        }

        public ActionBasedOnComplaint UpdateAction(ActionBasedOnComplaint actionBasedOnComplaint)
        {
            throw new NotImplementedException();
        }
    }
}
