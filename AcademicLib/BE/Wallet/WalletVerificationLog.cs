using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Wallet
{
    public class WalletVerificationLog
    {
        public WalletVerificationLog()
        {
            RequestTime = DateTime.Now;
            ResponseTime = DateTime.Now;
        }
        public int TranId { get; set; }
        public string Idx { get; set; }
        public string TypeIdx { get; set; }
        public string TypeName { get; set; }
        public string StateIdx { get; set; }
        public string StateName { get; set; }
        public string StateTemplate { get; set; }
        public double Amount { get; set; }
        public double FeeAmount { get; set; }
        public bool Refunded { get; set; }
        public DateTime Created_On { get; set; }
        public string Ebanker { get; set; }
        public string UserIdx { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string MerchantIdx { get; set; }
        public string MerchantName { get; set; }
        public string MerchantMobile { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
    }
}
