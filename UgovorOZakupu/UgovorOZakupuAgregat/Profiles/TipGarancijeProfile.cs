using AutoMapper;
using UgovorOZakupuAgregat.Entities;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.Profiles
{
    /// <summary>
    /// Profil za mapiranje tipa garancije
    /// </summary>
    public class TipGarancijeProfile : Profile
    {
        /// <summary>
        /// Knstruktor profila za mapiranje tipa garancije
        /// </summary>
        public TipGarancijeProfile()
        {
            CreateMap<TipGarancije, TipGarancijeDto>();

            CreateMap<TipGarancijeDto, TipGarancije>();

            CreateMap<TipGarancijeUpdateDto, TipGarancije>();

            CreateMap<TipGarancije, TipGarancije>();
        }
    }
}