using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Academic.Reporting
{
    public class ClassScheduleStatus
    {

        AcademicLib.DA.Academic.Reporting.ClassScheduleStatusDB db = null;

        int _UserId = 0;

        public ClassScheduleStatus(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Reporting.ClassScheduleStatusDB(hostName, dbName);
        }
        public AcademicLib.RE.Academic.ClassScheduleStatus getallClassSchedule( int AcademicyearId)
        {
            return db.GetClassScheduleStatus(_UserId,AcademicyearId);
        }
    }
}