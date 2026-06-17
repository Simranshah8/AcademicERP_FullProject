using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class SeatPlan
    {
        public SeatPlan()
        {
            ClassName = "";
            SectionName = "";
        }
        public int ExamShiftId { get; set; }
        public int ExamTypeId { get; set; }
        public int RoomId { get; set; }
        public string ExamShiftName { get; set; }
        public string ExamTypeName { get; set; }
        public string RoomName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public string ClassSectionName { get; set; }
        public int TotalSeat { get; set; }
        public int NoOfSeatAlloted { get; set; }

        public string UserName { get; set; }
        public DateTime LogDateTime_AD { get; set; }
        public string LogDateTime_BS { get; set; }

        public int TotalBanch { get; set; }
    }
    public class SeatPlanCollections : System.Collections.Generic.List<SeatPlan>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
