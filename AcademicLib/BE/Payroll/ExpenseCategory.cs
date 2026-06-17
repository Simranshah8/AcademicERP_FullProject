using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Payroll
{
    public class ExpenseCategory : ResponeValues
    {
        public int? TranId { get; set; }
        public string Name { get; set; }
        public int? SNO { get; set; }
        public int? GroupNameId { get; set; }
        public bool? CanEdit { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
    }
    public class ExpenseCategoryCollections : System.Collections.Generic.List<ExpenseCategory>
    {
        public ExpenseCategoryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}