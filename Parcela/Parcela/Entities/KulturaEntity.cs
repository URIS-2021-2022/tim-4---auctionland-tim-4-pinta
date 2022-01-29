﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Prestavlja model kulture
    /// </summary>
    public class KulturaEntity
    {
        /// <summary>
        /// ID kulture
        /// </summary>
        public Guid KulturaID { get; set; }

        /// <summary>
        /// Naziv kulture
        /// </summary>
        public String KulturaNaziv { get; set; }
    }
}
