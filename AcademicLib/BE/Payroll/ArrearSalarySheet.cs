using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Payroll
{
    public class EmployeeForArrearSalarySheet : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public int? PayHeadingId { get; set; }
        public string EmployeeCode { get; set; }
        public int EnrollNo { get; set; }
        public int? SNo { get; set; }
        public string EmployeeName { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string PayHeading { get; set; }
        public double? Amount { get; set; }
        public int? YearId { get; set; }
        public int? MonthId { get; set; }

    }
    public class EmployeeForArrearSalarySheetCollections : System.Collections.Generic.List<EmployeeForArrearSalarySheet>
    {
        public EmployeeForArrearSalarySheetCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ArrearSalarySheet
    {
        public int EmployeeId { get; set; }
        public int PayHeadingId { get; set; }
        public double? Amount { get; set; }
        public int? YearId { get; set; }
        public int? MonthId { get; set; }
    }

    public class ArrearSalarySheetCollections : System.Collections.Generic.List<ArrearSalarySheet>
    {
        public ArrearSalarySheetCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}