using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.Areas.Support.Model
{
    public class CustomerDashboard : ResponeValues
    {
        public string ExpiryMiti { get; set; }
        public int? ExpireAfterDays { get; set; }
        public double? TotalDues { get; set; }
        public MonthlyTicket TicketSumm { get; set; } = new MonthlyTicket();
        public List<MonthlyTicket> MonthlyTicketSumm { get; set; } = new List<MonthlyTicket>();
        public List<MonthlyTicket> ExecutiveTicketSumm { get; set; } = new List<MonthlyTicket>();
        public SupportExecutive Executive { get; set; }
    }

    public class MonthlyTicket
    {
        public int TotalT { get; set; }
        public int OpenT { get; set; }
        public int HoldT { get; set; }
        public int InProgressT { get; set; }
        public int ClosedT { get; set; }
        public int ApprovedT { get; set; }
        public int ApprovedPT { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }


    }
}