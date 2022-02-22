using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KupacMikroservis.Data
{
    public class KupacRepository : IKupacRepository
    {


        private readonly KupacContext context;

        private readonly IMapper mapper;

        public KupacRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public KupacEntity CreateKupac(KupacEntity kupac)
        {

            var createdEntity = context.Add(kupac);
            return mapper.Map<KupacEntity>(createdEntity.Entity);


        }

        public void DeleteKupac(Guid kupacID)
        {
            var kupac = GetKupacById(kupacID);
            context.Remove(kupac);


        }

        public List<KupacEntity> GetKupci()
        {
            List<KupacEntity> list = new List<KupacEntity>();
            return list;

        }

        public KupacEntity GetKupacById(Guid kupacID)
        {


            return new KupacEntity();
        }

        public void UpdateKupac(KupacEntity kupac)
        {

        }
    }
}