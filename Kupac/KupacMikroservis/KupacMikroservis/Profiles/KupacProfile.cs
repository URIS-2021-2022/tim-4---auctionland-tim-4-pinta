using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class KupacProfile : Profile
    {
        public KupacProfile()
        {


            CreateMap<KupacEntity, KupacDto>();
            

            CreateMap<PravnoLiceEntity, PravnoLiceDto>();
            CreateMap<FizickoLiceEntity, FizickoLiceDto>();

            CreateMap<PravnoLiceEntity, KupacDto>()
                .ForMember(
                dest => dest.JedinstveniBroj,
                opt => opt.MapFrom(src => src.MaticniBroj));

            CreateMap<FizickoLiceEntity, KupacDto>()
              .ForMember(
              dest => dest.JedinstveniBroj,
              opt => opt.MapFrom(src => src.JMBG));


            CreateMap<KupacEntity, KupacCreateDto>();
            CreateMap<KupacEntity, KupacUpdateDto>();
            CreateMap<KupacDto,KupacEntity>();
            CreateMap<KupacCreateDto, KupacEntity>();
            CreateMap<KupacUpdateDto, KupacEntity>();
            CreateMap<KupacUpdateDto, KupacDto>();
            CreateMap<KupacDto,KupacUpdateDto>();



        }
    }
}
