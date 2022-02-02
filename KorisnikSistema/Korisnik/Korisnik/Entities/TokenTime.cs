using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Models
{
    public class TokenTime
    {
        public Guid token { get; set; }

        public int KorisnikId{ get; set; }

        public string time{ get; set; }
    }
}
