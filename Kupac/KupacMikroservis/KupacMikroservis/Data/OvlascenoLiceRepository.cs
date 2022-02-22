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
    public class OvlascenoLiceRepository : IOvlascenoLiceRepository
    {


        private readonly KupacContext context;

        private readonly IMapper mapper;

        public OvlascenoLiceRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public OvlascenoLiceEntity CreateOvlascenoLice(OvlascenoLiceEntity ovlascenoLice)
        {

            var createdEntity = context.Add(ovlascenoLice);
            return mapper.Map<OvlascenoLiceEntity>(createdEntity.Entity);

        }

        public void DeleteOvlascenoLice(Guid ovlascenoLiceID)
        {

            var ovlascenolice = GetOvlascenoLiceById(ovlascenoLiceID);
            context.Remove(ovlascenolice);


        }

        public List<OvlascenoLiceEntity> GetOvlascenaLica()
        {

            return context.oLica.ToList();


        }

        public OvlascenoLiceEntity GetOvlascenoLiceById(Guid ovlascenoLiceID)
        {
            return context.oLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoLiceID);


        }

        public void UpdateOvlascenoLice(OvlascenoLiceEntity ovlascenolice)
        {

        }
    }
}