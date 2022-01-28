using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public interface IStatusJavnogNadmetanjaRepository
    {
        List<StatusJavnogNadmetanjaModel> GetStatusJavnogNadmetanja();

        StatusJavnogNadmetanjaModel GetStatusJavnogNadmetanjaById(Guid statusJavnogNadmetanjaID);

        StatusJavnogNadmetanjaModel CreateStatusJavnogNadmetanja(StatusJavnogNadmetanjaModel statusJavnogNadmetanja);

        StatusJavnogNadmetanjaModel UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaModel statusJavnogNadmetanja);

        void DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID);
    }
}
