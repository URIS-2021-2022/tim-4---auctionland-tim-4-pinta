using AutoMapper;
using Uplata.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Data
{
    public class UplataRepository : IUplataRepository
    {
        private readonly UplataContext context;
        private readonly IMapper mapper;

        public UplataRepository(UplataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public UplataEntity CreateUplata(UplataEntity uplata)
        {
            uplata.UplataID = Guid.NewGuid();
            context.Uplate.Add(uplata);
            return uplata;
        }

        public void DeleteUplata(Guid uplataID)
        {
            context.Uplate.Remove(context.Uplate.FirstOrDefault(p => p.UplataID == uplataID));
        }

        public UplataEntity GetUplataByID(Guid uplataID)
        {
            return context.Uplate.FirstOrDefault(p => p.UplataID == uplataID);
        }

        public List<UplataEntity> GetUplate()
        {
            return (from p in context.Uplate select p).ToList();
        }

        public void UpdateUplata(UplataEntity uplata)
        {
            throw new NotImplementedException();
        }
    }
}