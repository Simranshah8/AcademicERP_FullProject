using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
using AcademicLib.DA;
using AcademicERP.DA;

namespace AcademicLib.BL
{

    public class StudentHealthReport
    {
        StudentHealthReportDB db = null;
        int _UserId = 0;

        public StudentHealthReport(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new StudentHealthReportDB(hostName, dbName);
        }

        public RE.StudentHealthReportCollections GetStudentHealthReport(DateTime? DateFrom, DateTime? DateTo)
        {
            return db.GetAllStudentHealthReport(_UserId, DateFrom, DateTo);
        }
        public RE.StudentHealthPastHistoryCollections GetStudentHealthPastHistory(DateTime? DateFrom, DateTime? DateTo)
        {
            return db.GetAllStudentHealthPastHistory(_UserId, DateFrom, DateTo);
        }
        
    }

}

