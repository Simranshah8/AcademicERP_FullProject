using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.HomeWork
{
    public class Assignment
    {
        DA.HomeWork.AssignmentDB db = null;
        int _UserId = 0;

        public Assignment(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.HomeWork.AssignmentDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.HomeWork.Assignment beData)
        {
            bool isModify = beData.AssignmentId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues UpdateDeadline(AcademicLib.BE.HomeWork.Assignment beData)
        {
            ResponeValues isValid = IsValidDeadlineData(ref beData);
            if (isValid.IsSuccess)
                return db.UpdateDeadline(beData);
            else
                return isValid;
        }
        public ResponeValues IsValidDeadlineData(ref BE.HomeWork.Assignment beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (!beData.AssignmentId.HasValue || beData.AssignmentId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (!beData.DeadlineDate.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Assigment Summit Deadline Date";
                }
                else if (beData.AssignmentTypeId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Assigment Type Name";
                }
                else
                {
                    if (beData.DeadlineDate.HasValue)
                    {
                        var fd = new DateTime(beData.DeadlineDate.Value.Year, beData.DeadlineDate.Value.Month, beData.DeadlineDate.Value.Day);
                        var td = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                        if (fd < td)
                        {
                            resVal.ResponseMSG = "Please ! Enter Deadline greate then now datetime";
                            return resVal;
                        }
                    }
                    if (beData.DeadlineTime.HasValue)
                    {
                        var dd = beData.DeadlineDate.Value;
                        beData.DeadlineTime = new DateTime(dd.Year, dd.Month, dd.Day, beData.DeadlineTime.Value.Hour, beData.DeadlineTime.Value.Minute, beData.DeadlineTime.Value.Second);
                    }
                    else
                    {
                        var td = DateTime.Today;
                        beData.DeadlineTime = new DateTime(td.Year, td.Month, td.Day, 24, 0, 0);
                    }

                    if (beData.DeadlineTime.Value < DateTime.Now)
                    {
                        resVal.ResponseMSG = "Please ! Enter Deadline Date And Time Greater then Now";
                        return resVal;
                    }

                    if (beData.DeadlineforRedoTime.HasValue && beData.DeadlineforRedo.HasValue)
                    {
                        var dd = beData.DeadlineforRedo.Value;
                        beData.DeadlineTime = new DateTime(dd.Year, dd.Month, dd.Day, beData.DeadlineforRedoTime.Value.Hour, beData.DeadlineforRedoTime.Value.Minute, beData.DeadlineforRedoTime.Value.Second);
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
        public ResponeValues SubmitAssignment(API.Student.AssignmentSubmit beData, ref string msg)
        {
            return db.SubmitAssignment(beData,ref msg);
        }
        public ResponeValues CheckAssignment(API.Teacher.AssignmentChecked beData)
        {
            return db.CheckAssignment(beData);
        }
        public ResponeValues CheckClassWiseAssignment( AcademicLib.API.Teacher.AssignmentCheckedCollections dataColl)
        {
            return db.CheckClassWiseAssignment(_UserId, dataColl); 
        }
            public RE.HomeWork.AssignmentCollections GetAllAssignment(int EntityId, DateTime? dateFrom, DateTime? dateTo, bool isStudent = false, int? studentId = null, int? ClassId = null, int? SectionId = null, int? SubjectId = null, int? EmployeeId = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.getAllAssignment(_UserId, EntityId, dateFrom, dateTo, isStudent, studentId, ClassId, SectionId, SubjectId, EmployeeId, BatchId, SemesterId, ClassYearId);
        }
        public AcademicLib.RE.HomeWork.AssignmentDetailsCollections getAssignmentDetailsById(int AssignmentId)
        {
            return db.getAssignmentDetailsById(_UserId, AssignmentId);
        }
        public BE.HomeWork.Assignment GetAssignmentById(int EntityId, int AssignmentId)
        {
            return db.getAssignmentById(_UserId, EntityId, AssignmentId);
        }
        public ResponeValues DeleteById(int EntityId, int AssignmentId)
        {
            return db.DeleteById(_UserId, EntityId, AssignmentId);
        }
        public ResponeValues IsValidData(ref BE.HomeWork.Assignment beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.AssignmentId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.AssignmentId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Title ";
                }
                else
                {
                    if (beData.SectionId.HasValue && beData.SectionId.Value == 0)
                        beData.SectionId = null;

                    if (beData.DeadlineTime.HasValue)
                    {
                        var dd = beData.DeadlineDate.Value;
                        beData.DeadlineTime = new DateTime(dd.Year, dd.Month, dd.Day, beData.DeadlineTime.Value.Hour, beData.DeadlineTime.Value.Minute, beData.DeadlineTime.Value.Second);
                    }

                    if (beData.AttachmentColl != null && beData.AttachmentColl.Count > 0)
                    {
                        var tmpAcademicColl = beData.AttachmentColl;
                        beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                        foreach (var aDet in tmpAcademicColl)
                        {
                            if (!string.IsNullOrEmpty(aDet.DocPath))
                            {
                                beData.AttachmentColl.Add(aDet);
                            }
                        }
                    }

                    if(beData.MarkScheme==1 && beData.Marks==0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Enter Mark";
                        return resVal;
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
