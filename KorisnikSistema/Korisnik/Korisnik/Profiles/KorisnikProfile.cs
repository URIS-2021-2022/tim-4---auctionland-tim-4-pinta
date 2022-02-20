using AutoMapper;
using Korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Profiles
{
    public class KorisnikProfile : Profile
    {
        public KorisnikProfile()
        {
            CreateMap<KorisnikModel, KorisnikDto>();

            CreateMap<KorisnikDto, KorisnikModel>();

            CreateMap<KorisnikModel, KorisnikModel>();
        }
    }
}
