using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Wallet
{
    public class PaymentGateway : ResponeValues
    {
        public int? TranId { get; set; }
        public string Name { get; set; } = "";
        public string IconPath { get; set; } = "";
        public PAYMENTGATEWAYS ForGateWay { get; set; }
        public int ForId
        {
            get
            {
                return (int)ForGateWay;
            }
        }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public int? LedgerId { get; set; }
        public string LedgerName { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public string SchoolId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string MerchantId { get; set; } = "";
        public string MerchantName { get; set; } = "";
        
    }
    public class PaymentGatewayCollections : System.Collections.Generic.List<PaymentGateway>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
