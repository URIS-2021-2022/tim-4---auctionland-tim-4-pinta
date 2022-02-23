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
    /// Profil za mapiranje odvodnjavanja
    /// </summary>
    public class OdvodnjavanjeProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public OdvodnjavanjeProfile()
        {
            CreateMap<OdvodnjavanjeEntity, OdvodnjavanjeDto>();
            CreateMap<OdvodnjavanjeDto, OdvodnjavanjeEntity>();
            CreateMap<OdvodnjavanjeEntity, OdvodnjavanjeCreateDto>();
            CreateMap<OdvodnjavanjeCreateDto, OdvodnjavanjeEntity>();
            CreateMap<OdvodnjavanjeEntity, OdvodnjavanjeUpdateDto>();
            CreateMap<OdvodnjavanjeUpdateDto, OdvodnjavanjeEntity>();
        }
    }
}
