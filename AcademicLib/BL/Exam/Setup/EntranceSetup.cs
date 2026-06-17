using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Setup
{
    public class EntranceSetup
    {
        DA.Exam.Setup.EntranceSetupDB db = null;
        int _UserId = 0;
        public EntranceSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Setup.EntranceSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Setup.EntranceSetup beData, int BranchId,int AcademicYearId)
        {
            return db.SaveUpdate(beData, BranchId, AcademicYearId);
        }
        public BE.Exam.Setup.EntranceSetup GetEntranceSetup(int EntityId)
        {
            return db.getEntranceSetup(_UserId, EntityId);
        }

        public BE.Exam.Setup.EntranceCardDataCollections GetDataForEntranceCard(DateTime? DateFrom, DateTime? DateTo)
        {
            return db.getDataForEntranceCard(_UserId, DateFrom, DateTo);
        }
    }
}
