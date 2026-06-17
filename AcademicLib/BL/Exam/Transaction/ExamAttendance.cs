using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamAttendance
    {
        DA.Exam.Transaction.ExamAttendanceDB db = null;
        int _UserId = 0;

        public ExamAttendance(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.ExamAttendanceDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ExamwiseAttendenceCollections beData)
        {
          
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, beData);
            else
                return isValid;
        }
        public AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections getExamWiseAttendanceClassWise(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getExamWiseAttendanceClassWise(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId);
        }
        public AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections getAttendanceForExam( int ClassId, int? SectionId, DateTime DateFrom, DateTime DateTo,int? AcademicYearId)
        {
            return db.getAttendanceForExam(_UserId, ClassId, SectionId, DateFrom, DateTo,AcademicYearId);
        }
            public AcademicLib.API.Teacher.StudentForExamAttendanceCollections getStudentForExamAttendance(int AcademicYearId, int ClassId, int? SectionId, int ExamTypeId)
        {
            return db.getStudentForExamAttendance(_UserId,AcademicYearId, ClassId, SectionId, ExamTypeId);
        }
            public BE.Exam.Transaction.ExamAttendanceCollections GetAllExamAttendance(int EntityId)
        {
            return db.getAllExamAttendance(_UserId, EntityId);
        }
        public BE.Exam.Transaction.ExamAttendance GetExamAttendanceById(int EntityId, int ExamAttendanceId)
        {
            return db.getExamAttendanceById(_UserId, EntityId, ExamAttendanceId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamAttendanceId)
        {
            return db.DeleteById(_UserId, EntityId, ExamAttendanceId);
        }

        public ResponeValues ImportExamWiseAttendance( List<BE.Exam.Transaction.ImportExamWiseAttendance> dataColl)
        {
            return db.ImportExamWiseAttendance(_UserId, dataColl);   
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamwiseAttendenceCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (dataColl == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }               
                else
                {
                    int row = 1;
                    foreach(var v in dataColl)
                    {
                        if (v.StudentId == 0)
                        {
                            resVal.ResponseMSG = "Please ! Select Student At Row "+row.ToString();
                            return resVal;
                        }

                        if (v.ExamTypeId == 0)
                        {
                            resVal.ResponseMSG = "Please ! Select ExamType At Row " + row.ToString();
                            return resVal;
                        }

                        if (v.WorkingDays < (v.PresentDays + v.AbsentDays))
                        {
                            resVal.ResponseMSG = "Please ! enter P+A days less then equal working day.";
                            return resVal;
                        }

                        if(v.DateFrom.HasValue && v.DateTo.HasValue)
                        {
                            if (v.DateFrom.Value > v.DateTo.Value)
                            {
                                resVal.ResponseMSG = "Please ! Enter Date To Greater Then DateFrom";
                                return resVal;
                            }
                        }
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
