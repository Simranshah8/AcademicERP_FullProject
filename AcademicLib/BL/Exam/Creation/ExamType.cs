using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Creation
{
    public class ExamType
    {
        DA.Exam.Creation.ExamTypeDB db = null;
        int _UserId = 0;

        public ExamType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Creation.ExamTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Exam.Creation.ExamType beData)
        {
            bool isModify = beData.ExamTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(AcademicYearId, beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Creation.ExamTypeCollections GetAllExamType(int AcademicYearId, int EntityId, int? ForEntity = null)
        {
            return db.getAllExamType(_UserId, AcademicYearId, EntityId, ForEntity);
        }
        public BE.Exam.Creation.ExamType GetExamTypeById(int EntityId, int ExamTypeId)
        {
            return db.getExamTypeById(_UserId, EntityId, ExamTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int ExamTypeId)
        {
            return db.DeleteById(_UserId, EntityId, ExamTypeId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Creation.ExamType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ExamTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ExamTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Name";
                }
                else if (string.IsNullOrEmpty(beData.DisplayName))
                {
                    resVal.ResponseMSG = "Please ! Enter Display Name";
                }
                else
                {
                    if (beData.ResultDate.HasValue && beData.ResultTime.HasValue)
                    {
                        var dt = beData.ResultDate.Value;
                        var t = beData.ResultTime.Value;
                        beData.ResultDate = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        beData.ResultTime = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                    }
                    else if (beData.ResultDate.HasValue && !beData.ResultTime.HasValue)
                    {
                        var dt = beData.ResultDate.Value;
                        beData.ResultDate = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                        beData.ResultTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                    }

                    if (!beData.IsOnlineExam)
                    {
                        beData.ExamDate = null;
                        beData.StartTime = null;
                        beData.Duration = 0;
                    }
                    else if (beData.IsOnlineExam)
                    {
                        if (beData.ExamDate.HasValue && beData.StartTime.HasValue)
                        {
                            var d = beData.ExamDate.Value;
                            beData.StartTime = new DateTime(d.Year, d.Month, d.Day, beData.StartTime.Value.Hour, beData.StartTime.Value.Minute, beData.StartTime.Value.Second);
                        }
                    }

                    if (beData.MarkSubmitDeadline_Teacher.HasValue && beData.TeacherTime.HasValue)
                    {
                        var dt = beData.MarkSubmitDeadline_Teacher.Value;
                        var t = beData.TeacherTime.Value;
                        beData.MarkSubmitDeadline_Teacher = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                    }
                    else if (beData.MarkSubmitDeadline_Teacher.HasValue)
                    {
                        var dt = beData.MarkSubmitDeadline_Teacher.Value;
                        beData.MarkSubmitDeadline_Teacher = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                    }

                    if (beData.AdminTime.HasValue && beData.MarkSubmitDeadline_Admin.HasValue)
                    {
                        var dt = beData.MarkSubmitDeadline_Admin.Value;
                        var t = beData.AdminTime.Value;
                        beData.MarkSubmitDeadline_Admin = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                    }
                    else if (beData.MarkSubmitDeadline_Admin.HasValue)
                    {
                        var dt = beData.MarkSubmitDeadline_Admin.Value;
                        beData.MarkSubmitDeadline_Admin = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                    }

                    if (beData.ResultTime.HasValue && beData.MarkSubmitDeadline_Teacher.HasValue)
                    {
                        if (beData.MarkSubmitDeadline_Teacher.Value > beData.ResultTime)
                        {
                            resVal.ResponseMSG = "Please enter Mark submit Date Time before the Result Date Time.";
                            resVal.IsSuccess = false;
                            return resVal;
                        }
                    }

                    //if (beData.ResultTime.HasValue && beData.MarkSubmitDeadline_Admin.HasValue)
                    //{
                    //    if (beData.MarkSubmitDeadline_Admin.Value > beData.ResultTime)
                    //    {
                    //        resVal.ResponseMSG = "Please enter Mark submit Date Time before the Result Date Time.";
                    //        resVal.IsSuccess = false;
                    //        return resVal;
                    //    }
                    //}



                    foreach (var v in beData.ClassWiseColl)
                    {
                        if (v.ResultDateTime.HasValue && v.ResultTime.HasValue)
                        {
                            var dt = v.ResultDateTime.Value;
                            var t = v.ResultTime.Value;
                            v.ResultDateTime = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        }


                        if (v.MarkSubmitDeadline_Teacher.HasValue && v.TeacherTime.HasValue)
                        {
                            var dt = v.MarkSubmitDeadline_Teacher.Value;
                            var t = v.TeacherTime.Value;
                            v.MarkSubmitDeadline_Teacher = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        }

                        if (v.ResultDateTime.HasValue && beData.ExamDate.HasValue)
                        {
                            if (v.ResultDateTime.Value < beData.ExamDate.Value)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please enter Exam Date before the Result Date.";
                                return resVal;
                            }
                        }

                        //added
                        // Class-wise Result Date and Time handling
                        if (v.ResultDateTime.HasValue && v.ResultTime.HasValue)
                        {
                            var dt = v.ResultDateTime.Value;
                            var t = v.ResultTime.Value;
                            v.ResultDateTime = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        }
                        else if (v.ResultDateTime.HasValue && !v.ResultTime.HasValue)
                        {
                            var dt = v.ResultDateTime.Value;
                            v.ResultDateTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                            v.ResultTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                        }

                        // Class-wise Mark Submission Deadline
                        if (v.MarkSubmitDeadline_Teacher.HasValue && v.TeacherTime.HasValue)
                        {
                            var dt = v.MarkSubmitDeadline_Teacher.Value;
                            var t = v.TeacherTime.Value;
                            v.MarkSubmitDeadline_Teacher = new DateTime(dt.Year, dt.Month, dt.Day, t.Hour, t.Minute, t.Second);
                        }
                        else if (v.MarkSubmitDeadline_Teacher.HasValue)
                        {
                            var dt = v.MarkSubmitDeadline_Teacher.Value;
                            v.MarkSubmitDeadline_Teacher = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
                        }

                        // Validate Class-wise Result Date against Exam Date
                        if (v.ResultDateTime.HasValue && beData.ExamDate.HasValue)
                        {
                            if (v.ResultDateTime.Value < beData.ExamDate.Value)
                            {
                                resVal.ResponseMSG = "Please enter Exam Date before the Result Date for Class.";
                                resVal.IsSuccess = false;
                                return resVal;
                            }
                        }

                        // Validate Class-wise Mark Submission against Result Date
                        if (v.ResultDateTime.HasValue && v.MarkSubmitDeadline_Teacher.HasValue)
                        {
                            if (v.MarkSubmitDeadline_Teacher.Value > v.ResultDateTime.Value)
                            {
                                resVal.ResponseMSG = "Please enter Mark Submission Deadline for Class before Result Date";
                                resVal.IsSuccess = false;
                                return resVal;
                            }
                        }

                        //Ends
                    }


                    if (beData.ExamDate.HasValue && beData.ResultDate.HasValue)
                    {
                        if (beData.ResultDate.Value < beData.ExamDate.Value)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please enter Exam Date before the Result Date.";
                            return resVal;
                        }
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
