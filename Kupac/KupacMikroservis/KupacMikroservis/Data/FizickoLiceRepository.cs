using AutoMapper;
using KupacMikroservis.Data;
using KupacMikroservis.Entities;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class FizickoLiceRepository : IFizickoLiceRepository
{
    //   public static List<FizickoLiceEntity> FizickaLica { get; set; } = new List<FizickoLiceEntity>();


    private readonly KupacContext context;

    private readonly IMapper mapper;

    public FizickoLiceRepository(KupacContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public bool SaveChanges()
    {
        return context.SaveChanges() > 0;
    }

    
    
    public FizickoLiceEntity CreateFizickoLice(FizickoLiceEntity fizlice)
    {
        var createdEntity = context.Add(fizlice);
        return mapper.Map<FizickoLiceEntity>(createdEntity.Entity);


        /*   fizlice.KupacId = Guid.NewGuid();
           FizickaLica.Add(fizlice);
          FizickoLiceEntity fl = GetFizickoLiceById(fizlice.KupacId);
           return fl; */
    }

    public void DeleteFizickoLice(Guid fizliceID)
    {
        var fizlice = GetFizickoLiceById(fizliceID);
        context.Remove(fizlice);

        //  FizickaLica.Remove(FizickaLica.FirstOrDefault(fl => fl.KupacId == fizliceID));
    }

    public List<FizickoLiceEntity> GetFizickaLica()
    {
        return context.fLica.ToList();

        //  return (from fl in FizickaLica select fl).ToList();
    }

    public FizickoLiceEntity GetFizickoLiceById(Guid fizliceID)
    {

        return context.fLica.FirstOrDefault(fl => fl.KupacId == fizliceID);

        //  return FizickaLica.FirstOrDefault(fl => fl.KupacId == fizliceID);
    }

    public void UpdateFizickoLice(FizickoLiceEntity fizlice)
    {
        /* FizickoLiceEntity fl = GetFizickoLiceById(fizlice.KupacId);

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
         return fl;*/

        
    }
}
