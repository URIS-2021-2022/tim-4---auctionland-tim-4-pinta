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
    public class KontaktOsobaRepository : IKontaktOsobaRepository
    {


        private readonly KupacContext context;

        private readonly IMapper mapper;


        public KontaktOsobaRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public KontaktOsobaEntity CreateKontaktOsoba(KontaktOsobaEntity kontaktOsoba)
        {

            var createdEntity = context.Add(kontaktOsoba);
            return mapper.Map<KontaktOsobaEntity>(createdEntity.Entity);

        }

        public void DeleteKontaktOsoba(Guid kontaktOsobaID)
        {

            var ko = GetKontaktOsoba(kontaktOsobaID);
            context.Remove(ko);


        }

        public List<KontaktOsobaEntity> GetKontaktOsobe()
        {

            return context.kOsobe.ToList();


        }

        public KontaktOsobaEntity GetKontaktOsoba(Guid kontaktOsobaID)
        {
            return context.kOsobe.FirstOrDefault(ko => ko.KontaktOsobaId == kontaktOsobaID);


        }

        public void UpdateKontaktOsoba(KontaktOsobaEntity kontaktOsoba)
        {

        }
    }
}