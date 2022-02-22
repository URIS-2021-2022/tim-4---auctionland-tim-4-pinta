using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        private readonly ParcelaContext context;

        public DeoParceleRepository(ParcelaContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public DeoParceleEntity CreateDeoParcele(DeoParceleEntity deoParcele)
        {
            deoParcele.DeoParceleID = Guid.NewGuid();
            context.DeloviParcela.Add(deoParcele);
            return deoParcele;
        }

        public void DeleteDeoParcele(Guid deoParceleID)
        {
            context.DeloviParcela.Remove(context.DeloviParcela.FirstOrDefault(dp => dp.DeoParceleID == deoParceleID));
        }

        public List<DeoParceleEntity> GetDeloviParcela()
        {
            return (from dp in context.DeloviParcela select dp).ToList();
        }

        public DeoParceleEntity GetDeoParceleById(Guid deoParceleID)
        {
            return context.DeloviParcela.FirstOrDefault(dp => dp.DeoParceleID == deoParceleID);
        }

        public void UpdateDeoParcele(DeoParceleEntity deoParcele)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
