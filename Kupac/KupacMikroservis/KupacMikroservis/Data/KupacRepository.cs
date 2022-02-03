using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class KupacRepository : IKupacRepository
{
    public static List<KupacEntity> Kupci { get; set; } = new List<KupacEntity>();
    public static List<PravnoLiceEntity> PravnaLica { get; set; } = new List<PravnoLiceEntity>();
    public static List<FizickoLiceEntity> FizLica { get; set; } = new List<FizickoLiceEntity>();

    public KupacRepository()
    {
        FillData();
    }

    private void FillData()
    {
        Kupci.AddRange(new List<KupacEntity>
               {
                   new KupacEntity
                   {
                       KupacId = Guid.Parse("1a412c13-a165-58f7-8dbd-67596c3974c0"),
                       Naziv = "Mika Petrovic",
                       BrojTelefona1 = "021415513",
                       BrojTelefona2 = "0214255731",
                       Email = "mkp@gmail.com",
                       BrojRacuna = "65543227654",
                       ImaZabranu = false,
                       DatumPocetkaZabrane = DateTime.Now,
                       DuzinaTrajanjaZabraneUGodinama = 0,
                       DatumPrestankaZabrane = DateTime.Now,
                       

                   },
                   new KupacEntity
                   {
                       KupacId = Guid.Parse("2a213c13-b195-58f7-8dbd-67596c3974c0"),
                       Naziv = "Petar Mikic",
                       BrojTelefona1 = "021145512",
                       BrojTelefona2 = "021945521",
                       Email = "petarm@gmail.com",
                       BrojRacuna = "1234567890",
                       ImaZabranu = false,
                       DatumPocetkaZabrane = DateTime.Now,
                       DuzinaTrajanjaZabraneUGodinama = 0,
                       DatumPrestankaZabrane = DateTime.Now,
                      
                   }
               });
    }
    public KupacEntity CreateKupac(KupacEntity kupac) { 
        kupac.KupacId = Guid.NewGuid();
        Kupci.Add(kupac);
        KupacEntity k = GetKupacById(kupac.KupacId);
        return k;
    }

    public void DeleteKupac(Guid kupacID)
    {
        Kupci.Remove(Kupci.FirstOrDefault(k => k.KupacId == kupacID));
    }

    public List<KupacEntity> GetKupci()
    {
        PravnaLica = (from pl in PravnaLica select pl).ToList();
        FizLica = (from fl in FizLica select fl).ToList();

        Kupci.AddRange(PravnaLica);
        Kupci.AddRange(FizLica);



        return Kupci;
    }

    public KupacEntity GetKupacById(Guid kupacID)
    {
        return Kupci.FirstOrDefault(k => k.KupacId == kupacID);
    }

    public KupacEntity UpdateKupac(KupacEntity kupac)
    {
        KupacEntity k = GetKupacById(kupac.KupacId);
        
         k.Naziv = kupac.Naziv;
        k.BrojTelefona1 = kupac.BrojTelefona1;
        k.BrojTelefona2 = kupac.BrojTelefona2;
        k.Email = kupac.Email;
        k.BrojRacuna = kupac.BrojRacuna;
        k.ImaZabranu = kupac.ImaZabranu;
        k.DatumPocetkaZabrane = kupac.DatumPocetkaZabrane;
        k.DuzinaTrajanjaZabraneUGodinama = kupac.DuzinaTrajanjaZabraneUGodinama;
        k.DatumPrestankaZabrane = kupac.DatumPrestankaZabrane;
       


        return k;
    }
}
