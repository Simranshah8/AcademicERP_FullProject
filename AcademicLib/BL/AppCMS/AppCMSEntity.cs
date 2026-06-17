using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class AppCMSEntity
    {
        DA.AppCMS.Creation.AppCMSEntityDB db = null;
        int _UserId = 0;

        public AppCMSEntity(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.AppCMSEntityDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.AppCMSEntityCollections dataColl)
        {
            return db.SaveUpdate(_UserId, dataColl);
        }
     
        public BE.AppCMS.Creation.AppCMSEntityCollections GetEntity(string BranchCode)
        {
            return db.getEntity(_UserId, BranchCode);
        }
       

    }
}
