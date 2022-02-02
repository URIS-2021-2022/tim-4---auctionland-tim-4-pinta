using JavnoNadmetanjeAgregat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
     //potpise metoda koje se koriste za manipulacije podacima, klasa ce implementirati interfejs
    public interface IJavnoNadmetanjeRepository
    {
        List<JavnoNadmetanjeEntity> GetJavnoNadmetanje(); //moze da vrati javna nadmetanja sa filterima

        JavnoNadmetanjeEntity GetJavnoNadmetanjeById(Guid javnoNadmetanjeID); 

        JavnoNadmetanjeEntity CreateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje); 

        JavnoNadmetanjeEntity UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje);

        void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID);
    }
}
