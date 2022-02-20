using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ObradivostRepository : IObradivostRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;

        public ObradivostRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ObradivostEntity CreateObradivost(ObradivostEntity obradivost)
        {
            obradivost.ObradivostID = Guid.NewGuid();
            context.Obradivosti.Add(obradivost);
            return obradivost;
        }

        public void DeleteObradivost(Guid obradivostID)
        {
            context.Obradivosti.Remove(context.Obradivosti.FirstOrDefault(o => o.ObradivostID == obradivostID));
        }

        public ObradivostEntity GetObradivostById(Guid obradivostID)
        {
            return context.Obradivosti.FirstOrDefault(o => o.ObradivostID == obradivostID);
        }

        public List<ObradivostEntity> GetObradivosti()
        {
            return (from o in context.Obradivosti select o).ToList();
        }

        public void UpdateObradivost(ObradivostEntity obradivost)
        {
            
        }
    }
}
