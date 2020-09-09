using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Machines.Models;
using Machines.DataAccess;

namespace Machines.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFaultRepository repo;

        public HomeController(IFaultRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var unresolvedFaults = repo.GetUnresolvedFaults();

            return View("UnresolvedFaults", unresolvedFaults);
        }
    }
}
