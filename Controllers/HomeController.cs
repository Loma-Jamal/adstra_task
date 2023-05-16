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
    public class HomeController :Controller
    {

        IUnitOfWork _unitOfWork;
        Response _CallResponse = new Response();


        public IActionResult Login()
        {
            return View();
        }

        public HomeController(IUnitOfWork t)
        {
            _unitOfWork = t;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(VMLogin viewModel)
        {
            try
            {
                if (!_unitOfWork.TblUsers.Queryable().Any(a => a.UserName == viewModel.UserName))
                {
                    _CallResponse.IsSuccess = false;
                    _CallResponse.Message = "Incorrect Username or Password. Please try again.";

                    return Json(_CallResponse);
                }

            }
            catch(Exception ex)
            {

            }



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
