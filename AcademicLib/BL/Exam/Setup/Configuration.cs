using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Setup
{
    public class Configuration
    {
        DA.Exam.Setup.ConfigurationDB db = null;
        int _UserId = 0;
        public Configuration(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db =new DA.Exam.Setup.ConfigurationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Setup.Configuration beData,int BranchId)
        {
            return db.SaveUpdate(beData,BranchId);
        }
        public BE.Exam.Setup.Configuration GetConfiguration(int EntityId)
        {
            return db.getConfiguration(_UserId, EntityId);
        }
        public ResponeValues SaveSeatPlanConfiguration(List<BE.Exam.Setup.SeatPlanConfiguraion> dataColl)
        {
            return db.SaveSeatPlanConfiguration(_UserId, dataColl);
        }
        public List<BE.Exam.Setup.SeatPlanConfiguraion> getSeatPlanConfiguration()
        {
            return db.getSeatPlanConfiguration(_UserId);
        }

        //Added by Suresh for getting the mindues classwise
        public BE.Exam.Setup.ClassWiseMinDues GetMinDuesClasswise( int ClassId)
        {
            return db.getClasswiseMinDues(_UserId, ClassId);
        }

    }
}
