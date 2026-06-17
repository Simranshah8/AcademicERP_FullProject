using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Attendance
{
    public class PendingAttendance
    {
        DA.Attendance.PendingAttendanceDB db = null;
        int _UserId = 0;
        public PendingAttendance(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Attendance.PendingAttendanceDB(hostName,dbName);
            
        }
        public RE.Attendance.PendingAttendanceCollections GetPendngAttendanace(int? AcademicYearId, int? ClassId, int? SectionId, int? BatchId, int? SemesterId,int? ClassYearId ,DateTime? DateFrom, DateTime? DateTo, int For=1)

        {
            return db.GetPendingAttendance(_UserId, AcademicYearId, ClassId, SectionId, BatchId, SemesterId, ClassYearId, DateFrom, DateTo, For);
        }
    }
    
}