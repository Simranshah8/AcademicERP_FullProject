using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class SalarySheet
    {
        DA.Payroll.SalarySheetDB db = null;
        int _UserId = 0;
        public SalarySheet(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.SalarySheetDB(hostName, dbName);
        }
        public ResponeValue SaveJV(int YearId, int MonthId)
        {
            return db.SaveJV(_UserId, YearId, MonthId);
        }
            public ResponeValues SaveUpdate(AcademicLib.BE.Payroll.SalarySheetCollections dataColl, AcademicLib.BE.Payroll.AttendanceTypeCollections AttendanceTypeColl)
        {
            ResponeValues resVal = new ResponeValues();

            foreach (var beData in dataColl)
            {
                if (beData.YearId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Year to save SalarySheet ";
                    return resVal;
                }

                if (beData.MonthId == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Month to save SalarySheet ";
                    return resVal;
                }              

            }

            resVal = db.UpdateSalarySheet(_UserId, dataColl, AttendanceTypeColl);

            return resVal;
        }

        public AcademicLib.BE.Payroll.SalarySheetDetail GetAllSalarySheet(int EntityId, int? BranchId, int? DepartmentId,int? CategoryId, int? YearId, int? MonthId)
        {
            return db.getAllSalarySheet(_UserId, EntityId, BranchId, DepartmentId, CategoryId,YearId,MonthId);
        }

        public ResponeValues DeleteSalarySheet(int EntityId, int BranchId, int DepartmentId, int CategoryId, int YearId, int MonthId)
        {
            return db.DeleteSalarySheet(_UserId, EntityId, BranchId, DepartmentId, CategoryId, YearId, MonthId);
        }

        public ResponeValues DeleteById(int EntityId, int EmployeeId, int YearId, int MonthId)
        {
            return db.DeleteById(_UserId, EntityId, EmployeeId, YearId, MonthId);
        }
        public AcademicLib.RE.Payroll.SalarySheetCollections getSalarySheet( int YearId, int MonthId, string CompanyIdColl, string BranchIdColl, string DepartmentIdColl, string CategoryIdColl,
            int EmployeeId, string EmployeeIdColl)
        {
            return db.getSalarySheet(_UserId, YearId, MonthId, CompanyIdColl, BranchIdColl, DepartmentIdColl, CategoryIdColl, EmployeeId, EmployeeIdColl);
        }
    }
}