using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
   public interface IFizickoLiceRepository
    {
        List<FizickoLiceEntity> GetFizickaLica();

        FizickoLiceEntity GetFizickoLiceById(Guid fizickoLiceID);

        FizickoLiceEntity CreateFizickoLice(FizickoLiceEntity fizickoLice);

        FizickoLiceEntity UpdateFizickoLice(FizickoLiceEntity fizickoLice);

        void DeleteFizickoLice(Guid fizickoLiceID);
    }
}