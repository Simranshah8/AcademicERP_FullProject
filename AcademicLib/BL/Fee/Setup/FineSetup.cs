using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Setup
{
    public class FineSetup
    {
        DA.Fee.Setup.FineSetupDB db = null;
        int _UserId = 0;
        public FineSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Setup.FineSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Setup.Fine beData, int AcademicYearId)
        {
            return db.SaveUpdate(beData,AcademicYearId);
        }
 
        public BE.Fee.Setup.FineSetupCollections GetAllFineSetup(int EntityId)
        {
            return db.getAllFineSetup(_UserId, EntityId);
        }

        public BE.Fee.Setup.Fine GetFineSetup(int AcademicYearId)
        {
            return db.getFineSetup(_UserId, AcademicYearId);
        }
        public ResponeValues DeleteById(int AcademicYearId)
        {
            return db.DeleteById(_UserId, AcademicYearId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Setup.FineSetup beData, bool IsModify)
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
                
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter FineSetup Name";
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