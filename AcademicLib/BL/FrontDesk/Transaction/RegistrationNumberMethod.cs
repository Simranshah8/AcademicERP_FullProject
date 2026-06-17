using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class RegistrationNumberMethod
    {
        DA.FrontDesk.Transaction.RegistrationNumberMethodDB db = null;
        int _UserId = 0;
        public RegistrationNumberMethod(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.RegistrationNumberMethodDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.RegistrationNumberMethod beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.FrontDesk.Transaction.RegistrationNumberMethod GetConfiguration(int EntityId, int? BranchId)
        {
            return db.getConfiguration(_UserId, EntityId, BranchId);
        }

    }
}
