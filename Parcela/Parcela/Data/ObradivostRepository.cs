using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ObradivostRepository : IObradivostRepository
    {
        public static List<ObradivostModel> Obradivosti { get; set; } = new List<ObradivostModel>();

        public ObradivostRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Obradivosti.AddRange(new List<ObradivostModel>
            {
                new ObradivostModel
                {
                    ObradivostID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ObradivostNaziv = "Obradivost1"
                },
                new ObradivostModel
                {
                    ObradivostID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ObradivostNaziv = "Obradivost2"
                }
            });
        }

        public ObradivostModel CreateObradivost(ObradivostModel obradivost)
        {
            obradivost.ObradivostID = Guid.NewGuid();
            Obradivosti.Add(obradivost);
            ObradivostModel o = GetObradivostById(obradivost.ObradivostID);
            return o;
        }

        public void DeleteObradivost(Guid obradivostID)
        {
            Obradivosti.Remove(Obradivosti.FirstOrDefault(o => o.ObradivostID == obradivostID));
        }

        public ObradivostModel GetObradivostById(Guid obradivostID)
        {
            return Obradivosti.FirstOrDefault(o => o.ObradivostID == obradivostID);
        }

        public List<ObradivostModel> GetObradivosti()
        {
            return (from o in Obradivosti select o).ToList();
        }

        public ObradivostModel UpdateObradivost(ObradivostModel obradivost)
        {
            ObradivostModel o = GetObradivostById(obradivost.ObradivostID);

            o.ObradivostNaziv = obradivost.ObradivostNaziv;

            return o;
        }
    }
}
