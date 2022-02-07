﻿using Uplata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.ServiceCalls
{
    public interface IKupacService
    {
        Task<UplataDto> GetKupacByIdAsync(Guid kupacID);
    }
}
