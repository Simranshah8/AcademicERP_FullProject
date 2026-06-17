using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Class
    {
        DA.Academic.Creation.ClassDB db = null;
        int _UserId = 0;
        public Class(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.ClassDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Class beData)
        {
            bool isModify = beData.ClassId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues UpdateClassForOR(BE.Academic.Creation.ClassCollections dataColl)
        {
            return db.UpdateClassForOR(_UserId, dataColl);
        }
            public BE.Academic.Creation.ClassCollections GetAllClass(int EntityId,bool forOnlineRegistration= false)
        {
            return db.getAllClass(_UserId, EntityId,forOnlineRegistration);
        }
        public BE.Academic.Creation.Class GetClassById(int EntityId,int ClassId,int AcademicYearId)
        {
            return db.getClassById(_UserId, EntityId, ClassId,AcademicYearId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassId)
        {
            return db.DeleteById(_UserId, EntityId, ClassId);
        }
        public BE.Academic.Creation.ClassSectionList getClassSectionList(int AcademicYearId, bool ForRunning,string Role)
        {
            return db.getClassSectionList(_UserId,AcademicYearId,ForRunning,Role);
        }
        public ResponeValues GetClassSetupAutoOrderNo(int EntityId, int? For)
        {
            return db.GetClassSetupAutoOrderNo(_UserId, EntityId, For); 
        }
            public ResponeValues IsValidData(ref BE.Academic.Creation.Class beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ClassId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ClassId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Class Name";
                }
                else
                {

                    if (beData.RunningAcademicYearId.HasValue && beData.RunningAcademicYearId == 0)
                        beData.RunningAcademicYearId = null;

                    if (beData.ClassType == 0)
                        beData.ClassType = 1;

                    if (beData.FacultyId.HasValue && beData.FacultyId == 0)
                        beData.FacultyId = null;

                    if (beData.LevelId.HasValue && beData.LevelId == 0)
                        beData.LevelId = null;

                    if (beData.ClassType==2)
                    {
                        if(beData.ClassYearIdColl==null || beData.ClassYearIdColl.Count==0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Select Class Year Name";
                            return resVal;
                        }
                    }

                    if (beData.ClassType == 3)
                    {
                        if (beData.ClassSemesterIdColl == null || beData.ClassSemesterIdColl.Count == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Select Class Semester Name";
                            return resVal;
                        }
                    }

                    if(beData.ClassSemesterIdColl!=null && beData.ClassSemesterIdColl.Count>0 && beData.ClassYearIdColl!=null  && beData.ClassYearIdColl.Count > 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Select Year or Semester any one";
                        return resVal;
                    }

                    if(beData.StartMonthId>0 && beData.EndMonthId == 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Select End Month";
                        return resVal;
                    }

                    if(beData.StartMonthId==0 && beData.EndMonthId > 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! Select Start Month";
                        return resVal;
                    }

                    if(beData.StartMonthId>0 && beData.EndMonthId > 0)
                    {
                        if(beData.AcademicMonthColl==null || beData.AcademicMonthColl.Count == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Enter  Month Details";
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
