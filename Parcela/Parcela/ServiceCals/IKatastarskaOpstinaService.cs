using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public interface IKatastarskaOpstinaService
    {
        /// <summary>
        /// Zahtev za katastarskom opstinom po ID-ju
        /// </summary>
        /// <param name="katastarskaOpstinaID"></param>
        /// <returns></returns>
        Task<OpstinaParceleDto> GetKatastarskaOpstinaByIdAsync(Guid katastarskaOpstinaID, string token);
    }
}
