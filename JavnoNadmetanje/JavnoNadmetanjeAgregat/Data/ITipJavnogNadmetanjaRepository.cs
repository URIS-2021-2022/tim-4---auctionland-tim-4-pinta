using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public interface ITipJavnogNadmetanjaRepository
    {
        List<TipJavnogNadmetanjaModel> GetTipJavnogNadmetanja();

        TipJavnogNadmetanjaModel GetTipJavnogNadmetanjaById(Guid TipJavnogNadmetanjaID);

        TipJavnogNadmetanjaModel CreateTipJavnogNadmetanja(TipJavnogNadmetanjaModel tipJavnogNadmetanja);

        TipJavnogNadmetanjaModel UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaModel tipJavnogNadmetanja);

        void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID);
    }
}
