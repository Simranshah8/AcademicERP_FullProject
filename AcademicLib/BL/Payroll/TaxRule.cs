using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class TaxRule
    {
        DA.Payroll.TaxRuleDB db = null;
        int _UserId = 0;
        public TaxRule(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.TaxRuleDB(hostName, dbName);
        }

        public ResponeValues Update(List<AcademicLib.BE.Payroll.TaxRule> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.Update(_UserId, dataColl);

            return resVal;
        }
        //public ResponeValues SaveFormData(AcademicLib.BE.Payroll.TaxRule beData)
        //{
        //    bool isModify = beData.TranId > 0;
        //    ResponeValues isValid = IsValidData(ref beData, isModify);
        //    if (isValid.IsSuccess)
        //        return db.SaveUpdate(beData, isModify);
        //    else
        //        return isValid;
        //}
        public AcademicLib.BE.Payroll.TaxRuleCollections GetAllTaxRule(int EntityId, int? TaxFor)
        {
            return db.getAllTaxRule(_UserId, EntityId, TaxFor);
        }

        //public AcademicLib.BE.Payroll.TaxRule GetTaxRuleById(int EntityId, int TranId)
        //{
        //    return db.getTaxRuleById(_UserId, EntityId, TranId);
        //}
        //public ResponeValues DeleteById(int EntityId, int TranId)
        //{
        //    return db.DeleteById(_UserId, EntityId, TranId);
        //}
        public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.TaxRule beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TaxRuleId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TaxRuleId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter TaxRule Name";
                //}
                else
                {
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