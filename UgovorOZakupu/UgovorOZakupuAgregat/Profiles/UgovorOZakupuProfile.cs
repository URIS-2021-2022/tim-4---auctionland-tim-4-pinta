using AutoMapper;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    public class UgovorOZakupuProfile : Profile
    {
        public UgovorOZakupuProfile()
        {
            CreateMap<UgovorOZakupu, UgovorOZakupuDto>();

            CreateMap<UgovorOZakupuDto, UgovorOZakupu>();

            CreateMap<UgovorOZakupuUpdateDto, UgovorOZakupu>();

            CreateMap<UgovorOZakupu, UgovorOZakupu>();
        }
    }
}