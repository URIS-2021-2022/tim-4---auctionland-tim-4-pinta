using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
   public interface IFizickoLiceRepository
    {
        List<FizickoLiceModel> GetFizickaLica();

        FizickoLiceModel GetFizickoLiceById(Guid fizickoLiceID);

        FizickoLiceModel CreateFizickoLice(FizickoLiceModel fizickoLice);

        FizickoLiceModel UpdateFizickoLice(FizickoLiceModel fizickoLice);

        void DeleteFizickoLice(Guid fizickoLiceID);
    }
}