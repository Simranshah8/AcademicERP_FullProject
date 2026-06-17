using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Attendance.Reporting
{
    public class AttendanceAnalysis
    {
        AcademicLib.DA.Attendance.Reporting.AttendanceAnalysisDB db = null;

        int _UserId = 0;

        public AttendanceAnalysis(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new AcademicLib.DA.Attendance.Reporting.AttendanceAnalysisDB(hostName, dbName);
        }
        public AcademicLib.RE.Attendance.Reporting.AttendanceAnalysis getallAttendanceAnalysis()
        {
            return db.GetAcademicAnalysis(_UserId);
        }

    }
}