using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Transaction
{
    public class BookReceived
    {
        DA.Library.Transaction.BookReceivedDB db = null;
        int _UserId = 0;
        public BookReceived(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Transaction.BookReceivedDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(List<BE.Library.Transaction.BookReceived> dataColl)
        {
            foreach(var beData in dataColl)
            {
                beData.CUserId = _UserId;
            }
            return db.SaveUpdate(dataColl);
        }
     
    }
}
