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
        private readonly IMapper mapper;

        public DeoParceleRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public DeoParceleEntity CreateDeoParcele(DeoParceleEntity deoParcele)
        {
            deoParcele.DeoParceleID = Guid.NewGuid();
            context.DeloviParcela.Add(deoParcele);
            DeoParceleEntity dp = GetDeoParceleById(deoParcele.DeoParceleID);
            return dp;
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

        public DeoParceleEntity UpdateDeoParcele(DeoParceleEntity deoParcele)
        {
            throw new NotImplementedException();
        }
    }
}
