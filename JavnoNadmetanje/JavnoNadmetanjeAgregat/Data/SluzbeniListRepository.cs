using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        public static List<SluzbeniListModel> SluzbeniListovi { get; set; } = new List<SluzbeniListModel>();

        public SluzbeniListRepository()
        {
            FillData();
        }

        private void FillData()
        {
            SluzbeniListovi.AddRange(new List<SluzbeniListModel>
            {
                new SluzbeniListModel
                {
                  SluzbeniListID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                  Opstina= "Beograd",
                  BrojSluzbenogLista= 12,
                  DatumIzdavanjaSluzbenogLista= DateTime.Parse("27-01-2021")
                },
                new SluzbeniListModel
                {
                    SluzbeniListID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Opstina= "Novi Sad",
                    BrojSluzbenogLista= 13,
                    DatumIzdavanjaSluzbenogLista= DateTime.Parse("27-02-2021")

                }
            });
        }

        public List<SluzbeniListModel> GetSluzbeniList()
        {
            return (from sl in SluzbeniListovi select sl).ToList();
        }

        public SluzbeniListModel GetSluzbeniListById(Guid sluzbeniListID)
        {
            return SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID);
        }

        public SluzbeniListModel CreateSluzbeniList(SluzbeniListModel sluzbeniList)
        {
            sluzbeniList.SluzbeniListID = Guid.NewGuid();
            SluzbeniListovi.Add(sluzbeniList);
            SluzbeniListModel sl = GetSluzbeniListById(sluzbeniList.SluzbeniListID);
            return sl;
        }

        public SluzbeniListModel UpdateSluzbeniList(SluzbeniListModel sluzbeniList)
        {
            SluzbeniListModel sl = GetSluzbeniListById(sluzbeniList.SluzbeniListID);

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
