//using JavnoNadmetanjeAgregat.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace JavnoNadmetanjeAgregat.Data
//{
//    public class SluzbeniListMockRepository : ISluzbeniListRepository
//    {
//        public static List<SluzbeniListEntity> SluzbeniListovi { get; set; } = new List<SluzbeniListEntity>();

//        public SluzbeniListMockRepository()
//        {
//            FillData();
//        }

//        private void FillData()
//        {
//            SluzbeniListovi.AddRange(new List<SluzbeniListEntity>
//            {
//                new SluzbeniListEntity
//                {
//                  SluzbeniListID = Guid.Parse("102E134D-FFDE-40FA-B355-F0B8BC52F886"),
//                  Opstina= "Beograd",
//                  BrojSluzbenogLista= 12,
//                  DatumIzdavanjaSluzbenogLista= DateTime.Parse("27-01-2021")
//                },
//                new SluzbeniListEntity
//                {
//                    SluzbeniListID = Guid.Parse("901B0AD2-6AA8-4076-8162-01B3A42F2A2E"),
//                    Opstina= "Novi Sad",
//                    BrojSluzbenogLista= 13,
//                    DatumIzdavanjaSluzbenogLista= DateTime.Parse("27-02-2021")

//                }
//            });
//        }

//        public List<SluzbeniListEntity> GetSluzbeniList()
//        {
//            return (from sl in SluzbeniListovi select sl).ToList();
//        }

//        public SluzbeniListEntity GetSluzbeniListById(Guid sluzbeniListID)
//        {
//            return SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID);
//        }

//        public SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList)
//        {
//            sluzbeniList.SluzbeniListID = Guid.NewGuid();
//            SluzbeniListovi.Add(sluzbeniList);
//            SluzbeniListEntity sl = GetSluzbeniListById(sluzbeniList.SluzbeniListID);
//            return sl;
//        }

//        public void UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
//        {
//            SluzbeniListEntity sl = GetSluzbeniListById(sluzbeniList.SluzbeniListID);

//            sl.Opstina = sluzbeniList.Opstina;
//            sl.BrojSluzbenogLista = sluzbeniList.BrojSluzbenogLista;
//            sl.DatumIzdavanjaSluzbenogLista = sluzbeniList.DatumIzdavanjaSluzbenogLista;
            
//        }

//        public void DeleteSluzbeniList(Guid sluzbeniListID)
//        {
//            SluzbeniListovi.Remove(SluzbeniListovi.FirstOrDefault(sl => sl.SluzbeniListID == sluzbeniListID));
//        }
//        public bool SaveChanges()
//        {
//            return true;
//        }
//    }
//}
