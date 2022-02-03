using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Entities
{
    /// <summary>
    /// Predstavlja model korisnika
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID korisnika
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime korinsika
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Korisnicko ime
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email korisnika
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hash-ovana lozinka korinsika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
    }
}
