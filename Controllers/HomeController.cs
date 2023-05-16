using Adstra_task.Models;
using Microsoft.AspNetCore.Http;
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

        IUnitOfWork _unitOfWork;
        Response _CallResponse = new Response();


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Profile(VMProfile vMProfile)
        {
            return View(vMProfile);
        }

        //[HttpPost]
        //[Route("/Profile")]
        //public async Task<IActionResult> Profile(VMProfile viewModel)
        //{
        //    try
        //    {
        //        if (!_unitOfWork.TblUsers.Queryable().Any(a => a.UserName == viewModel.UserName))
        //        {
        //            _CallResponse.IsSuccess = false;
        //            _CallResponse.Message = "You are not registered, please sign up first.";

        //            return Json(_CallResponse);
        //        }
        //        else if (_unitOfWork.TblUsers.Queryable().Any(a => a.UserName != viewModel.UserName || a.Password != viewModel.Password))
        //        {

        //            _CallResponse.IsSuccess = false;
        //            _CallResponse.Message = "Incorrect Username or Password. Please try again.";

        //            return Json(_CallResponse);
        //        }
        //        else
        //        {
        //            var Data = _unitOfWork.TblUsers.Queryable().Where(a => a.UserName == viewModel.UserName).FirstOrDefault();


        //            return RedirectToAction("Profile", Data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }



        //    return View();
        //}




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
                    _CallResponse.Message = "You are not registered, please sign up first.";

                    return Json(_CallResponse);
                }
                else if(_unitOfWork.TblUsers.Queryable().Any(a => a.UserName != viewModel.UserName || a.Password != viewModel.Password))
                {
                    
                    _CallResponse.IsSuccess = false;
                    _CallResponse.Message = "Incorrect Username or Password. Please try again.";

                    return Json(_CallResponse);
                }
                else
                {
                    var Data = _unitOfWork.TblUsers.Queryable().Where(a => a.UserName == viewModel.UserName).FirstOrDefault();
                 

                    return RedirectToAction("Profile", Data);
                }
            }
            catch (Exception ex)
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
