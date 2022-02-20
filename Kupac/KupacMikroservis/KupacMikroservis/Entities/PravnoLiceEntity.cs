using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{

    /// <summary>
    /// Model realnog entiteta Pravno lice, nadtipa Kupac
    /// </summary>
    public class PravnoLiceEntity: KupacEntity
    {
        /// <summary>
        /// Maticni broj pravnog lica
        /// </summary>
        public string MaticniBroj { get; set; }

        /// <summary>
        /// Faks pravnog lica
        /// </summary>
        public string Faks { get; set; }

    }
}