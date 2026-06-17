using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Attendance.Reporting
{
    public class StudentAttendanceSumary
    {
        AcademicLib.DA.Attendance.Reporting.StudentAttendanceSumaryDB db = null;

        int _UserId = 0;

        public StudentAttendanceSumary(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new AcademicLib.DA.Attendance.Reporting.StudentAttendanceSumaryDB(hostName, dbName);
        }
        public AcademicLib.RE.Attendance.StudentAttendanceSumaryCollections GetStudentAttendanceSumary(DateTime? DateFrom, DateTime? DateTo, int? AcademicYearId, int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId, int? BranchId)
        {
            return db.GetStudentAttendanceSumary(_UserId, DateFrom, DateTo, AcademicYearId, ClassId, SectionId, BatchId, SemesterId, ClassYearId, BranchId);
        }

    }
}