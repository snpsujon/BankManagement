﻿using Microsoft.AspNetCore.Http;
using System;

namespace BankManagement.ViewModel
{
    public class AuthVM
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string FullName { get; set; }
        public string UserAddress { get; set; }
        public string NIDNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string PictureLink { get; set; }
        public string Password { get; set; }
        public string redirecturl { get; set; }
        public int UserTypeID { get; set; }
        public bool IsActive { get; set; }
        public string InactiveReason { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
