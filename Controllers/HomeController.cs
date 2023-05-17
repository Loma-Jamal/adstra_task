using Adstra_task.Models;
using Microsoft.AspNetCore.Authorization;
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
       
        public IActionResult Profile()
        {
            VMProfile vMProfile = new VMProfile();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
            {

                int Id =(int) HttpContext.Session.GetInt32("UserID");
                var Data = _unitOfWork.TblUsers.Queryable().Where(a => a.Id == Id).FirstOrDefault();
                vMProfile.Name = Data.Name;
                vMProfile.Email = Data.Email;
                vMProfile.MobileNumber = Data.MobileNumber;
            }


            return View(vMProfile);
        }

        public IActionResult Dashboard()
        {
            VMProfile vMProfile = new VMProfile();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
            {

                int Id = (int)HttpContext.Session.GetInt32("UserID");
                var Data = _unitOfWork.TblUsers.Queryable().Where(a => a.Id == Id).FirstOrDefault();
                vMProfile.Name = Data.Name;
                vMProfile.Email = Data.Email;
                vMProfile.MobileNumber = Data.MobileNumber;
            }


            return View(vMProfile);
        }
        public IActionResult Signup()
        {
           
            return View();
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
                else if(_unitOfWork.TblUsers.Queryable().Any(a => a.UserName == viewModel.UserName && a.Password != viewModel.Password))
                {
                    
                    _CallResponse.IsSuccess = false;
                    _CallResponse.Message = "Incorrect Username or Password. Please try again.";

                    return Json(_CallResponse);
                }
                else
                {
                    var Data = _unitOfWork.TblUsers.Queryable().Where(a => a.UserName == viewModel.UserName).FirstOrDefault();
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
                    {
                       
                        HttpContext.Session.SetInt32("UserID", Data.Id);
                    }

                    return RedirectToAction("Dashboard");
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


        public async Task<IActionResult> UpdateProfile(VMProfile vMProfile)
        {
            try
            {

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
                {

                    int Id = (int)HttpContext.Session.GetInt32("UserID");
                    var Data = _unitOfWork.TblUsers.Queryable().Where(a => a.Id == Id).FirstOrDefault();
                    Data.Name =vMProfile.Name;
                    Data.Email =vMProfile.Email;
                    Data.MobileNumber =vMProfile.MobileNumber;
                    _unitOfWork.TblUsers.Update(Data);
                    _unitOfWork.SaveChanges();

                    _CallResponse.IsSuccess = true;

                }

                else
                {

                    _CallResponse.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {

                _CallResponse.IsSuccess = false;
            }



            return Json(_CallResponse);
        }
        public async Task<IActionResult> RegisterUser(VMRegister vMRegister)
        {
            try
            {


                if (!_unitOfWork.TblUsers.Queryable().Where(a => a.UserName == vMRegister.UserName).Any())
                {
                    User user = new User();

                    user.Name = vMRegister.Name;
                    user.Email = vMRegister.Email;
                    user.MobileNumber = vMRegister.MobileNumber;
                    user.Password = vMRegister.Password;
                    user.UserName = vMRegister.UserName;
                    user.SubscriptionDate = DateTime.Now.ToString();
                    _unitOfWork.TblUsers.Insert(user);
                    _unitOfWork.SaveChanges();

                    _CallResponse.IsSuccess = true;
                }
                else
                {
                    _CallResponse.IsSuccess = false;
                    _CallResponse.Message = "UesrName already exist";
                }

               
            }
            catch (Exception ex)
            {

                _CallResponse.IsSuccess = false;
            }



            return Json(_CallResponse);
        }


    }
}
