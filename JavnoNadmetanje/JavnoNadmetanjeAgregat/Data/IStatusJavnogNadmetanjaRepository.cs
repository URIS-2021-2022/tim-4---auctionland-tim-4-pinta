
using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
    public interface IStatusJavnogNadmetanjaRepository
    {
        List<StatusJavnogNadmetanjaEntity> GetStatusJavnogNadmetanja();

        StatusJavnogNadmetanjaEntity GetStatusJavnogNadmetanjaById(Guid statusJavnogNadmetanjaID);

        StatusJavnogNadmetanjaEntity CreateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja);

        StatusJavnogNadmetanjaEntity UpdateStatusJavnogNadmetanja(StatusJavnogNadmetanjaEntity statusJavnogNadmetanja);

        void DeleteStatusJavnogNadmetanja(Guid statusJavnogNadmetanjaID);
    }
}
