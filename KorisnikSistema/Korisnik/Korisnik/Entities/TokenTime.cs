using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Models
{
    public class TokenTime
    {

        [Key]
        public int tokenId { get; set; }

        public string token { get; set; }

        
        public int korisnikId{ get; set; }

        public DateTime time{ get; set; }
    }
}
