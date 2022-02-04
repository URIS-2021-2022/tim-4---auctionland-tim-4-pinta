using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class PrioritetProfile : Profile
    {
        public PrioritetProfile()
        {
            CreateMap<PrioritetEntity, PrioritetDTO>();
            CreateMap<PrioritetDTO, PrioritetEntity>();
            CreateMap<PrioritetEntity, PrioritetCreateDTO>();
            CreateMap<PrioritetEntity, PrioritetUpdateDTO>();
            CreateMap<PrioritetUpdateDTO,PrioritetEntity>();
            CreateMap<PrioritetCreateDTO,PrioritetEntity>();

        }
    }
}
