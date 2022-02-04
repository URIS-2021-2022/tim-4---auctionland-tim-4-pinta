using AdresaServis.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Data
{
    public class DrzavaRepository : IDrzavaRepository
    {
        private readonly AdresaContext context;
        private readonly IMapper mapper;

        public DrzavaRepository(AdresaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public DrzavaEntity CreateDrzava(DrzavaEntity drzava)
        {
            drzava.DrzavaID = Guid.NewGuid();
            context.Drzave.Add(drzava);
            DrzavaEntity d = GetDrzavaById(drzava.DrzavaID);
            return d;
        }

        public void DeleteDrzava(Guid drzavaID)
        {
            context.Drzave.Remove(context.Drzave.FirstOrDefault(d => d.DrzavaID == drzavaID));
        }

        public List<DrzavaEntity> GetDrzave()
        {
            return (from d in context.Drzave select d).ToList();
        }

        public DrzavaEntity GetDrzavaById(Guid drzavaID)
        {
            return context.Drzave.FirstOrDefault(d => d.DrzavaID == drzavaID);
        }

        public DrzavaEntity UpdateDrzava(DrzavaEntity drzava)
        {
            throw new NotImplementedException();
        }
    }
}
