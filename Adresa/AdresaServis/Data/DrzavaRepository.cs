using AdresaServis.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Data
{
    /// <summary>
    /// Repozitorijum za drzave
    /// </summary>
    public class DrzavaRepository : IDrzavaRepository
    {
        private readonly AdresaContext context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public DrzavaRepository(AdresaContext context)
        {
            this.context = context;
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
        /// Kreiranje drzave
        /// </summary>
        /// <param name="drzava"></param>
        /// <returns></returns>
        public DrzavaEntity CreateDrzava(DrzavaEntity drzava)
        {
            drzava.DrzavaID = Guid.NewGuid();
            context.Drzave.Add(drzava);
            return drzava;
        }

        /// <summary>
        /// Brisanje drzave
        /// </summary>
        /// <param name="drzavaID"></param>
        public void DeleteDrzava(Guid drzavaID)
        {
            context.Drzave.Remove(context.Drzave.FirstOrDefault(d => d.DrzavaID == drzavaID));
        }

        /// <summary>
        /// Vracanje svih drzava
        /// </summary>
        /// <returns></returns>
        public List<DrzavaEntity> GetDrzave()
        {
            return (from d in context.Drzave select d).ToList();
        }

        /// <summary>
        /// Vracanje drzave po ID-ju
        /// </summary>
        /// <param name="drzavaID"></param>
        /// <returns></returns>
        public DrzavaEntity GetDrzavaById(Guid drzavaID)
        {
            return context.Drzave.FirstOrDefault(d => d.DrzavaID == drzavaID);
        }

        /// <summary>
        /// Modifikovanje drzave
        /// </summary>
        /// <param name="drzava"></param>
        /// <returns></returns>
        public void UpdateDrzava(DrzavaEntity drzava)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
