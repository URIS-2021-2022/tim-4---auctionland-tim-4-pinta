using System;
using System.Collections.Generic;
using System.Linq;
using Uplata.Entities;
using System.Threading.Tasks;

namespace Uplata.Data
{
    public class UplataMockRepository : IUplataRepository
    {
        public static List<UplataModel> uplate { get; set; } = new List<UplataModel>();

        public UplataMockRepository()
        {
            FillData();
        }

        public void FillData()
        {
            uplate.AddRange(new List<UplataModel>
            {
                new UplataModel
                {
                    UplataID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Iznos   = "150000",
                    Datum  = DateTime.Now,
                    SvrhaUplate   = "ucesce na licitaciji",
                    PozivNaBroj   = "3121-424324523-444",
                    KupacID   = Guid.Parse("6a411c23-a195-48f7-8dbd-67596c3974c0"),
                    JavnoNadmetanjeID = Guid.Parse("6a411c23-a192-48f7-8dbd-67596c3974c0")
                },
                new UplataModel
                {
                    UplataID = Guid.Parse("7a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Iznos   = "200000",
                    Datum  = DateTime.Now,
                    SvrhaUplate   = "ucesce na licitaciji",
                    PozivNaBroj   = "0242-424324523-444",
                    KupacID   = Guid.Parse("6a411c23-a195-48f7-8dbd-67596c3974c0"),
                    JavnoNadmetanjeID  = Guid.Parse("6a411c23-a192-48f7-8dbd-67596c3974c0")
                }
            });
        }

        public List<UplataModel> GetUplate()
        {
            return (from u in uplate select u).ToList();
        }

        public UplataModel GetUplataByID(Guid uplataID)
        {
            return uplate.FirstOrDefault(u => u.UplataID == uplataID);
        }

        public UplataModel CreateUplata(UplataModel uplataModel)
        {

            uplate.Add(uplataModel);
            UplataModel licitacija = GetUplataByID(uplataModel.UplataID);

            return new UplataModel
            {
                UplataID = licitacija.UplataID,
                Iznos = licitacija.Iznos,
                SvrhaUplate = licitacija.SvrhaUplate,
                Datum = licitacija.Datum,
                PozivNaBroj = licitacija.PozivNaBroj,
                KupacID = licitacija.KupacID,
                JavnoNadmetanjeID = licitacija.JavnoNadmetanjeID,
            };
        }
        public void DeleteUplata(Guid uplataID)
        {
            uplate.Remove(uplate.FirstOrDefault(u => u.UplataID == uplataID));
        }
        public UplataModel UpdateUplata(UplataModel uplataModel)
        {
            UplataModel uplata = GetUplataByID(uplataModel.UplataID);
            uplata.UplataID = uplataModel.UplataID;
            uplata.Iznos = uplataModel.Iznos;
            uplata.SvrhaUplate = uplataModel.SvrhaUplate;
            uplata.Datum = uplataModel.Datum;
            uplata.PozivNaBroj = uplataModel.PozivNaBroj;
            uplata.KupacID = uplataModel.KupacID;
            uplata.JavnoNadmetanjeID = uplataModel.JavnoNadmetanjeID;

            return uplata;
        }




    }
}
