using AdresaServis.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Data
{
    /// <summary>
    /// Repozitorijum za adrese
    /// </summary>
    public class AdresaRepository : IAdresaRepository
    {
        private readonly AdresaContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public AdresaRepository(AdresaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Cuvanje promena
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// Kreiranje adrese
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns></returns>
        public AdresaEntity CreateAdresa(AdresaEntity adresa)
        {
            adresa.AdresaID = Guid.NewGuid();
            context.Adrese.Add(adresa);
            return adresa;
        }

        /// <summary>
        /// Brisanje adrese
        /// </summary>
        /// <param name="adresaID"></param>
        public void DeleteAdresa(Guid adresaID)
        {
            context.Adrese.Remove(context.Adrese.FirstOrDefault(a => a.AdresaID == adresaID));
        }

        /// <summary>
        /// Vracanje adrese po ID-ju
        /// </summary>
        /// <param name="adresaID"></param>
        /// <returns></returns>
        public AdresaEntity GetAdresaById(Guid adresaID)
        {
            return context.Adrese.FirstOrDefault(a => a.AdresaID == adresaID);
        }

        /// <summary>
        /// Vracanje svih adresa
        /// </summary>
        /// <returns></returns>
        public List<AdresaEntity> GetAdrese()
        {
            return (from a in context.Adrese select a).ToList();
        }

        /// <summary>
        /// Modifikovanje adrese
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns></returns>
        public void UpdateAdresa(AdresaEntity adresa)
        {
           
        }
    }
}
