
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        public static List<SluzbeniListEntity> SluzbeniListovi { get; set; } = new List<SluzbeniListEntity>();

        public SluzbeniListRepository()
        {
            FillData();
        }

        private void FillData()
        {
            SluzbeniListovi.AddRange(new List<SluzbeniListEntity>
            {
                new SluzbeniListEntity
                {
                  SluzbeniListID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                  Opstina= "Beograd",
                  BrojSluzbenogLista= 12,
                  DatumIzdavanjaSluzbenogLista= DateTime.Parse("27-01-2021")
                },
                new SluzbeniListEntity
                {
                    SluzbeniListID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Opstina= "Novi Sad",
                    BrojSluzbenogLista= 13,
                    DatumIzdavanjaSluzbenogLista= DateTime.Parse("27-02-2021")

                }
            });
        }

        public List<SluzbeniListEntity> GetSluzbeniList()
        {
            return (from sl in SluzbeniListovi select sl).ToList();
        }

        public SluzbeniListEntity GetSluzbeniListById(Guid sluzbeniListID)
        {
            return SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID);
        }

        public SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            sluzbeniList.SluzbeniListID = Guid.NewGuid();
            SluzbeniListovi.Add(sluzbeniList);
            SluzbeniListEntity sl = GetSluzbeniListById(sluzbeniList.SluzbeniListID);
            return sl;
        }

        public SluzbeniListEntity UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            SluzbeniListEntity sl = GetSluzbeniListById(sluzbeniList.SluzbeniListID);

            sl.Opstina = sluzbeniList.Opstina;
            sl.BrojSluzbenogLista = sluzbeniList.BrojSluzbenogLista;
            sl.DatumIzdavanjaSluzbenogLista = sluzbeniList.DatumIzdavanjaSluzbenogLista;
            return sl;
        }

        public void DeleteSluzbeniList(Guid sluzbeniListID)
        {
            SluzbeniListovi.Remove(SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID));
        }
    }
}
