using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class ExpenseRateSetup
    {
        DA.Payroll.ExpenseRateSetupDB db = null;
        int _UserId = 0;
        public ExpenseRateSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.ExpenseRateSetupDB(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Payroll.ExpenseRateSetupCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.UpdateExpenseRateSetup(_UserId, dataColl);

            return resVal;
        }
        public AcademicLib.BE.Payroll.EmployeeForExpenseRateSetupCollections GetAllExpenseRateSetup(int EntityId, int? BranchId, int? DepartmentId, int? CategoryId)
        {
            return db.getAllExpenseRateSetup(_UserId, EntityId, BranchId, DepartmentId, CategoryId);
        }
    }
}