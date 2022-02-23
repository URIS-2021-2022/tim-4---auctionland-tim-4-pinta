﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    public class TipJavnogNadmetanjaUpdateDto
    {
        /// <summary>
        ///ID tipa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ID")]
        public Guid TipJavnogNadmetanjaID { get; set; }
        /// <summary>
        ///Naziv tipa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv tipa")]
        public String NazivTipaJavnogNadmetanja { get; set; }
    }
}
