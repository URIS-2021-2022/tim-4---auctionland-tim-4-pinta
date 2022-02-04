using AutoMapper;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KupacMikroservis.Profiles
{
    public class PravnoLiceProfile : Profile
    {
        public PravnoLiceProfile()
        {
            CreateMap<PravnoLiceEntity, PravnoLiceDTO>();
          //  CreateMap<PravnoLiceEntity, PravnoLiceCreateDTO>();
          //  CreateMap<PravnoLiceEntity, PravnoLiceCreateDTO>();

        }
    }
}
