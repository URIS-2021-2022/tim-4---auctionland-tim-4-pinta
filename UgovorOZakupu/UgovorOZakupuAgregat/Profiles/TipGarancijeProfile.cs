using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    public class TipGarancijeProfile : Profile
    {
        public TipGarancijeProfile()
        {
            CreateMap<TipGarancije, TipGarancijeDto>();

            CreateMap<TipGarancijeDto, TipGarancije>();

            CreateMap<TipGarancijeUpdateDto, TipGarancije>();

            CreateMap<TipGarancije, TipGarancije>();


        }
    }
}
