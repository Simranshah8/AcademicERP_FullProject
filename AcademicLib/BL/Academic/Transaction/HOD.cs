using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class HOD
    {

        DA.Academic.Transaction.HODDB db = null;
        int _UserId = 0;

        public HOD(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.HODDB(hostName, dbName);
        }
 
        public ResponeValues SaveFormData(int? AcademicYearId, BE.Academic.Transaction.ClassHODCollections beData)
        {
            return db.SaveUpdate(_UserId,AcademicYearId,beData);
        }
        public BE.Academic.Transaction.ClassHODCollections GetAllHOD(int DepartmentId, int EmployeeId, int ShiftId, int? SubjectId,int? AcademicYearId)
        {
            return db.getAllHOD(_UserId, DepartmentId, EmployeeId, ShiftId, SubjectId,AcademicYearId);
        }
        public BE.Academic.Transaction.ClassHODCollections GetAllHOD()
        {
            return db.getAllHOD(_UserId);
        }
        public ResponeValues DeleteById(int DepartmentId,int EmployeeId,int ShiftId,int? AcademicYearId)
        {
            return db.DeleteById(_UserId, DepartmentId, EmployeeId, ShiftId,AcademicYearId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.HOD beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.HODId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.HODId != 0)
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
