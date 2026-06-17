using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class LibrarySetting
    {
        DA.Library.Creation.LibrarySettingDB db = null;
        int _UserId = 0;
        public LibrarySetting(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.LibrarySettingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.Setup beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
            {
                beData.Student.CUserId = _UserId;
                beData.Teacher.CUserId = _UserId;
                
                var res1= db.SaveLibrarySettingStudent(beData.Student);   
                
                if(beData.Teacher!=null)
                    res1 = db.SaveLibrarySettingTeacher(beData.Teacher);

                if(beData.ClassWise!=null && beData.ClassWise.Count>0)
                    res1 = db.SaveLibrarySettingClassWise(_UserId, beData.ClassWise);

                if (beData.CategoryWise != null && beData.CategoryWise.Count > 0)
                    res1 = db.SaveLibrarySettingCategoryWise(_UserId, beData.CategoryWise);


                return res1;
            }                
            else
                return isValid;
        }
        public AcademicLib.BE.Library.Creation.Setup getSetting()
        {
            return db.getSetting(_UserId);
        }
            public ResponeValues IsValidData(ref BE.Library.Creation.Setup beData)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
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
        public ResponeValues DeleteBookIssueClassWiseById(int TranId, int ClassId)
        {
            return db.DeleteBookIssueClassWiseById(_UserId, TranId, ClassId);
        }

    }
}
