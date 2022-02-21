using AutoMapper;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    /// <summary>
    /// Profil za mapiranje ugovora o zakupu
    /// </summary>
    public class UgovorOZakupuProfile : Profile
    {
        /// <summary>
        /// Konstruktor profila za mapiranje ugovora o zakupu
        /// </summary>
        public UgovorOZakupuProfile()
        {
            CreateMap<UgovorOZakupu, UgovorOZakupuDto>();

            CreateMap<UgovorOZakupuDto, UgovorOZakupu>();

            CreateMap<UgovorOZakupu, UgovorOZakupu>();

            CreateMap<UgovorOZakupuUpdateDto, UgovorOZakupu>();

            CreateMap<UgovorOZakupuCreateDto, UgovorOZakupu>();
        }
    }
}