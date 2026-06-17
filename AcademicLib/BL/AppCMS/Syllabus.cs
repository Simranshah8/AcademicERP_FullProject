using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

    public class Syllabus
    {

        DA.AppCMS.Creation.SyllabusPlanDB db = null;

        int _UserId = 0;

        public Syllabus(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.SyllabusPlanDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.SyllabusPlan beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.SyllabusPlanCollections getAllSyllabus(int EntityId, string BranchCode = "")
        {
            return db.getAllSyllabus(_UserId, EntityId, BranchCode);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }


        public BE.AppCMS.Creation.SyllabusPlan GetSyllabusPlanById(int EntityId, int TranId)
        {
            return db.getSyllabusPlanById(_UserId, EntityId, TranId);
        }

        //public BE.AppCMS.Creation.SyllabusPlanDetails GetSyllabusTopicById(int EntityId, int SyllabusId)
        //{
        //    return db.GetSyllabusTopicById(_UserId, EntityId, SyllabusId);
        //}
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.SyllabusPlan beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Syllabus Name ";
                }

                else
                {
                    if (beData.NoOfSyllabus != beData.DetailsColl.Count)
                    {
                        resVal.ResponseMSG = "No of syllabus and its details does not matched";
                        return resVal;
                    }
                    foreach (BE.AppCMS.Creation.SyllabusPlanDetails syllabusPlanDetails in beData.DetailsColl)
                    {
                        if (syllabusPlanDetails.SNo == 0)
                        {
                            resVal.ResponseMSG = "Please ! Enter S.No. of Topic";
                            return resVal;
                        }
                        if (string.IsNullOrEmpty(syllabusPlanDetails.SyllabusName))
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Enter syllabus Name";
                            return resVal;
                        }
                        foreach (BE.AppCMS.Creation.SyllabusTopic syllabusTopic in syllabusPlanDetails.TopicColl)
                        {
                            if (string.IsNullOrEmpty(syllabusTopic.TopicName))
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Please ! Enter Topic Name Of syllabus(" + syllabusPlanDetails.SNo.ToString() + ")";
                                return resVal;
                            }
                        }
                    }
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
            }

            catch (Exception ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            return resVal;
        }
    }

}

