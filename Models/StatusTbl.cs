using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class StatusTbl
    {
        [Key]
        public int StatusID { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
