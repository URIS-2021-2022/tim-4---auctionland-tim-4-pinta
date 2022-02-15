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

        public UplataModel CreateUplata(UplataModel uplata)
        {
            uplata.UplataID = Guid.NewGuid();
            context.Uplate.Add(uplata);
            UplataModel u = GetUplataByID(uplata.UplataID);
            return u;
        }

        public void DeleteUplata(Guid uplataID)
        {
            context.Uplate.Remove(context.Uplate.FirstOrDefault(p => p.UplataID == uplataID));
        }

        public UplataModel GetUplataByID(Guid uplataID)
        {
            return context.Uplate.FirstOrDefault(p => p.UplataID == uplataID);
        }

        public List<UplataModel> GetUplate()
        {
            return (from u in context.Uplate select u).ToList();
        }

        public UplataModel UpdateUplata(UplataModel uplata)
        {
            throw new NotImplementedException();
        }
    }
}