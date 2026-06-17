using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Wallet
{
    public class WalletResponse
    {
        public string token { get; set; }
        public double amount { get; set; }
        public string mobile { get; set; }
        public string product_identity { get; set; }
        public string product_name { get; set; }
    }
}
