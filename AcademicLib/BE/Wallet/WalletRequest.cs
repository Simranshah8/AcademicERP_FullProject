using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Wallet
{
    public class WalletRequest
    {
        public WalletRequest()
        {
            PublicKey = "";
            PrivateKey = "";
            Url = "";
            MobileNo = "";
            ProductId = "";
            ProductName = "";
            ProductURL = "";
            Token = "";
            RequestTime = DateTime.Now;
            ResponseTime = DateTime.Now;
            IpAddress = "";

        }

        public int TranId { get; set; }
        public int UserId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Url { get; set; }
        public string MobileNo { get; set; }
        public double Amount { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductURL { get; set; }
        public string Token { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string IpAddress { get; set; }       

        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
