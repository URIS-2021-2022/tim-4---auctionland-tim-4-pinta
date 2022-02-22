using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class PravnoLiceRepository : IPravnoLiceRepository
{
    //   public static List<PravnoLiceEntity> PravnaLica { get; set; } = new List<PravnoLiceEntity>();


    private readonly KupacContext context;

    private readonly IMapper mapper;

    public PravnoLiceRepository(KupacContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public bool SaveChanges()
    {
        return context.SaveChanges() > 0;
    }


    public PravnoLiceEntity CreatePravnoLice(PravnoLiceEntity pravnolice)
    {
        var createdEntity = context.Add(pravnolice);
        context.SaveChanges();
        return mapper.Map<PravnoLiceEntity>(createdEntity.Entity);


    /*    pravnolice.KupacId = Guid.NewGuid();
        PravnaLica.Add(pravnolice);
        PravnoLiceEntity pl = GetPravnoLiceById(pravnolice.KupacId);
        return pl;*/
    }

    public void DeletePravnoLice(Guid pravnoliceID)
    {
        var pravnolice = GetPravnoLiceById(pravnoliceID);
        context.SaveChanges();
        context.Remove(pravnolice);

      //  PravnaLica.Remove(PravnaLica.FirstOrDefault(pl => pl.KupacId == pravnoliceID));
    }

    public List<PravnoLiceEntity> GetPravnaLica()
    {
        return context.pLica.ToList();

        //  return (from pl in PravnaLica select pl).ToList();
    }

    public PravnoLiceEntity GetPravnoLiceById(Guid pravnoliceID)
    {
        return context.pLica.FirstOrDefault(pl => pl.KupacId == pravnoliceID);

        //  return PravnaLica.FirstOrDefault(pl => pl.KupacId == pravnoliceID);
    }

    public void UpdatePravnoLice(PravnoLiceEntity pravnolice)
    {
        /*   PravnoLiceEntity pl = GetPravnoLiceById(pravnolice.KupacId);

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


           return pl;*/
       
    }
}

