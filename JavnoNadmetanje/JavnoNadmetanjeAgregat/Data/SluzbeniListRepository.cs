
using AutoMapper;
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        private readonly JavnoNadmetanjeContext context;
     

        public SluzbeniListRepository(JavnoNadmetanjeContext context)
        {

            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        public List<SluzbeniListEntity> GetSluzbeniList()
        {
            return (from sl in context.SluzbeniListovi select sl).ToList();
        }

        public SluzbeniListEntity GetSluzbeniListById(Guid sluzbeniListID)
        {
            return context.SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID);
        }

        public SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            sluzbeniList.SluzbeniListID = Guid.NewGuid();
            context.SluzbeniListovi.Add(sluzbeniList);
            return sluzbeniList;
        }

        public void UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            throw new NotImplementedException();
        }

        public void DeleteSluzbeniList(Guid sluzbeniListID)
        {
            context.SluzbeniListovi.Remove(context.SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID));
        }
    }
}
