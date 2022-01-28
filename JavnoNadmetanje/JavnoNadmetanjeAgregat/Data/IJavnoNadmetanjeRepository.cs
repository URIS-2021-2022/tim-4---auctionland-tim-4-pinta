using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Data
{
     //potpise metoda koje se koriste za manipulacije podacima, klasa ce implementirati interfejs
    public interface IJavnoNadmetanjeRepository
    {
        List<JavnoNadmetanjeModel> GetJavnoNadmetanje(); //moze da vrati javna nadmetanja sa filterima

        JavnoNadmetanjeModel GetJavnoNadmetanjeById(Guid javnoNadmetanjeID); 

        JavnoNadmetanjeModel CreateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje); 

        JavnoNadmetanjeModel UpdateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje);

        void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID);
    }
}
