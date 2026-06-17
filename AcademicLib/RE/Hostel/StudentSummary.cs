using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Hostel
{
    public class StudentSummary : RE.Academic.StudentSummary
    {
        public string RoomName { get; set; }
        public string BedName { get; set; }
        public int BedNo { get; set; }
        public double Rate { get; set; }
        public DateTime? AllotDate { get; set; }
        public string AllotMiti { get; set; }       
        public double DuesAmt { get; set; }
        public double DebitAmt { get; set; }
        public double CreditAmt { get; set; }

    }

    public class StudentSummaryCollections : List<StudentSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
