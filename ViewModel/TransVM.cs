using System;

namespace BankManagement.ViewModel
{
    public class TransVM
    {
        public int TransID { get; set; }
        public int TransTypeID { get; set; }
        public int FromBankAccID { get; set; }
        public int ToBankAccID { get; set; }
        public string ToBankAccNumber { get; set; }
        public string FromBankAccNumber { get; set; }
        public string TrxType { get; set; }

        public string MobileNumber { get; set; }
        public string Oparator { get; set; }
        public string OparatorType { get; set; }


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
