using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
    public interface IPrioritetRepository
    {
        List<PrioritetModel> GetPrioriteti();

        PrioritetModel GetPrioritetById(Guid prioritetID);

        PrioritetModel CreatePrioritet(PrioritetModel prioritet);

        PrioritetModel UpdatePrioritet(PrioritetModel prioritet);

        void DeletePrioritet(Guid prioritetID);
    }
}
