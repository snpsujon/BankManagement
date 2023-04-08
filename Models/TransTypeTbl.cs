using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class TransTypeTbl
    {
        [Key]
        public int TransTypeID { get; set; }
        public string TransType { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
