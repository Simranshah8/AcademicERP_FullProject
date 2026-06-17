using System;
using AcademicLib.RE;
using AcademicLib.DA;

namespace AcademicLib.BL
{
    public class PrintLogs
    {
        DA.PrintLogsDB db = null;

        int _UserId = 0;
        public PrintLogs(int userId, string hostName, string dbName)
        {
            this._UserId = userId;
            db = new PrintLogsDB(hostName, dbName);
        }

        public ExamTypeCollection GetExamTypeData(int? ExamTypeId, int? ExamTypeGroupId,int ?AcademicYearId)
        {
            return db.GetExamTypeData(_UserId, ExamTypeId, ExamTypeGroupId, AcademicYearId);
        }

    }
}
