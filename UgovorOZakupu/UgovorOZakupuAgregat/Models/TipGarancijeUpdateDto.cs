using System;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO za ažuriranje tipa garancije
    /// </summary>
    public class TipGarancijeUpdateDto
    {
        /// <summary>
        /// ID tipa garancije
        /// </summary>
        public Guid TipId { get; set; }

        /// <summary>
        /// Naziv tipa garancije
        /// </summary>
        public string Naziv { get; set; }
    }
}