
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public interface ITipJavnogNadmetanjaRepository
    {
        List<TipJavnogNadmetanjaEntity> GetTipJavnogNadmetanja();

        TipJavnogNadmetanjaEntity GetTipJavnogNadmetanjaById(Guid TipJavnogNadmetanjaID);

        TipJavnogNadmetanjaEntity CreateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja);

        TipJavnogNadmetanjaEntity UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaEntity tipJavnogNadmetanja);

        void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID);
    }
}
