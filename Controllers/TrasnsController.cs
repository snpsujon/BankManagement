using BankManagement.Data;
using BankManagement.Models;
using BankManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BankManagement.Controllers
{
    public class TrasnsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public TrasnsController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Route("trans")]
        public IActionResult Index()
        {
            var alltrans = (from t in _context.transTbls.ToList()
                            join tt in _context.transTypeTbls.ToList() on t.TransTypeID equals tt.TransTypeID
                            join fb in _context.usersBankAccTbls.ToList() on t.FromBankAccID equals fb.BankAccID
                            join tb in _context.usersBankAccTbls.ToList() on t.ToBankAccID equals tb.BankAccID

                            select new TransVM
                            {
                                FromBankAccNumber = fb.BankAccNumber,
                                ToBankAccNumber = tb.BankAccNumber,
                                TrxType = tt.TransType,
                                TransAmount = t.TransAmount,
                                TransReason = t.TransReason,
                                CreatedAT = t.CreatedAT
                            }


                ).ToList();

            return View(alltrans);
        }
        [HttpGet]
        [Route("transfer")]
        public IActionResult Transfer()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Home", new { redirecturl = "transfer" });
            }
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            var userinfo = (from u in _context.usersTbls
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
                            }).FirstOrDefault();
            return View(userinfo);
        }
        [HttpPost]
        [Route("transfer")]
        public JsonResult Transfer(TransVM model)
        {
            var userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            var userbank = _context.usersBankAccTbls.Where(x => x.UserID == userid).FirstOrDefault();
            var tobank = _context.usersBankAccTbls.Where(x => x.BankAccNumber == model.ToBankAccNumber).FirstOrDefault();
            if (tobank == null)
            {
                return Json(new { success = false, message = "Invalid Bank Account Number" });
            }
            else
            {
                if (userbank.BankAccBalance < model.TransAmount)
                {
                    return Json(new { success = false, message = "Insufficient Balance" });
                }
                else
                {
                    userbank.BankAccBalance = userbank.BankAccBalance - model.TransAmount;
                    tobank.BankAccBalance = tobank.BankAccBalance + model.TransAmount;
                    _context.Update(userbank);
                    _context.Update(tobank);
                    var trans = new TransTbl
                    {
                        TransTypeID = 1,
                        FromBankAccID = userbank.BankAccID,
                        ToBankAccID = tobank.BankAccID,
                        TransAmount = model.TransAmount,
                        CreatedAT = DateTime.Now,
                        CreatedBy = userid,
                        TransReason = model.TransReason,
                    };
                    _context.Add(trans);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Transfer Successfull" });
                }
            }
            //return Json("success");


        }


    }
}
