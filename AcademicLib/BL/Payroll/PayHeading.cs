using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class PayHeading
    {
        DA.Payroll.PayHeadingDB db = null;
        int _UserId = 0;
        public PayHeading(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.PayHeadingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Payroll.PayHeading beData)
        {
            bool isModify = beData.PayHeadingId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Payroll.PayHeadingCollections GetAllPayHeading(int EntityId)
        {
            return db.getAllPayHeading(_UserId, EntityId);
        }

        public AcademicLib.BE.Payroll.BranchForPayHeadingCollections GetBranchForPayHeading(int EntityId)
        {
            return db.getBranchForPayHeading(_UserId, EntityId);
        }
        public AcademicLib.BE.Payroll.CategoryForPayHeadingCollections GetCategoryForPayHeading(int EntityId)
        {
            return db.getCategoryForPayHeading(_UserId, EntityId);
        }
        public AcademicLib.BE.Payroll.PayHeading GetPayHeadingById(int EntityId, int PayHeadingId)
        {
            return db.getPayHeadingById(_UserId, EntityId, PayHeadingId);
        }
        public ResponeValues DeleteById(int EntityId, int PayHeadingId)
        {
            return db.DeleteById(_UserId, EntityId, PayHeadingId);
        }
        public AcademicLib.BE.Payroll.PayHeadingCollections getAllPayHeadingForTran()
        {
            return db.getAllPayHeadingForTran(_UserId);
        }
            public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.PayHeading beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.PayHeadingId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.PayHeadingId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter PayHeading Name";
                }
                else if(!beData.LedgerId.HasValue || beData.LedgerId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Ledger";
                }
                else
                {
                    if (beData.CalculationType == 1)
                    {
                        if(!beData.AttendanceTypeId.HasValue || beData.AttendanceTypeId == 0)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Please ! Select Attendance Type";
                            return resVal;
                        }
                    }else
                    {
                        beData.AttendanceTypeId = null;
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