using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class FizickoLiceProfile : Profile
    {
        public FizickoLiceProfile()
        {
            CreateMap<FizickoLiceEntity, FizickoLiceDTO>();
           // CreateMap<FizickoLiceEntity, FizickoLiceCreateDTO>();
          //  CreateMap<FizickoLiceEntity, FizickoLiceUpdateDTO>();


        }
    }
}
