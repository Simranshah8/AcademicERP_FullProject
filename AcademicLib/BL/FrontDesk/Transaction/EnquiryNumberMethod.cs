using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class EnquiryNumberMethod
    {
        DA.FrontDesk.Transaction.EnquiryNumberMethodDB db = null;
        int _UserId = 0;
        public EnquiryNumberMethod(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.EnquiryNumberMethodDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.EnquiryNumberMethod beData)
        {
            return db.SaveUpdate(beData);
        }
        public BE.FrontDesk.Transaction.EnquiryNumberMethod GetConfiguration(int EntityId, int? BranchId)
        {
            return db.getConfiguration(_UserId, EntityId, BranchId);
        }

    }
}
