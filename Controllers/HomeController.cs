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
                var user = _context.usersTbls.ToList();
                if (user.Count == 0)
                {
                    DefultData();
                }
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
            if (HttpContext.Session.GetString("Email") != null)
            {

                return RedirectToAction("Index");

            }
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
                var isUser = _context.usersTbls.Where(x => x.UserName == authVM.UserName).FirstOrDefault();
                if (isUser != null)
                {
                    return Json(new { success = false, message = "Duplicate UserName" });
                }
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
                    UserTypeID = 3,
                    IsActive = true,
                    CreatedAT = DateTime.Now,
                    CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserID"))
                };
                _context.Add(user);
                _context.SaveChanges();

                string LastBankAccNum = null;
                var BankAccExist = _context.usersBankAccTbls.ToList();
                if (BankAccExist.Count > 0)
                {
                    LastBankAccNum = _context.usersBankAccTbls.OrderByDescending(x => x.BankAccID).FirstOrDefault().BankAccNumber;

                }

                if (LastBankAccNum == null)
                {

                    LastBankAccNum = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "/0000000001";
                }
                else
                {
                    var split = LastBankAccNum.Split('/');
                    var last = split[1];
                    var lastNum = Convert.ToInt32(last);
                    lastNum++;
                    LastBankAccNum = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "/" + lastNum.ToString("0000000000");
                }

                UsersBankAccTbl bankAcc = new UsersBankAccTbl
                {
                    UserID = user.UserID,
                    BankAccNumber = LastBankAccNum,
                    BankAccBalance = 0,
                    StatusID = 1,
                    BankAccTypeID = 1,
                    CreatedAT = DateTime.Now,
                    CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserID"))
                };
                _context.Add(bankAcc);
                _context.SaveChanges();
                return Json(new { success = true, message = "User Create Successfully" });
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
            HttpContext.Session.SetString("UserPicture", user.PictureLink);
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

        public void DefultData()
        {
            //Admin User Create
            UsersTbl userTbl = new UsersTbl
            {
                UserName = "admin",
                UserEmail = "admin@abcbank.com",
                UserPhone = "01700000000",
                FullName = "Admin",
                UserAddress = "Dhaka",
                NIDNumber = "123456789",
                BirthDate = DateTime.Now,
                PictureLink = "nopic.jpg",
                Password = "123456",
                IsActive = true,
                UserTypeID = 1,
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(userTbl);
            _context.SaveChanges();

            //Admin Bank account
            UsersBankAccTbl bankAcc = new UsersBankAccTbl
            {
                UserID = userTbl.UserID,
                BankAccNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "/0000000001",
                BankAccBalance = 999999999,
                StatusID = 1,
                BankAccTypeID = 2,
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(bankAcc);
            _context.SaveChanges();
            //User Type Tbl
            UserTypeTbl userType = new UserTypeTbl
            {
                UserType = "Admin",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(userType);
            _context.SaveChanges();
            UserTypeTbl userType1 = new UserTypeTbl
            {
                UserType = "Super Admin",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(userType1);
            _context.SaveChanges();
            UserTypeTbl userType2 = new UserTypeTbl
            {
                UserType = "User",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(userType2);
            _context.SaveChanges();

            //Status Table
            StatusTbl status = new StatusTbl
            {
                Status = "Active",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status);
            _context.SaveChanges();
            StatusTbl status1 = new StatusTbl
            {
                Status = "Inactive",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status1);
            _context.SaveChanges();
            StatusTbl status2 = new StatusTbl
            {
                Status = "Paused",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status2);
            _context.SaveChanges();
            StatusTbl status3 = new StatusTbl
            {
                Status = "Pending",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status3);
            _context.SaveChanges();
            StatusTbl status4 = new StatusTbl
            {
                Status = "Success",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status4);
            _context.SaveChanges();
            StatusTbl status5 = new StatusTbl
            {
                Status = "Decline",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status5);
            _context.SaveChanges();
            StatusTbl status6 = new StatusTbl
            {
                Status = "Failed",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status6);
            _context.SaveChanges();
            StatusTbl status7 = new StatusTbl
            {
                Status = "Banned",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(status7);
            _context.SaveChanges();


            //Bank Account Type
            BankAccTypeTbl bankAccType = new BankAccTypeTbl
            {
                BankAccType = "Saving",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(bankAccType);
            _context.SaveChanges();
            BankAccTypeTbl bankAccType1 = new BankAccTypeTbl
            {
                BankAccType = "Current",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(bankAccType1);
            _context.SaveChanges();

            //Transaction Type
            TransTypeTbl transactionType = new TransTypeTbl
            {
                TransType = "Transfer",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(transactionType);
            _context.SaveChanges();
            TransTypeTbl transactionType1 = new TransTypeTbl
            {
                TransType = "Mobile Recharge",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(transactionType1);
            _context.SaveChanges();
            TransTypeTbl transactionType2 = new TransTypeTbl
            {
                TransType = "Bill Payment",
                CreatedAT = DateTime.Now,
                CreatedBy = 1
            };
            _context.Add(transactionType2);
            _context.SaveChanges();



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
