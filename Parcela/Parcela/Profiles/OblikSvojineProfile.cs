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
    /// Profil za mapiranje oblika svojine
    /// </summary>
    public class OblikSvojineProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public OblikSvojineProfile()
        {
            CreateMap<OblikSvojineEntity, OblikSvojineDto>();
            CreateMap<OblikSvojineDto, OblikSvojineEntity>();
        }
    }
}
