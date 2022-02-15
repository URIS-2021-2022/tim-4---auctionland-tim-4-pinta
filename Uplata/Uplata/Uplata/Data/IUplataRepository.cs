using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Models;
using Uplata.Entities;



namespace Uplata.Data
{
    public interface IUplataRepository
    {
        List<UplataModel> GetUplate();

        UplataModel GetUplataByID(Guid uplataID);

        UplataModel CreateUplata(UplataModel uplataModel);

        UplataModel UpdateUplata(UplataModel uplataModel);

        void DeleteUplata(Guid uplataID);
    }
}
