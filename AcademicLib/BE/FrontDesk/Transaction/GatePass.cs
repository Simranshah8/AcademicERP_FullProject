using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class GatePass : ResponeValues
    {
        public int? TranId { get; set; }
        public int GatePassNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public MEETTOS MeeTo { get; set; }
        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public string OthersName { get; set; }
        public string Email { get; set; }
        public string Purpose { get; set; }
        public  DateTime? InTime { get; set; }
        public   DateTime? ValidityTime { get; set; }
        public  DateTime? OutTime { get; set; }       
        public string Remarks { get; set; }
        public string UserName { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogMiti { get; set; }
        public string BranchName { get; set; }
        public bool WillReturnBack { get; set; }
    }
    public class GatePassCollections : List<GatePass>
    {
        public GatePassCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
      
}

