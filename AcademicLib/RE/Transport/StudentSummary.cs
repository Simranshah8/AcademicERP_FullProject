using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Transport
{
    public class StudentSummary : RE.Academic.StudentSummary
    {
        public string RouteName { get; set; }
        public string PointName { get; set; }
        public string TravelType { get; set; } 
        public double Rate { get; set; }
        public string PickupTime { get; set; }
        public string DropTime { get; set; } 
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public double DuesAmt { get; set; }
        public double DebitAmt { get; set; }
        public double CreditAmt { get; set; }
        public string ClassYear { get; set; } = "";

    }

    public class StudentSummaryCollections : List<StudentSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
