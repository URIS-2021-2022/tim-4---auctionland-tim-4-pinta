using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class PravnoLiceRepository : IPravnoLiceRepository
{
    public static List<PravnoLiceModel> PravnaLica { get; set; } = new List<PravnoLiceModel>();

    public PravnoLiceRepository()
    {
        FillData();
    }

    private void FillData()
    {
        PravnaLica.AddRange(new List<PravnoLiceModel>
               {
                   new PravnoLiceModel
                   {
                       KupacId = Guid.Parse("6a412c13-a195-58f7-8dbd-67596c3974c0"),
                       Naziv = "Firma DOO",
                       BrojTelefona1 = "021415566",
                       BrojTelefona2 = "021425576",
                       Email = "fdoo@gmail.com",
                       BrojRacuna = "23189843223",
                       ImaZabranu = false,
                       DatumPocetkaZabrane = DateTime.Now,
                       DuzinaTrajanjaZabraneUGodinama = 0,
                       DatumPrestankaZabrane = DateTime.Now,
                       Faks = "45231423",
                       MaticniBroj = "34923023"

                   },
                   new PravnoLiceModel
                   {
                       KupacId = Guid.Parse("6a413c13-b195-58f7-8dbd-67596c3974c0"),
                       Naziv = "Kompanija DOO",
                       BrojTelefona1 = "021145563",
                       BrojTelefona2 = "021945571",
                       Email = "kdoo@gmail.com",
                       BrojRacuna = "23624242423",
                       ImaZabranu = false,
                       DatumPocetkaZabrane = DateTime.Now,
                       DuzinaTrajanjaZabraneUGodinama = 0,
                       DatumPrestankaZabrane = DateTime.Now,
                        Faks = "4535322",
                       MaticniBroj = "2113454465"
                   }
               });
    }
    public PravnoLiceModel CreatePravnoLice(PravnoLiceModel pravnolice)
    {
        pravnolice.KupacId = Guid.NewGuid();
        PravnaLica.Add(pravnolice);
        PravnoLiceModel pl = GetPravnoLiceById(pravnolice.KupacId);
        return pl;
    }

    public void DeletePravnoLice(Guid pravnoliceID)
    {
       PravnaLica.Remove(PravnaLica.FirstOrDefault(pl => pl.KupacId == pravnoliceID));
    }

    public List<PravnoLiceModel> GetPravnaLica()
    {
        return (from pl in PravnaLica select pl).ToList();
    }

    public PravnoLiceModel GetPravnoLiceById(Guid pravnoliceID)
    {
        return PravnaLica.FirstOrDefault(pl => pl.KupacId == pravnoliceID);
    }

    public PravnoLiceModel UpdatePravnoLice(PravnoLiceModel pravnolice)
    {
        PravnoLiceModel pl = GetPravnoLiceById(pravnolice.KupacId);

         pl.Naziv = pravnolice.Naziv;
        pl.BrojTelefona1 = pravnolice.BrojTelefona1;
        pl.BrojTelefona2 = pravnolice.BrojTelefona2;
        pl.Email = pravnolice.Email;
        pl.BrojRacuna = pravnolice.BrojRacuna;
        pl.ImaZabranu = pravnolice.ImaZabranu;
        pl.DatumPocetkaZabrane = pravnolice.DatumPocetkaZabrane;
        pl.DuzinaTrajanjaZabraneUGodinama = pravnolice.DuzinaTrajanjaZabraneUGodinama;
        pl.DatumPrestankaZabrane = pravnolice.DatumPrestankaZabrane;
        pl.Faks = pravnolice.Faks;
        pl.MaticniBroj = pravnolice.MaticniBroj;


        return pl;
    }
}

