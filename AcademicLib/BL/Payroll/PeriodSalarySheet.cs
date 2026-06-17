using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class PeriodSalarySheet
    {
        DA.Payroll.PeriodSalarySheetDB db = null;
        int _UserId = 0;
        public PeriodSalarySheet(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.PeriodSalarySheetDB(hostName, dbName);
        }
        
        public BE.Payroll.PeriodSalarySheetCollections GetAllPeriodSalarySheet(int FromYearId, int FromMonthId, int ToYearId, int ToMonthId, int? BranchId=null, int? DepartmentId=null, int? CategoryId=null)
        {
            return db.getAllPeriodSalarySheet(_UserId, FromYearId, FromMonthId, ToYearId, ToMonthId, BranchId, DepartmentId, CategoryId);
        }

       
    }
}