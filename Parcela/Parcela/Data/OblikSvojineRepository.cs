﻿using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OblikSvojineRepository : IOblikSvojineRepository
    {
        public static List<OblikSvojineModel> ObliciSvojine { get; set; } = new List<OblikSvojineModel>();

        public OblikSvojineRepository()
        {
            FillData();
        }

        private void FillData()
        {
            ObliciSvojine.AddRange(new List<OblikSvojineModel>
            {
                new OblikSvojineModel
                {
                    OblikSvojineID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    OblikSvojineNaziv = "Oblik svojine 1"
                },
                new OblikSvojineModel
                {
                    OblikSvojineID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    OblikSvojineNaziv = "Oblik svojine 2"
                }
            });
        }

        public OblikSvojineModel CreateOblikSvojine(OblikSvojineModel oblikSvojine)
        {
            oblikSvojine.OblikSvojineID = Guid.NewGuid();
            ObliciSvojine.Add(oblikSvojine);
            OblikSvojineModel os = GetOblikSvojineById(oblikSvojine.OblikSvojineID);
            return os;
        }

        public void DeleteOblikSvojine(Guid oblikSvojineID)
        {
            ObliciSvojine.Remove(ObliciSvojine.FirstOrDefault(os => os.OblikSvojineID == oblikSvojineID));
        }

        public List<OblikSvojineModel> GetObliciSvojine()
        {
            return (from os in ObliciSvojine select os).ToList();
        }

        public OblikSvojineModel GetOblikSvojineById(Guid oblikSvojineID)
        {
            return ObliciSvojine.FirstOrDefault(os => os.OblikSvojineID == oblikSvojineID);
        }

        public OblikSvojineModel UpdateOblikSvojine(OblikSvojineModel oblikSvojine)
        {
            OblikSvojineModel os = GetOblikSvojineById(oblikSvojine.OblikSvojineID);

            os.OblikSvojineNaziv = oblikSvojine.OblikSvojineNaziv;

            return os;
        }
    }
}
