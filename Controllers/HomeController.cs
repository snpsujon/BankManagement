using BankManagement.Data;
using BankManagement.Models;
using BankManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BankManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login");

            }
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");


        }

        [HttpGet]
        [Route("auth")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public JsonResult Login(AuthVM authVM)
        {
            var isUser = _context.usersTbls.Where(x => x.UserName == authVM.UserName && x.Password == authVM.Password).FirstOrDefault();
            if (isUser != null)
            {
                if (isUser.PictureLink == null)
                {
                    HttpContext.Session.SetString("Email", isUser.UserEmail);
                    HttpContext.Session.SetString("Name", isUser.FullName);
                    HttpContext.Session.SetString("UserID", isUser.UserID.ToString());

                    return Json(5);
                }
                HttpContext.Session.SetString("Email", isUser.UserEmail);
                HttpContext.Session.SetString("Name", isUser.FullName);
                HttpContext.Session.SetString("UserID", isUser.UserID.ToString());
                HttpContext.Session.SetString("UserType", isUser.UserTypeID.ToString());
                HttpContext.Session.SetString("UserPicture", isUser.PictureLink);
                return Json(authVM.redirecturl);
            }
            return Json(0);
        }


        [HttpPost]
        [Route("register")]
        public JsonResult RegisterUser(AuthVM authVM)
        {
            try
            {
                UsersTbl user = new UsersTbl
                {
                    UserName = authVM.UserName,
                    UserPhone = authVM.UserPhone,
                    FullName = authVM.FullName,
                    UserAddress = authVM.UserAddress,
                    UserEmail = authVM.UserEmail,
                    NIDNumber = authVM.NIDNumber,
                    BirthDate = authVM.BirthDate,
                    Password = authVM.Password,
                    UserTypeID = 2,
                    IsActive = true,
                    CreatedAT = DateTime.Now,
                    CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserID"))
                };
                _context.Add(user);
                _context.SaveChanges();
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

        [HttpGet]
        [Route("usermanagement")]
        public IActionResult UserManage()
        {
            var users = _context.usersTbls.ToList();
            return View(users);
        }
        [HttpGet]
        [Route("uploadpic")]
        public IActionResult UserProfilePic()
        {
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            AuthVM user = new AuthVM();
            user.UserID = userid;
            return View(user);
        }
        [HttpPost]
        [Route("uploadpic")]
        public IActionResult UserProfilePic(AuthVM model)
        {
            string uniqueFileName = UploadedFile(model);
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

            UsersTbl user = _context.usersTbls.Where(x => x.UserID == userid).FirstOrDefault();
            user.PictureLink = uniqueFileName;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        private string UploadedFile(AuthVM model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/ProfilePicture");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
