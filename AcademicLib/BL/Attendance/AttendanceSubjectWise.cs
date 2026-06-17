using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Attendance
{
    public class AttendanceSubjectWise
    {
        DA.Attendance.AttendanceSubjectWiseDB db = null;
        int _UserId = 0;
        public AttendanceSubjectWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Attendance.AttendanceSubjectWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Attendance.AttendanceSubjectWiseCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Attendance.AttendanceSubjectWiseCollections getClassWiseAttendance(int AcademicYearId, int ClassId, int? SectionId, int SubjectId, DateTime forDate, int InOutMode = 2, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null, int? PeriodId = null)
        {
            return db.getClassWiseAttendance(_UserId, AcademicYearId, ClassId, SectionId, SubjectId, forDate, InOutMode, BatchId, SemesterId, ClassYearId, PeriodId);

        }
        public ResponeValues DeleteSubjectWiseAttendance(DateTime? ForDate, int? ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AttendaneTypeId, int? PeriodId)
        {
            return db.DeleteSubjectWiseAttendance(_UserId, ForDate, ClassId, SectionId, BatchId, ClassYearId, SemesterId, AttendaneTypeId, PeriodId);
        }
        public AcademicLib.API.Teacher.AttendanceSummaryCollections getClassWiseSummary(int AcademicYearId, int? ClassId, int? SectionId, int? SubjectId, DateTime? fromDate, DateTime? toDate)
        {
            return db.getClassWiseSummary(_UserId,AcademicYearId, ClassId, SectionId, SubjectId, fromDate, toDate);
        }
            public ResponeValues IsValidData(ref BE.Attendance.AttendanceSubjectWiseCollections beData)
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
                        if(data.Attendance==null || data.Attendance == 0)
                        {
                            resVal.ResponseMSG = "Please ! Enter Attendance Details At Row "+row.ToString();
                            return resVal;
                        }

                        if(data.SubjectId==0)
                        {
                            resVal.ResponseMSG = "Please ! select valid Subject Name At Row "+row.ToString();
                            return resVal;
                        }

                        if (data.ForDate.Year < 2020)
                        {
                            resVal.ResponseMSG = "Please ! Enter For Date";
                            return resVal;
                        }

                        if (data.Attendance.HasValue)
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
    }
}
