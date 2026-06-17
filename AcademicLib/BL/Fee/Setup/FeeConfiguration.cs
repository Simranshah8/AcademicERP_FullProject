using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Setup
{
    public class FeeConfiguration
    {
        DA.Fee.Setup.FeeConfigurationDB db = null;
        int _UserId = 0;

        public FeeConfiguration(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Setup.FeeConfigurationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Setup.FeeConfiguration beData)
        {
            return db.SaveUpdate(beData);
        }      
        public BE.Fee.Setup.FeeConfiguration GetFeeConfigurationById(int EntityId,int? AcademicYearId)
        {
            return db.getFeeConfigurationById(_UserId, EntityId,AcademicYearId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues MergeStudentOpening( int AcademicYearId, int FeeItemId)
        {
            return db.MergeStudentOpening(_UserId, AcademicYearId, FeeItemId);
        }
        public ResponeValues GenerateSalesInvoice( int AcademicYearId, bool IsReGenerate, DateTime? DateFrom, DateTime? DateTo)
        {
            return db.GenerateSalesInvoice(_UserId, AcademicYearId, IsReGenerate,DateFrom,DateTo);
        }

            public ResponeValues UpdateMissingLeftStudent(  int FromAcademicYearId, int ToAcademicYearId)
        {
            return db.UpdateMissingLeftStudent(_UserId, FromAcademicYearId, ToAcademicYearId);
        }

        public ResponeValues UpdateMissingSalesInvoice()
        {
            return db.UpdateMissingSalesInvoice(_UserId);
        }
            public AcademicLib.BE.Fee.Setup.FeeDefaulterMinDues getRecTemplateTranId(int EntityId, int? AcademicYearId)
        {
            return db.getRecTemplateTranId(_UserId, EntityId, AcademicYearId);
        }
        public ResponeValues MissingFeeToAccount(DateTime? DateFrom, DateTime? DateTo)
        {
            return db.MissingFeeToAccount(_UserId, DateFrom, DateTo);
        }
        public ResponeValues IsValidData(ref BE.Fee.Setup.FeeConfiguration beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }               
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Logo))
                //{
                //    resVal.ResponseMSG = "Please ! Select Logo";
                //}
                else
                {

                    if (beData.BillGenerate_VoucherId.HasValue && beData.BillGenerate_VoucherId == 0)
                        beData.BillGenerate_VoucherId = null;

                    if (beData.FeeReceipt_VoucherId.HasValue && beData.FeeReceipt_VoucherId == 0)
                        beData.FeeReceipt_VoucherId = null;

                    if (beData.DebitNote_VoucherId.HasValue && beData.DebitNote_VoucherId == 0)
                        beData.DebitNote_VoucherId = null;

                    if (beData.CreditNote_VoucherId.HasValue && beData.CreditNote_VoucherId == 0)
                        beData.CreditNote_VoucherId = null;


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
