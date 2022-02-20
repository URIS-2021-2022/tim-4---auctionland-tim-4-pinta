using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    //Vracamo podatke o javnom nadmetanju i kreiramo nova javna nadmetanja
    //u kom formatu ce biti podaci koje vracamo

    /// <summary>
    /// DTO za javno nadmetanje
    /// </summary>
    public class JavnoNadmetanjeUplateDto
    {
        public Guid JavnoNadmetanjeID { get; set; }
        /// <summary>
        /// Vreme pocetka
        /// </summary>
        public DateTime VremePocetka { get; set; }
        /// <summary>
        /// Vreme kraja
        /// </summary>
        public DateTime VremeKraja { get; set; }
        /// <summary>
        /// Broj ucesnika na javnom nadmetanju
        /// </summary>
        public int BrojUcesnika { get; set; }
        /// <summary>
        /// Izlicitirana cena na javnom nadmetanju
        /// </summary>
        public int IzlicitiranaCena { get; set; }
        /// <summary>
        /// Adresa odrzavanja javnog nadmetanja
        /// </summary>
        public string AdresaOdrzavanja { get; set; }
        /// <summary>
        /// ID statusa javnog nadmetanja
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// ID tipa javnog nadmetanja
        /// </summary>
        public string Tip { get; set; }
        /// <summary>
        /// ID sluzbenog lista
        /// </summary>
        public string SluzbeniList { get; set; }
    }
}
