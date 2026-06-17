using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Exam.Transaction
{
    public class EntranceSymbolNo
    {
        DA.Exam.Transaction.EntranceSymbolNoDB db = null;

        int _UserId = 0;
        public EntranceSymbolNo(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.EntranceSymbolNoDB(hostName, dbName);
        }
        public BE.Exam.Transaction.SymboolNoCollections GetEntranceSymboolNo(int? ClassId)
        {
            return db.GetEntranceSymbolNo(_UserId, ClassId);
        }
        public ResponeValues SaveFormData(List<BE.Exam.Transaction.EntranceSymbolNo> dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            resVal = db.SaveUpdate(_UserId, dataColl);
            return resVal;

        }

    }
}