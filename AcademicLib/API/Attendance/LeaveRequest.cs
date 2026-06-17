using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AcademicLib.API.Attendance
{
    public class LeaveRequest : ResponeValues
    {
        public LeaveRequest()
        {
            Remarks = "";
            LeaveDuration = AcademicLib.BE.Attendance.LEAVEDURATION.FULL_DAY;
            LeavePeriod = AcademicLib.BE.Attendance.LEAVEPERIOD.OTHERS;
            DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
                  
        public int? EmployeeId { get; set; }
        public int? StudentId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string MessageToEmployee { get; set; }
        public string Remarks { get; set; }
        public int RequestId { get; set; }
        public AcademicLib.BE.Attendance.LEAVEDURATION LeaveDuration { get; set; }
        public AcademicLib.BE.Attendance.LEAVEPERIOD LeavePeriod { get; set; }
        public double? LeaveHours { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections DocumentColl { get; set; }

        public double Lat { get; set; }
        public double Lan { get; set; }
        public string Location { get; set; }
        public int? AlternativeEmployeeId { get; set; }

        public double TotalDays { get; set; }
        public int? SubstitueEmpId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class LeaveApprove
    {
        public int LeaveRequestId { get; set; }
        public int ApprovedBy { get; set; }
        public string ApprovedByUser { get; set; }
        public string ApprovedRemarks { get; set; }
        public int ApprovedType { get; set; }

    }

}
