using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Reporting
{
    public class OnlineClass
    {
        DA.Academic.Reporting.OnlineClassDB db = null;
        int _UserId = 0;

        public OnlineClass(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db=new DA.Academic.Reporting.OnlineClassDB(hostName, dbName);
        }
        public AcademicLib.RE.Academic.OnlineClassAdmin getClassList(DateTime? forDate)
        {
            return db.getClassList(_UserId, forDate);
        }
        public AcademicLib.RE.Academic.OnlineClassAdmin getMissedClassList(DateTime? forDate)
        {
            return db.getMissedClassList(_UserId, forDate);
        }
        }
}
