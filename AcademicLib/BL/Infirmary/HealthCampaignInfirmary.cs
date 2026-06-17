using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace AcademicLib.BL.Infirmary
{
    public class HCInfirmary
    {
        DA.Infirmary.HCInfirmaryDB db = null;
        int _UserId = 0;
        public HCInfirmary(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Infirmary.HCInfirmaryDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Infirmary.HCInfirmary hCInfirmary)
        {
            ResponeValues isValid = new ResponeValues();
            isValid.IsSuccess = true; isValid.ResponseMSG = "";
            int idx = 0;


            // step 2
            hCInfirmary.CUserId = _UserId;
            hCInfirmary.LogDateTime = DateTime.Now;

            hCInfirmary.CUserId = _UserId;
            hCInfirmary.LogDateTime = DateTime.Now;

            return db.SaveHCInfirmary(hCInfirmary, _UserId);

        }

        public BE.Infirmary.HCInfirmaryInfoCollections getAllHCInfirmarys() => db.getAllHCInfirmarys();

        public ResponeValues IsValidData(BE.Infirmary.HCInfirmary data, int idx)
        {
            ResponeValues resVal = new ResponeValues();

            resVal.IsSuccess = true;
            resVal.ResponseMSG = "Valid";

            return resVal;
        }

        public BE.Infirmary.HCInfirmary getHCInfirmaryById(int hCInfirmaryId) => db.getHCInfirmaryById(hCInfirmaryId);
        public ResponeValues deleteHCInfirmaryById(int hCInfirmaryId) => db.deleteHCInfirmaryById(hCInfirmaryId, _UserId);

        public List<int> getAllHCInfirmarySuccessfullStudents(int hCInfirmaryId) => db.getAllHCInfirmarySuccessfullStudents(hCInfirmaryId);
    }
}