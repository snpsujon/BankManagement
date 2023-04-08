using System;

namespace BankManagement.ViewModel
{
    public class UserProfileVM
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
        public string Status { get; set; }
        public string BankAccType { get; set; }
        public string UserType { get; set; }
        public string BankAccNumber { get; set; }
        public float BankAccBalance { get; set; }
    }
}
