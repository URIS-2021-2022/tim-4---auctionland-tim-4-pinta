using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KupacMikroservis.Data
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {


        private readonly KupacContext context;

        private readonly IMapper mapper;

        public PravnoLiceRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public PravnoLiceEntity CreatePravnoLice(PravnoLiceEntity pravnolice)
        {
            var createdEntity = context.Add(pravnolice);
            context.SaveChanges();
            return mapper.Map<PravnoLiceEntity>(createdEntity.Entity);



        }

        public void DeletePravnoLice(Guid pravnoliceID)
        {
            var pravnolice = GetPravnoLiceById(pravnoliceID);
            context.SaveChanges();
            context.Remove(pravnolice);


        }

        public List<PravnoLiceEntity> GetPravnaLica()
        {
            return context.pLica.ToList();


        }

        public PravnoLiceEntity GetPravnoLiceById(Guid pravnoliceID)
        {
            return context.pLica.FirstOrDefault(pl => pl.KupacId == pravnoliceID);


        }

        public void UpdatePravnoLice(PravnoLiceEntity pravnolice)
        {


        }
    }

}