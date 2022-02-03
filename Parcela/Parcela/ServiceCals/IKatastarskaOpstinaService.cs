﻿using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public interface IKatastarskaOpstinaService
    {
        OpstinaParceleDto GetKatastarskaOpstinaByIdAsync(Guid katastarskaOpstinaID);
    }
}
