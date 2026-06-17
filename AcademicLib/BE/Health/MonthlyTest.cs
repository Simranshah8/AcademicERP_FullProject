using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.BE.Health.Transaction
{
    public class MonthlyTest: ResponeValues
    {
        public MonthlyTest()
        {
            AttachMonTesColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public int? TranId { get; set; }
        public int? Month { get; set; }
        public string Teeth { get; set; }
        public string Nail { get; set; }
        public string Cleanliness { get; set; }
        public  int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
       
        public DateTime? ForDate { get; set; }
        public string ForMiti { get; set; }
        public string MonthName { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachMonTesColl { get; set; }
    }
    public class MonthlyTestCollections : System.Collections.Generic.List<MonthlyTest>
    {
        public MonthlyTestCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}