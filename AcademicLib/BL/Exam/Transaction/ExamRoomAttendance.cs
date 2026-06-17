using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamRoomAttendance
    {
        DA.Exam.Transaction.ExamRoomAttendanceDB db = null;
        int _UserId = 0;

        public ExamRoomAttendance(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamRoomAttendanceDB(hostName, dbName);
        }

        public ResponeValues SaveUpdateExamAttendance(BE.Exam.Transaction.ExamRoomAttendanceCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            resVal = db.SaveUpdateExamAttendance(_UserId, dataColl);
            return resVal;
        }

        public BE.Exam.Transaction.ExamRoomAttendanceCollections GetExamWiseAttendance(int AcademicYearId,int EntityId, int ExamTypeId, int ExamShiftId, int RoomId, DateTime ExamDate)
        {
            return db.getExamWiseAttendance(_UserId, AcademicYearId, EntityId, ExamTypeId, ExamShiftId, RoomId, ExamDate);
        }

        public ResponeValues DeleteExamRoomWiseAttendance(DateTime ExamDate, int ExamTypeId, int ExamShiftId, int RoomId)
        {
            return db.DeleteExamRoomWiseAttendance(_UserId, ExamDate, ExamTypeId, ExamShiftId, RoomId);
        }

    }
}
