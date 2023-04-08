using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class UsersBankAccTbl
    {
        [Key]
        public int BankAccID { get; set; }
        public string BankAccNumber { get; set; }
        public int UserID { get; set; }
        public int BankAccTypeID { get; set; }
        public int StatusID { get; set; }
        public float BankAccBalance { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
