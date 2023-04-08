using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class BankAccTypeTbl
    {
        [Key]
        public int BankAccTypeID { get; set; }
        public string BankAccType { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
