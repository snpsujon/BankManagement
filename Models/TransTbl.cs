using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class TransTbl
    {
        [Key]
        public int TransID { get; set; }
        public int TransTypeID { get; set; }
        public int BankAccID { get; set; }
        public string TransExtType { get; set; }
        public string TransReason { get; set; }
        public float TransChargeAmount { get; set; }
        public float TransAmount { get; set; }
        public float TransTotalAmount { get; set; }
        public int StatusID { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
