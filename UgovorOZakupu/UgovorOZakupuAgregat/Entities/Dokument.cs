﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Entities
{
    public class Dokument
    {
        [Key]
        public Guid DokumentId { get; set; }
        public string ZavodniBroj { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumDonosenjaDokumenta { get; set; }
    }
}
