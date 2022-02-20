using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OblikSvojineRepository : IOblikSvojineRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;

        public OblikSvojineRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public OblikSvojineEntity CreateOblikSvojine(OblikSvojineEntity oblikSvojine)
        {
            oblikSvojine.OblikSvojineID = Guid.NewGuid();
            context.ObliciSvojine.Add(oblikSvojine);
            return oblikSvojine;
        }

        public void DeleteOblikSvojine(Guid oblikSvojineID)
        {
            context.ObliciSvojine.Remove(context.ObliciSvojine.FirstOrDefault(os => os.OblikSvojineID == oblikSvojineID));
        }

        public List<OblikSvojineEntity> GetObliciSvojine()
        {
            return (from os in context.ObliciSvojine select os).ToList();
        }

        public OblikSvojineEntity GetOblikSvojineById(Guid oblikSvojineID)
        {
            return context.ObliciSvojine.FirstOrDefault(os => os.OblikSvojineID == oblikSvojineID);
        }

        public void UpdateOblikSvojine(OblikSvojineEntity oblikSvojine)
        {
            
        }
    }
}
