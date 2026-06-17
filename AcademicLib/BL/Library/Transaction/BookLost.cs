using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Library.Transaction
{
    public class BookLostDamage
    {
        DA.Library.Transaction.BookLostDB db = null;
        int _UserId = 0;

        public BookLostDamage(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Transaction.BookLostDB(hostName, dbName);
        }

        public ResponeValues SaveFormData(List<BE.Library.Transaction.BookLostDamage> dataColl)
        {
            foreach (var beData in dataColl)
            {
                beData.CUserId = _UserId;
            }
            return db.SaveBookLost(dataColl);
        }
        public BE.Library.Transaction.BookLostCollections GetBookLostDamages(DateTime? DateFrom, DateTime? DateTo)
        {
            return db.GetBookLostDamages(_UserId, DateFrom, DateTo);
        }
        public ResponeValues UpdateBookLostAction(int IssueId, double? RecoveryAmount, bool? IsBookSubmitted)
        {
            return db.UpdateBookLostAction(_UserId, IssueId,  RecoveryAmount, IsBookSubmitted);
        }
    }

}


