using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Attendance
{
    public class AttendanceStudentWise
    {
        DA.Attendance.AttendanceStudentWiseDB db = null;
        int _UserId = 0;
        public AttendanceStudentWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.Attendance.AttendanceStudentWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Attendance.AttendanceStudentWiseCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Attendance.AttendanceStudentWiseCollections getClassWiseAttendance(int AcademicYearId, int ClassId, int? SectionId, DateTime forDate, int InOutMode = 2, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.getClassWiseAttendance(_UserId,AcademicYearId, ClassId, SectionId,forDate,InOutMode,BatchId,SemesterId,ClassYearId);

        }
        public AcademicLib.API.Teacher.AttendanceSummaryCollections getClassWiseSummary(int AcademicYearId, int? ClassId, int? SectionId, DateTime? fromDate, DateTime? toDate)
        {
            return db.getClassWiseSummary(_UserId,AcademicYearId, ClassId, SectionId, fromDate, toDate);
        }
        public RE.Attendance.StudentManualDailyAttendanceCollections getManualDailyAttendance(int AcademicYearId, int? ClassId, int? SectionId, DateTime forDate, int InOutMode = 2, int? BatchId=null, int? SemesterId=null, int? ClassYearId=null, int? ClassShiftId = null)
        {
            return db.getManualDailyAttendance(_UserId,AcademicYearId, ClassId, SectionId, forDate, InOutMode,BatchId,SemesterId,ClassYearId,ClassShiftId);
        }
        public List<AcademicLib.RE.Global.StudentMonthlyAttendanceSumm> getMonthlyAttendanceSummaryForDB(int AcademicYearId, DateTime? fromDate, DateTime? toDate, int? MonthId)
        {
            return db.getMonthlyAttendanceSummaryForDB(_UserId, AcademicYearId, fromDate, toDate, MonthId);
        }
        public RE.OnlineClass.DateWiseAttendanceCollections getPeriodWiseAttendance(int AcademicYearId, DateTime forDate, int? classId, int? sectionId, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null)
        {
            return db.getPeriodWiseAttendance(_UserId,AcademicYearId, forDate, classId, sectionId,BatchId,ClassYearId,SemesterId);
        }
        public RE.OnlineClass.DateWiseAttendanceCollections getSubjectWiseAttendance(int AcademicYearId, DateTime fromDate, DateTime toDate, int? classId, int? sectionId, int subjectId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.getSubjectWiseAttendance(_UserId,AcademicYearId, fromDate, toDate, classId, sectionId, subjectId,BatchId,SemesterId,ClassYearId);
        }
        public ResponeValues DeleteStudentManualDailyAttendance(DateTime? ForDate, int? ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AttendaneTypeId)
        {
            return db.DeleteStudentManualDailyAttendance(_UserId, ForDate, ClassId, SectionId, BatchId, ClassYearId, SemesterId, AttendaneTypeId);
        }

        public RE.Attendance.PeriodForAttendanceCollections getPeriodForAttendance(int EntityId, int? BatchId, int? ClassId, int? SectionId, int? SemesterId, int? ClassYearId, int SubjectId)
        {
            return db.getPeriodForAttendance(_UserId, EntityId, BatchId, ClassId, SectionId, SemesterId, ClassYearId, SubjectId);
        }
        public ResponeValues IsValidData(ref BE.Attendance.AttendanceStudentWiseCollections beData)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else
                {

                    int row = 1;
                    foreach (var data in beData)
                    {
                        if (data.Attendance == null || data.Attendance==0)
                        {
                            resVal.ResponseMSG = "Please ! Enter Attendance Details At Row "+row.ToString();
                            return resVal;
                        }
                        if (data.ForDate.Year < 2020)
                        {
                            resVal.ResponseMSG = "Please ! Enter For Date";
                            return resVal;
                        }

                        if(data.Attendance.HasValue)
                        {
                            if (data.Attendance.Value != BE.Attendance.ATTENDANCES.LATE)
                                data.LateMin = 0;
                        }
                        data.CUserId = _UserId;

                        row++;
                    }

                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return resVal;
        }


        public List<AcademicLib.RE.Global.EmployeeMonthlyAttendanceSum> getMonthlyAttendanceSummaryForEmpDB(int? YearId, int? MonthId)
        {
            return db.getMonthlyAttendanceSummaryForEmpDB(_UserId, YearId, MonthId);
        }
    }
}
