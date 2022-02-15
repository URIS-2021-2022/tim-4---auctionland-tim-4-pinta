﻿using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public interface IGatewayService
    {
        Task<GatewayDto> GetUrl(string servis);
    }
}
