using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
    public interface IPrioritetRepository
    {
        List<PrioritetEntity> GetPrioriteti();

        PrioritetEntity GetPrioritetById(Guid prioritetID);

        PrioritetEntity CreatePrioritet(PrioritetEntity prioritet);

        PrioritetEntity UpdatePrioritet(PrioritetEntity prioritet);

        void DeletePrioritet(Guid prioritetID);
    }
}
