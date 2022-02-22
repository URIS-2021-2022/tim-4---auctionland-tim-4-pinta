using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KulturaRepository : IKulturaRepository
    {
        private readonly ParcelaContext context;

        public KulturaRepository(ParcelaContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public KulturaEntity CreateKultura(KulturaEntity kultura)
        {
            kultura.KulturaID = Guid.NewGuid();
            context.Kulture.Add(kultura);
            return kultura;
        }

        public void DeleteKultura(Guid kulturaID)
        {
            context.Kulture.Remove(context.Kulture.FirstOrDefault(k => k.KulturaID == kulturaID));
        }

        public KulturaEntity GetKulturaById(Guid kulturaID)
        {
            return context.Kulture.FirstOrDefault(k => k.KulturaID == kulturaID);
        }

        public List<KulturaEntity> GetKulture()
        {
            return (from k in context.Kulture select k).ToList();
        }

        public void UpdateKultura(KulturaEntity kultura)
        {
            
        }
    }
}
