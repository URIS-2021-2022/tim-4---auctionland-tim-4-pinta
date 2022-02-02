using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
        private readonly ParcelaContext context;
        private readonly IMapper mapper;

        public ParcelaRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ParcelaEntity CreateParcela(ParcelaEntity parcela)
        {
            parcela.ParcelaID = Guid.NewGuid();
            context.Parcele.Add(parcela);
            ParcelaEntity p = GetParcelaById(parcela.ParcelaID);
            return p;
        }

        public void DeleteParcela(Guid parcelaID)
        {
            context.Parcele.Remove(context.Parcele.FirstOrDefault(p => p.ParcelaID == parcelaID));
        }

        public ParcelaEntity GetParcelaById(Guid parcelaID)
        {
            return context.Parcele.FirstOrDefault(p => p.ParcelaID == parcelaID);
        }

        public List<ParcelaEntity> GetParcele()
        {
            return (from p in context.Parcele select p).ToList();
        }

        public ParcelaEntity UpdateParcela(ParcelaEntity parcela)
        {
            throw new NotImplementedException();
        }
    }
}
