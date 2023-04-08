using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class UserTypeTbl
    {
        [Key]
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public DateTime? CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
