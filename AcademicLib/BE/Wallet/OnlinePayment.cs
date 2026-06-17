using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Wallet
{
    public class OnlinePayment : ResponeValues
    {
        public int TranId { get; set; }      
        public int PaymentGateWayId { get; set; }
        public double Amount { get; set; }
        public string ReferenceId { get; set; }
        public string MobileNo { get; set; }
        public string Notes { get; set; }
        public string From { get; set; }
        public DateTime LogDateTime { get; set; }
        public int? ReceiptTranId { get; set; }
        public string ReceiptPath { get; set; }
        public string PrivateKey { get; set; }
        public int? UptoMonthId { get; set; }

        public int? ClassId { get; set; }
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }

    public class PaymentGateWay : ResponeValues
    {
        public PAYMENTGATEWAYS GateWay { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string SchoolId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; } = "";
        public string IconPath { get; set; } = "";
        public string MerchantId { get; set; } = "";
        public string MerchantName { get; set; } = "";
        public int? LedgerId { get; set; }
    }
    public class PaymentGateWayCollections : System.Collections.Generic.List<PaymentGateWay>
    {
        public PaymentGateWayCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum PAYMENTGATEWAYS
    {
        KHALTI=1,
        E_SEWA=2,
        PHONE_PAY=3,
        PAY_U=4,
        RAZOR_PAY=5,
        INSTAMOJO=6,
        NEPAL_PAY=7
    }
}
