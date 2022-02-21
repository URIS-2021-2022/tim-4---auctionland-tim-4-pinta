﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za modifikovanje obradivosti
    /// </summary>
    public class ObradivostUpdateDto
    {
        /// <summary>
        /// ID obradivosti
        /// </summary>
        public Guid ObradivostID { get; set; }

        /// <summary>
        /// Naziv tipa obradivosti
        /// </summary>
        public String ObradivostNaziv { get; set; }
    }
}