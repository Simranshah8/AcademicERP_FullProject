using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Setup
{
    public class ClassWiseAcademicMonth
    {
        DA.Academic.Setup.ClassWiseAcademicMonthDB db = null;
        int _UserId = 0;
        public ClassWiseAcademicMonth(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Setup.ClassWiseAcademicMonthDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Setup.ClassWiseAcademicMonth beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.Academic.Setup.ClassWiseAcademicMonthCollections GetAllClassWiseAcademicMonth(int EntityId)
        {
            return db.getAllClassWiseAcademicMonth(_UserId, EntityId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassId)
        {
            return db.DeleteById(_UserId, EntityId, ClassId);
        }

    }
}
