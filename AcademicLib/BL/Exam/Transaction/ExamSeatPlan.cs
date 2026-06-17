using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamSeatPlan
    {
        DA.Exam.Transaction.ExamSeatPlanDB db = null;
        int _UserId = 0;

        public ExamSeatPlan(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamSeatPlanDB(hostName, dbName);
        }
        public ResponeValues SaveSeatDetails( BE.Exam.Transaction.SeatDetailsCollections dataColl)
        {
            return db.SaveSeatDetails(_UserId, dataColl);
        }
        public RE.Exam.SeatPlanCollections GetAllExamSeatPlan(int AcademicYearId)
        {
            return db.GetAllExamSeatPlan(_UserId,AcademicYearId);
        }
            public BE.Exam.Transaction.ExamRoomCollections GetRoomList(int AcademicYearId, int ExamShiftId, int ExamTypeId)
        {
            return db.GetRoomList(_UserId,AcademicYearId, ExamShiftId, ExamTypeId);
        }
        public BE.Exam.Transaction.RoomSeatDetailsCollections GetRoomSeatDetails(int AcademicYearId, int ExamShiftId, int ExamTypeId, int RoomId,int SeatPlanAs)
        {
            return db.GetRoomSeatDetails(_UserId,AcademicYearId, ExamShiftId, ExamTypeId, RoomId,SeatPlanAs);
        }
        public BE.Academic.Creation.ClassSectionCollections GetClassSectionList(int AcademicYearId, int ExamShiftId, int ExamTypeId, int RoomId, int SeatPlanAs, ref BE.Exam.Transaction.StudentForSeatPlanCollections studentColl)
        {
            return db.GetClassSectionList(_UserId,AcademicYearId, ExamShiftId, ExamTypeId, RoomId,SeatPlanAs, ref studentColl);
        }
            public ResponeValues DeleteSeatPlan(int ExamShiftId, int ExamTypeId, int RoomId)
        {
            return db.DeleteSeatPlan(_UserId,ExamShiftId,ExamTypeId,RoomId);
        }
            public ResponeValues GenerateSeatPlan(int AcademicYearId, AcademicLib.BE.Exam.Transaction.GenerateSeatPlan beData)
        {
            return db.GenerateSeatPlan(AcademicYearId,beData);
        }
        public AcademicLib.RE.Exam.ExamSeatPlanDetails GetExamSeatPlan(int AcademicYearId, int ExamTypeId,  int ExamShiftId,int FieldValueAs,string RoomIdColl)
        {
            return db.GetExamSeatPlan(_UserId,AcademicYearId,ExamTypeId, ExamShiftId, FieldValueAs,RoomIdColl);
        }
        public AcademicLib.RE.Exam.StudentExamSeatPlanCollections GetStudentListForSMS(int AcademicYearId, int ExamTypeId, int ExamShiftId)
        {
            return db.GetStudentListForSMS(_UserId,AcademicYearId, ExamTypeId, ExamShiftId);
        }
     }
}
