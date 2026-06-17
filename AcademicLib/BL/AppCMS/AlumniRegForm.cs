using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class AlumniReg
	{

		DA.AppCMS.Creation.AlumniRegFormDB db = null;

		int _UserId = 0;

		public AlumniReg(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.AlumniRegFormDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.AlumniReg beData)
		{
			bool isModify = beData.AlumniRegId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
		public BE.AppCMS.Creation.AlumniRegCollections GetAllAlumni()
		{
			return db.getAllAlumni(_UserId);
		}
        public ResponeValues DeleteById(int AlumniRegId)
        {
            return db.DeleteById(_UserId, AlumniRegId);
        }

        public BE.AppCMS.Creation.AlumniReg GetAlumniRegById(int AlumniRegId)
        {
            return db.getAlumniRegById(_UserId, AlumniRegId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.AlumniReg beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.AlumniRegId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.AlumniRegId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.FullName))
                {
                    resVal.ResponseMSG = "Please ! Enter FullName ";
                }
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

