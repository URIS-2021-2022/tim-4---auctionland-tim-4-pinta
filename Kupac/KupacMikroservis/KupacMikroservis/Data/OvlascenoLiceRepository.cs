using KupacMikroservis.Data;
using KupacMikroservis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OvlascenoLiceRepository : IOvlascenoLiceRepository
{
    public static List<OvlascenoLiceModel> OvlascenaLica { get; set; } = new List<OvlascenoLiceModel>();

    public OvlascenoLiceRepository()
    {
        FillData();
    }

    private void FillData()
    {
        OvlascenaLica.AddRange(new List<OvlascenoLiceModel>
               {
                   new OvlascenoLiceModel
                   {
                       OvlascenoLiceId = Guid.Parse("6a412c13-a185-58f7-8dbd-67596c3974c0"),
                       Ime = "Marko",
                       Prezime = "Markovic",
                       BrojLicnogDokumenta ="2435213345",
                       BrojTable = new ArrayList{453423,654,213}
                       

                   },
                   new OvlascenoLiceModel
                   {
                       OvlascenoLiceId = Guid.Parse("6a412c13-a185-58f7-8dcd-67596c3974c0"),
                       Ime = "Luka",
                       Prezime = "Lukovic",
                       BrojLicnogDokumenta ="352467865",
                       BrojTable = new ArrayList{64342,1324,676}
                   }
               });
    }
    public OvlascenoLiceModel CreateOvlascenoLice(OvlascenoLiceModel ovlascenolice)
    {
        ovlascenolice.OvlascenoLiceId = Guid.NewGuid();
        OvlascenaLica.Add(ovlascenolice);
        OvlascenoLiceModel ol = GetOvlascenoLiceById(ovlascenolice.OvlascenoLiceId);
        return ol;
    }

    public void DeleteOvlascenoLice(Guid ovlascenoliceID)
    {
        OvlascenaLica.Remove(OvlascenaLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID));
    }

    public List<OvlascenoLiceModel> GetOvlascenaLica()
    {
        return (from ol in OvlascenaLica select ol).ToList();
    }

    public OvlascenoLiceModel GetOvlascenoLiceById(Guid ovlascenoliceID)
    {
        return OvlascenaLica.FirstOrDefault(ol => ol.OvlascenoLiceId == ovlascenoliceID);
    }

    public OvlascenoLiceModel UpdateOvlascenoLice(OvlascenoLiceModel ovlascenolice)
    {
        OvlascenoLiceModel ol = GetOvlascenoLiceById(ovlascenolice.OvlascenoLiceId);

        // dp.RedniBroj = deoParcele.RedniBroj;
        // dp.PovrsinaDelaParcele = deoParcele.PovrsinaDelaParcele;

        return ol;
    }
}
