using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Setup
{
    public class AcademicConfiguration
    {
        DA.Academic.Setup.AcademicConfigurationDB db = null;
        int _UserId = 0;
        public AcademicConfiguration(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Setup.AcademicConfigurationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Setup.AcademicConfiguration beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.Academic.Setup.AcademicConfiguration GetConfiguration(int EntityId)
        {
            return db.getConfiguration(_UserId, EntityId);
        }

    }
}
