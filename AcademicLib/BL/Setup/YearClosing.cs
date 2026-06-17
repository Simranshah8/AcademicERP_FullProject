using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class YearClosing
    {
        DA.Setup.YearClosingDB db = null;
        int _UserId = 0;

        public YearClosing(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.YearClosingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Setup.YearClosing beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, beData);
            else
                return isValid;
        }
        public ResponeValues ReSaveUpdate(AcademicLib.BE.Setup.YearClosing beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.ReSaveUpdate(_UserId, beData);
            else
                return isValid;
        }
            public ResponeValues ReClosingStudentFee(AcademicLib.BE.Setup.YearClosing beData)
        {
            return db.ReClosingStudentFee(_UserId, beData);
        }
            public ResponeValues UpdateAcademicYear(BE.Setup.YearClosing beData)
        {
            return db.UpdateAcademicYear(_UserId, beData);
        }
        public ResponeValues generateOTP()
        {
            return db.generateOTP(_UserId);
        }
        public ResponeValues ReClosingStudentTransport(AcademicLib.BE.Setup.YearClosing beData)
        {
            return db.ReClosingStudentTransport(_UserId, beData);
        }
        public ResponeValues IsValidData(ref BE.Setup.YearClosing beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }else if (beData.FromAcademicYearId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid From Academic Year";
                }
                else if (beData.ToAcademicYearId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Valid To Academic Year";
                }
                else
                {
                    foreach(var v in beData.ClassColl)
                    {
                        if (beData.ExamAs == 1)
                            v.ExamTypeGroupId = 0;
                        else
                            v.ExamTypeId = 0;
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
