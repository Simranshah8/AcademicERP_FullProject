using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Wallet
{
    public class WalletVerify
    {
        public string Idx { get; set; }

        public Types type { get; set; }
        public class Types
        {
            public string idx { get; set; }
            public string name { get; set; }
        }

        public class states : Types
        {
            public string template { get; set; }
        }
        public states state { get; set; }

        public double Amount { get; set; }
        public double fee_amount { get; set; }
        public bool refunded { get; set; }
        public DateTime created_on { get; set; }
        public string ebanker { get; set; }

        public class UserDetails : Types
        {
            public string mobile { get; set; }
        }
        public UserDetails user { get; set; }
        public UserDetails merchant { get; set; }

        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
    }

}
