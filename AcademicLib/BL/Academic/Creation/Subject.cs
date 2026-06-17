using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Subject
    {
        DA.Academic.Creation.SubjectDB db = null;
        int _UserId = 0;
        public Subject(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.SubjectDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Subject beData)
        {
            bool isModify = beData.SubjectId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.SubjectCollections GetAllSubject(int EntityId, int AcademicYearId, int? EmployeeId=null, int? ClassId=null,int? SectionId=null,bool forAllSubject=false, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null,string Role="")
        {
            return db.getAllSubject(_UserId, EntityId,AcademicYearId, EmployeeId,ClassId,SectionId,forAllSubject,BatchId,ClassYearId,SemesterId,Role);
        }
        public BE.Academic.Creation.SubjectCollections getSubjectListForLessonPlan(int ClassId, int? BatchId, int? ClassYearId, int? SemesterId,   int AcademicYearId)
        {
            return db.getSubjectListForLessonPlan(_UserId, ClassId, BatchId, ClassYearId, SemesterId,  AcademicYearId);
        }
        public BE.Academic.Creation.Subject GetSubjectById(int EntityId, int SubjectId)
        {
            return db.getSubjectById(_UserId, EntityId, SubjectId);
        }
        public ResponeValues DeleteById(int EntityId, int SubjectId)
        {
            return db.DeleteById(_UserId, EntityId, SubjectId);
        }

        public ResponeValues ImportSubject( List<BE.Academic.Creation.ImportSubject> dataColl)
        {
            return db.ImportSubject(_UserId, dataColl);  
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Subject beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.SubjectId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.SubjectId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Subject Name";
                }
                else
                {

                    beData.CR = beData.CRTH + beData.CRPR;
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
