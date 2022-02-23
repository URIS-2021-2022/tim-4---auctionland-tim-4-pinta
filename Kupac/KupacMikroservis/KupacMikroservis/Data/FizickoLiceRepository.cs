using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KupacMikroservis.Data
{
    public class FizickoLiceRepository : IFizickoLiceRepository
    {


        private readonly KupacContext context;

        private readonly IMapper mapper;

        public FizickoLiceRepository(KupacContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }



        public FizickoLiceEntity CreateFizickoLice(FizickoLiceEntity fizLice)
        {
            var createdEntity = context.Add(fizLice);
            context.SaveChanges();
            return mapper.Map<FizickoLiceEntity>(createdEntity.Entity);
            



        }

        public void DeleteFizickoLice(Guid fizLiceID)
        {
            var fizlice = GetFizickoLiceById(fizLiceID);
            context.Remove(fizlice);
            context.SaveChanges();


        }

        public List<FizickoLiceEntity> GetFizickaLica()
        {
            return context.fLica.ToList();

        }

        public FizickoLiceEntity GetFizickoLiceById(Guid fizLiceID)
        {

            return context.fLica.FirstOrDefault(fl => fl.KupacId == fizLiceID);

        }

        public void UpdateFizickoLice(FizickoLiceEntity fizLice)
        {


        }
    }
}
