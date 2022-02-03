using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class FizickoLiceRepository : IFizickoLiceRepository
{
    public static List<FizickoLiceEntity> FizickaLica { get; set; } = new List<FizickoLiceEntity>();

    public FizickoLiceRepository()
    {
        FillData();
    }

    private void FillData()
    {
        FizickaLica.AddRange(new List<FizickoLiceEntity>
               {
                   new FizickoLiceEntity
                   {
                       KupacId = Guid.Parse("6a411c13-a195-58f7-8dbd-67576c3974c0"),
                       Naziv = "Pera Peric",
                       BrojTelefona1 = "021445566",
                       BrojTelefona2 = "021445576",
                       Email = "opera@gmail.com",
                       BrojRacuna = "236468776423",
                       ImaZabranu = false,
                       DatumPocetkaZabrane = DateTime.Now,
                       DuzinaTrajanjaZabraneUGodinama = 0,
                       DatumPrestankaZabrane = DateTime.Now,
                       JMBG = "349238138"

                   },
                   new FizickoLiceEntity
                   {
                       KupacId = Guid.Parse("6a421c13-b195-58f7-8dbd-37596c3974c0"),
                       Naziv = "Djura Djuric",
                       BrojTelefona1 = "021445563",
                       BrojTelefona2 = "021445571",
                       Email = "djura@gmail.com",
                       BrojRacuna = "236468346423",
                       ImaZabranu = false,
                       DatumPocetkaZabrane = DateTime.Now,
                       DuzinaTrajanjaZabraneUGodinama = 0,
                       DatumPrestankaZabrane = DateTime.Now,
                       JMBG = "689763528"
                   }
               });
    }
    
    public FizickoLiceEntity CreateFizickoLice(FizickoLiceEntity fizlice)
    {
        fizlice.KupacId = Guid.NewGuid();
        FizickaLica.Add(fizlice);
       FizickoLiceEntity fl = GetFizickoLiceById(fizlice.KupacId);
        return fl;
    }

    public void DeleteFizickoLice(Guid fizliceID)
    {
        FizickaLica.Remove(FizickaLica.FirstOrDefault(fl => fl.KupacId == fizliceID));
    }

    public List<FizickoLiceEntity> GetFizickaLica()
    {
        return (from fl in FizickaLica select fl).ToList();
    }

    public FizickoLiceEntity GetFizickoLiceById(Guid fizliceID)
    {
        return FizickaLica.FirstOrDefault(fl => fl.KupacId == fizliceID);
    }

    public FizickoLiceEntity UpdateFizickoLice(FizickoLiceEntity fizlice)
    {
        FizickoLiceEntity fl = GetFizickoLiceById(fizlice.KupacId);

         fl.Naziv = fizlice.Naziv;
        fl.BrojTelefona1 = fizlice.BrojTelefona1;
        fl.BrojTelefona2 = fizlice.BrojTelefona2;
        fl.Email = fizlice.Email;
        fl.BrojRacuna = fizlice.BrojRacuna;
        fl.ImaZabranu = fizlice.ImaZabranu;
        fl.DatumPocetkaZabrane = fizlice.DatumPocetkaZabrane;
        fl.DuzinaTrajanjaZabraneUGodinama = fizlice.DuzinaTrajanjaZabraneUGodinama;
        fl.DatumPrestankaZabrane = fizlice.DatumPrestankaZabrane;
        fl.JMBG = fizlice.JMBG;
        return fl;
    }
}
