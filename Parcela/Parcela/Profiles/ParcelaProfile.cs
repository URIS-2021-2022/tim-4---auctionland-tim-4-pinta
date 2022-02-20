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
    /// Profil za mapiranje parcele
    /// </summary>
    public class ParcelaProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ParcelaProfile()
        {
            CreateMap<ParcelaEntity, ParcelaDto>();
            CreateMap<ParcelaDto, ParcelaEntity>();
        }
    }
}
