using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class AllowPayHeading
    {
        DA.Payroll.AllowPayHeadingDB db = null;
        int _UserId = 0;
        public AllowPayHeading(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.AllowPayHeadingDB(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Payroll.AllowPayHeadingCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.UpdateAllowPayHeading(_UserId, dataColl);

            return resVal;
        }

        public AcademicLib.BE.Payroll.EmployeeForAllowPayHeadingCollections GetAllAllowPayHeading(int EntityId, int? BranchId, int? DepartmentId, int? CategoryId)
        {
            return db.getAllAllowPayHeading(_UserId, EntityId, BranchId,DepartmentId,CategoryId);
        }

		
	}
}