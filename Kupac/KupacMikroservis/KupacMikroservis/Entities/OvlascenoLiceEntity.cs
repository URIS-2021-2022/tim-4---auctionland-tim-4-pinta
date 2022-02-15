using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    public class OvlascenoLiceEntity
    {
        [Key]
        public Guid OvlascenoLiceId { get; set; }
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string BrojLicnogDokumenta { get; set; }

        public string BrojTable { get; set; }

        public Guid AdresaID { get; set; }

    }
}