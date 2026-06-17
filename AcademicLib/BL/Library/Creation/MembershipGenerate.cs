using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class MembershipGenerate
    {
        DA.Library.Creation.MembershipGenerateDB db = null;
        int _UserId = 0;
        public MembershipGenerate(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.MembershipGenerateDB(hostName, dbName);
        }
        public ResponeValues GenerateMembership(int AcademicYearId, BE.Library.Creation.MembershipGenerate beData)
        {
           
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.GenerateMembership(AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Library.Creation.StudentMemberCollections getClassWiseMemberlist(int AcademicYearId, int ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            return db.getClassWiseMemberlist(_UserId, AcademicYearId, ClassId, SectionId, BatchId, ClassYearId, SemesterId);
        }

        public BE.Library.Creation.EmployeeMemberCollections getEmpMemberlist()
        {
            return db.getEmpMemberlist(_UserId);           
        }
            public ResponeValues IsValidData(ref BE.Library.Creation.MembershipGenerate beData)
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
