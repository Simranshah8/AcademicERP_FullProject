using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Setup
{
    public class ConfigurationEmployee
    {
        DA.Academic.Setup.ConfigurationEmployeeDB db = null;
        int _UserId = 0;
        public ConfigurationEmployee(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Setup.ConfigurationEmployeeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Setup.ConfigurationEmployee beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.Academic.Setup.ConfigurationEmployee GetConfiguuration(int EntityId)
        {
            return db.getConfiguuration(_UserId, EntityId);
        }
       

    }
}
