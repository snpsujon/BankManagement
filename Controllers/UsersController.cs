using BankManagement.Data;
using BankManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BankManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UsersController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Route("profile")]
        public IActionResult UserProfile()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Home", new { redirecturl = "profile" });
            }
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            var userinfo = from u in _context.usersTbls
                           join b in _context.usersBankAccTbls on u.UserID equals b.UserID
                           join t in _context.userTypeTbls on u.UserTypeID equals t.UserTypeID
                           join s in _context.statusTbls on b.StatusID equals s.StatusID
                           join a in _context.bankAccTypeTbls on b.BankAccTypeID equals a.BankAccTypeID
                           where u.UserID == userid
                           select new UserProfileVM
                           {
                               UserID = u.UserID,
                               UserName = u.UserName,
                               UserEmail = u.UserEmail,
                               UserPhone = u.UserPhone,
                               FullName = u.FullName,
                               UserAddress = u.UserAddress,
                               NIDNumber = u.NIDNumber,
                               BirthDate = u.BirthDate,
                               PictureLink = u.PictureLink,
                               Password = u.Password,
                               Status = s.Status,
                               BankAccType = a.BankAccType,
                               UserType = t.UserType,
                               BankAccNumber = b.BankAccNumber,
                               BankAccBalance = b.BankAccBalance
                           };

            return View(userinfo.FirstOrDefault());
        }

        [HttpGet]
        [Route("usersmanagement")]
        public IActionResult UserManagement()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Home", new { redirecturl = "usersmanagement" });
            }
            var users = _context.usersTbls.ToList();
            return View(users);
        }

        [HttpGet]
        [Route("404")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
