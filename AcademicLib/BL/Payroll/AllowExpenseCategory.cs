using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class AllowExpenseCategory
    {
        DA.Payroll.AllowExpenseCategoryDB db = null;
        int _UserId = 0;
        public AllowExpenseCategory(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.AllowExpenseCategoryDB(hostName, dbName);
        }

        public ResponeValues SaveUpdate(AcademicLib.BE.Payroll.AllowExpenseCategoryCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.UpdateAllowExpenseCategory(_UserId, dataColl);

            return resVal;
        }

        public AcademicLib.BE.Payroll.EmployeeForAllowExpenseCategoryCollections GetAllAllowExpenseCategory(int EntityId, int? BranchId, int? DepartmentId, int? CategoryId)
        {
            return db.getAllAllowExpenseCategory(_UserId, EntityId, BranchId, DepartmentId, CategoryId);
        }


    }
}