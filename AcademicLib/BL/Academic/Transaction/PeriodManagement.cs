using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class PeriodManagement
    {

        DA.Academic.Transaction.PeriodManagementDB db = null;
        int _UserId = 0;

        public PeriodManagement(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.PeriodManagementDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.PeriodManagement beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Transaction.PeriodManagementCollections GetAllPeriodManagement(int EntityId)
        {
            return db.getAllPeriodManagement(_UserId, EntityId);
        }
        public BE.Academic.Transaction.PeriodManagement GetPeriodManagementById(int EntityId, int TranId)
        {
            return db.getPeriodManagementById(_UserId, EntityId, TranId);
        }
        public AcademicLib.BE.Academic.Transaction.PeriodManagement getPeriodManagementByClassShiftId( int EntityId, int ClassShiftId)
        {
            return db.getPeriodManagementByClassShiftId(_UserId, EntityId, ClassShiftId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.PeriodManagement beData, bool IsModify)
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
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Name";
                //}
                //else if (beData.ShiftId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Shift ";
                //}

                else
                {
                    List<DateTime> dateColl = new List<DateTime>();
                    foreach (var pd in beData.PeriodManagementDetailsColl)
                    {
                        if(pd.StartTime.HasValue && pd.EndTime.HasValue)
                        {
                            dateColl.Add(pd.StartTime.Value);
                            dateColl.Add(pd.EndTime.Value);
                            if (pd.StartTime.Value > pd.EndTime.Value)
                            {
                                resVal.ResponseMSG = "Please ! Enter EndTime Greater then Start Time";
                                resVal.IsSuccess = false;
                                return resVal;
                            }
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Enter StartTime and EndTime";
                            return resVal;
                        }
                    }

                    for(int i = 0; i < dateColl.Count-1; i++)
                    {
                        var sDate = dateColl[i];
                        var eDate = dateColl[i + 1];
                        if (eDate < sDate)
                        {
                            resVal.ResponseMSG = "Please ! Enter Start Time Greater Than End Time";
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
