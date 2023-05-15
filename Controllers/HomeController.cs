using Adstra_task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Adstra_task
{
    public class HomeController : Controller
    {
    

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(VMLogin viewModel)
        {
            return View();
        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
