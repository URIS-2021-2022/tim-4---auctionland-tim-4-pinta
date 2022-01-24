using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Controllers
{
    public class KorisnikController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
