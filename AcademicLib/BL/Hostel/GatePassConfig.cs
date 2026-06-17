using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class GatePassConfig
    {
        DA.Hostel.GatePassConfigDB db = null;
        int _UserId = 0;
        public GatePassConfig(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.GatePassConfigDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Hostel.GatePassConfig beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.Hostel.GatePassConfig GetConfiguration(int EntityId, int? BranchId)
        {
            return db.getConfiguration(_UserId, EntityId, BranchId);
        }

    }
}
