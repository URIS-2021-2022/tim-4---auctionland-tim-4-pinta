using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    //Vracamo podatke o javnom nadmetanju i kreiramo nova javna nadmetanja
    public class JavnoNadmetanjeModel 
    {
       
        public Guid JavnoNadmetanjeID { get; set; }
        public DateTime Datum { get; set; }
        public DateTime VremePocetka { get; set; }
        public DateTime VremeKraja { get; set; }
        public int PocetnaCenaPoHektaru { get; set; }
        public int  PeriodZakupa { get; set; }
        public Boolean Izuzeto { get; set; }
        public int[] Tip { get; set; }
        public Enum Status { get; set; }
        public int Krug { get; set; }
        public int VisinaDopuneDepozita { get; set; }
    }
}
