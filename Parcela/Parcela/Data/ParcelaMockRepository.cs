﻿using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ParcelaMockRepository : IParcelaRepository
    {
        public static List<ParcelaEntity> Parcele { get; set; } = new List<ParcelaEntity>();

        public ParcelaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Parcele.AddRange(new List<ParcelaEntity>
            {
                new ParcelaEntity
                {
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Povrsina = 1000,
                    BrojParcele = "12345",
                    BrojListaNepokretnosti = "12345",
                    KulturaStvarnoStanje = "Kukuruz",
                    KlasaStvarnoStanje = "Klasa1",
                    ObradivostStvarnoStanje = "Obradivost1",
                    ZasticenaZonaStvarnoStanje = "ZasticenaZona1",
                    OdvodnjavanjeStvarnoStanje = "Odvodnjavanje1"
                },
                new ParcelaEntity
                {
                    ParcelaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Povrsina = 2000,
                    BrojParcele = "54321",
                    BrojListaNepokretnosti = "54321",
                    KulturaStvarnoStanje = "Soja",
                    KlasaStvarnoStanje = "Klasa2",
                    ObradivostStvarnoStanje = "Obradivost2",
                    ZasticenaZonaStvarnoStanje = "ZasticenaZona2",
                    OdvodnjavanjeStvarnoStanje = "Odvodnjavanje2"
                }
            });
        }

        public List<ParcelaEntity> GetParcele()
        {
            return (from p in Parcele select p).ToList();
        }

        public ParcelaEntity GetParcelaById(Guid parcelaID)
        {
            return Parcele.FirstOrDefault(p => p.ParcelaID == parcelaID);
        }

        public ParcelaEntity CreateParcela(ParcelaEntity parcela)
        {
            parcela.ParcelaID = Guid.NewGuid();
            Parcele.Add(parcela);
            ParcelaEntity p = GetParcelaById(parcela.ParcelaID);
            return p;
        }

        public ParcelaEntity UpdateParcela(ParcelaEntity parcela)
        {
            ParcelaEntity p = GetParcelaById(parcela.ParcelaID);

            p.Povrsina = parcela.Povrsina;
            p.BrojParcele = parcela.BrojParcele;
            p.BrojListaNepokretnosti = parcela.BrojListaNepokretnosti;
            p.KulturaStvarnoStanje = parcela.KulturaStvarnoStanje;
            p.KlasaStvarnoStanje = parcela.KlasaStvarnoStanje;
            p.ObradivostStvarnoStanje = parcela.ObradivostStvarnoStanje;
            p.ZasticenaZonaStvarnoStanje = parcela.ZasticenaZonaStvarnoStanje;
            p.OdvodnjavanjeStvarnoStanje = parcela.OdvodnjavanjeStvarnoStanje;

            return p;
        }

        public void DeleteParcela(Guid parcelaID)
        {
            Parcele.Remove(Parcele.FirstOrDefault(p => p.ParcelaID == parcelaID));
        }
    }
}
