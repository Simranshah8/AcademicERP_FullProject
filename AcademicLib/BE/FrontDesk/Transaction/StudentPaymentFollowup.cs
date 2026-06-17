using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class StudentPaymentFollowup : ResponeValues
    {
        public int? TranId { get; set; }
        public string AutoNumber { get; set; }
        public int? AcademicYearId { get; set; }
        public int StudentId { get; set; }
        public int UptoMonthId { get; set; }
        public int? FeeItemId { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string Remarks { get; set; }
        public bool NextFollowupRequired { get; set; }
        public DateTime? NextFollowupDateTime { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public DateTime? NextFollowupTime { get; set; }
        public int? RefTranId { get; set; }
        public double DuesAmt { get; set; }
        public int? CommunicationTypeId { get; set; }
        public int? StatusId { get; set; }


    }
}
