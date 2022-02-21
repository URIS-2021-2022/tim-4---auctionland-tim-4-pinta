using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Profiles
{
    /// <summary>
    /// Profil za mapiranje obradivosti
    /// </summary>
    public class ObradivostProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ObradivostProfile()
        {
            CreateMap<ObradivostEntity, ObradivostDto>();
            CreateMap<ObradivostDto, ObradivostEntity>();
            CreateMap<ObradivostEntity, ObradivostUpdateDto>();
            CreateMap<ObradivostUpdateDto, ObradivostEntity>();
        }
    }
}
