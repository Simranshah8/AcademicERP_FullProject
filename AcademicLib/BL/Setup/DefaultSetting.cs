using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class DefaultSetting
    {
        DA.Setup.DefaultSettingDB db = null;
        int _UserId = 0;
        public DefaultSetting(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.DefaultSettingDB(hostName, dbName);
        }
        public ResponeValues SaveDefaultSettingTy(List<BE.Setup.DefaultSetting> DataColl)
        {
            return db.SaveUpdate(_UserId, DataColl);
        }
        public BE.Setup.DefaultSettingCollections GetDefaultSettingById(int ModuleId, int EntityId, int? SubEntityId)
        {
            return db.GetDefaultSettingById(_UserId, ModuleId, EntityId, SubEntityId);
        }
    }
}
