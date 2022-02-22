using AutoMapper;
using Licitacija.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija.Data
{
    public class LicitacijaRepository : ILicitacijaRepository
    {
        private readonly LicitacijaContext context;
        private readonly IMapper mapper;

        public LicitacijaRepository(LicitacijaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public LicitacijaEntity CreateLicitacija(LicitacijaEntity licitacija)
        {
            licitacija.LicitacijaID = Guid.NewGuid();
            context.Licitacije.Add(licitacija);
            return licitacija;
        }

        public void DeleteLicitacija(Guid licitacijaID)
        {
            context.Licitacije.Remove(context.Licitacije.FirstOrDefault(l => l.LicitacijaID == licitacijaID));
        }

        public LicitacijaEntity GetLicitacijaByID(Guid licitacijaID)
        {
            return context.Licitacije.FirstOrDefault(p => p.LicitacijaID == licitacijaID);
        }

        public List<LicitacijaEntity> GetLicitacije()
        {
            return (from l in context.Licitacije select l).ToList();
        }

        public void UpdateLicitacija(LicitacijaEntity licitacija)
        {
            throw new NotImplementedException();
        }
    }
}