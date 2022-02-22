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

        public OblikSvojineRepository(ParcelaContext context)
        {
            this.context = context;
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
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
