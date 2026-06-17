using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.HomeWork
{
    public class HomeWork
    {
        DA.HomeWork.HomeWorkDB db = null;
        int _UserId = 0;

        public HomeWork(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.HomeWork.HomeWorkDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.HomeWork.HomeWork beData)
        {
            bool isModify = beData.HomeWorkId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues SubmitHomeWork(API.Student.HomeWorkSubmit beData,ref string msg)
        {
              return db.SubmitHomeWork(beData,ref msg);         
        }
        public ResponeValues CheckHomeWork(API.Teacher.HomeWorkChecked beData)
        {
            return db.CheckHomeWork(beData);
        }
        public ResponeValues CheckClassWiseHomeWork(API.Teacher.HomeWorkCheckedCollections dataColl)
        {
            return db.CheckClassWiseHomeWork(_UserId, dataColl);
        }
        public RE.HomeWork.HomeWorkCollections GetAllHomeWork(int EntityId,DateTime? dateFrom,DateTime? dateTo,bool IsStudent=false,int? studentId=null, int? ClassId = null, int? SectionId = null, int? SubjectId = null, int? EmployeeId = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            return db.getAllHomeWork(_UserId, EntityId,dateFrom,dateTo, IsStudent,studentId,ClassId,SectionId,SubjectId,EmployeeId, BatchId, SemesterId, ClassYearId);
        }
        public AcademicLib.RE.HomeWork.HomeWorkDetailsCollections getHomeWorkDetailsById( int HomeWorkId)
        {
            return db.getHomeWorkDetailsById(_UserId, HomeWorkId);
        }
        public BE.HomeWork.HomeWork GetHomeWorkById(int EntityId, int HomeWorkId)
        {
            return db.getHomeWorkById(_UserId, EntityId, HomeWorkId);
        }
        public ResponeValues DeleteById(int EntityId, int HomeWorkId)
        {
            return db.DeleteById(_UserId, EntityId, HomeWorkId);
        }
        public ResponeValues UpdateDeadline(AcademicLib.BE.HomeWork.HomeWork beData)
        {
            ResponeValues isValid = IsValidDeadlineData(ref beData );
            if (isValid.IsSuccess)
                return db.UpdateDeadline(beData);
            else
                return isValid;
        }
        public ResponeValues IsValidDeadlineData(ref BE.HomeWork.HomeWork beData )
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (!beData.HomeWorkId.HasValue || beData.HomeWorkId == 0 )
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }              
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }              
                else if (!beData.DeadlineDate.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter HomeWork Summit Deadline Date";
                }else if (beData.HomeworkTypeId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid Homework Type Name";
                }
                else
                {
                    if (beData.DeadlineDate.HasValue)
                    {
                        var fd = new DateTime(beData.DeadlineDate.Value.Year, beData.DeadlineDate.Value.Month, beData.DeadlineDate.Value.Day);
                        var td = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                        if (fd < td)
                        {
                            resVal.ResponseMSG = "Please ! Enter Deadline greater then current datetime";
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
        public ResponeValues IsValidData(ref BE.HomeWork.HomeWork beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.HomeWorkId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.HomeWorkId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (beData.SubjectId == 0)
                {
                    resVal.ResponseMSG = "Invalid Subject Name";
                }
                else if (beData.HomeworkTypeId == 0)
                {
                    resVal.ResponseMSG = "Invalid HomeWork Type Name";
                }
                else if (!beData.DeadlineDate.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter HomeWork Summit Deadline Date";
                }             
                else
                {
                    if (beData.SectionId.HasValue && beData.SectionId.Value == 0)
                        beData.SectionId = null;

                    if (string.IsNullOrEmpty(beData.SectionIdColl) && beData.SectionId.HasValue)
                        beData.SectionIdColl = beData.SectionId.Value.ToString();

                    if (!string.IsNullOrEmpty(beData.SectionIdColl) && beData.SectionIdColl.Trim() == "0")
                        beData.SectionIdColl = "";

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

                    if(beData.DeadlineTime.Value<DateTime.Now)
                    {
                        resVal.ResponseMSG = "Please ! Enter Deadline Date And Time Greater then Now";
                        return resVal;
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
