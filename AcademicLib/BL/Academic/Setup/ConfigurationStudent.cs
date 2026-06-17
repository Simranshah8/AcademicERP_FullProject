using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Setup
{
    public class ConfigurationStudent
    {
        DA.Academic.Setup.ConfigurationStudentDB db = null;
        int _UserId = 0;
        public ConfigurationStudent(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Setup.ConfigurationStudentDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Setup.ConfigurationStudent beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.Academic.Setup.ConfigurationStudent GetConfiguration(int EntityId,int? BranchId)
        {
            return db.getConfiguration(_UserId, EntityId,BranchId);
        }
       
    }
}
