using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Academic.Reporting
{
    public class StudentSummaryModel
    {
        DA.Academic.Reporting.StudentSummaryModelDB db = null;
        int _UserId = 0;

        public StudentSummaryModel(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Reporting.StudentSummaryModelDB(hostName, dbName);
        }
        public RE.Academic.StudentSummaryModelCollections GetStudentDynamicSummary(int? AcademicYearId)
        {
            return db.GetStudentDynamicSummary(_UserId,AcademicYearId);
        }


    }
}