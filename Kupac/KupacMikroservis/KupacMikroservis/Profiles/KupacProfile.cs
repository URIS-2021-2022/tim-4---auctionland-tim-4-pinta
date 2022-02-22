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


            CreateMap<KupacEntity, KupacDTO>();
            

            CreateMap<PravnoLiceEntity, PravnoLiceDTO>();
            CreateMap<FizickoLiceEntity, FizickoLiceDTO>();

            CreateMap<PravnoLiceEntity, KupacDTO>()
                .ForMember(
                dest => dest.JedinstveniBroj,
                opt => opt.MapFrom(src => src.MaticniBroj));

            CreateMap<FizickoLiceEntity, KupacDTO>()
              .ForMember(
              dest => dest.JedinstveniBroj,
              opt => opt.MapFrom(src => src.JMBG));


            CreateMap<KupacEntity, KupacCreateDTO>();
            CreateMap<KupacEntity, KupacUpdateDTO>();
            CreateMap<KupacDTO,KupacEntity>();
            CreateMap<KupacCreateDTO, KupacEntity>();
            CreateMap<KupacUpdateDTO, KupacEntity>();

            

        }
    }
}
