using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class ExamRoom
    {
        DA.Exam.Transaction.ExamRoomDB db = null;
        int _UserId = 0;

        public ExamRoom(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.Exam.Transaction.ExamRoomDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.ExamRoom beData)
        {
            bool isModify = beData.RoomId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.ExamRoomCollections GetAllExamRoom(int EntityId)
        {
            return db.getAllExamRoom(_UserId, EntityId);
        }
        public BE.Exam.Transaction.ExamRoom GetExamRoomById(int EntityId, int RoomId)
        {
            return db.getExamRoomById(_UserId, EntityId, RoomId);
        }
        public ResponeValues DeleteById(int EntityId, int RoomId)
        {
            return db.DeleteById(_UserId, EntityId, RoomId);
        }
        public BE.Exam.Transaction.ExamRoomCollections GetExamShiftWiseRoom(int EntityId, int ExamTypeId, int ExamShiftId)
        {
            return db.getExamShiftWiseRoom(_UserId, EntityId, ExamTypeId, ExamShiftId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.ExamRoom beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RoomId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RoomId != 0)
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
                else if (beData.NoOfBanchRow==0)
                {
                    resVal.ResponseMSG = "Please ! Enter No Of Banch Row";
                }
                else if (beData.TotalBanch == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Total Banch In Room";
                }else if (beData.NoOfBanchRow != beData.DetailColl.Count)
                {
                    resVal.ResponseMSG = "No Of Banch Row Does Not Match";
                }
                else if(beData.TotalBanch!=beData.DetailColl.Sum(p1=>p1.NoOfBanch))
                {
                    resVal.ResponseMSG = "No Of Banch Row Does Not Match";
                }
                else
                {
                    if (beData.DetailColl != null)
                    {
                        foreach(var det in beData.DetailColl)
                        {
                            if(string.IsNullOrEmpty(det.Banch_Row_Name))
                            {
                                resVal.ResponseMSG = "Please ! Enter Column Name";
                                return resVal;
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
