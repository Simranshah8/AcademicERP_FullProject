using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Setup
{
    public class UpgradeStudentClass
    {
        DA.Academic.Setup.UpgradeStudentClassDB db = null;
        int _UserId = 0;
        public UpgradeStudentClass(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.Academic.Setup.UpgradeStudentClassDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Setup.UpgradeStudentClass beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.Academic.Setup.UpgradeStudentClassCollections GetAllUpgradeStudentClass(int EntityId)
        {
            return db.getAllUpgradeStudentClass(_UserId, EntityId);
        }       
        public ResponeValues DeleteById(int EntityId, int ClassId)
        {
            return db.DeleteById(_UserId, EntityId, ClassId);
        }
     
    }
}
