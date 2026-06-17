using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class PostalCallLog : ResponeValues
    {
        public int? TranId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public MEETTOS MeeTo { get; set; }
        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public string OthersName { get; set; }
        public string Email { get; set; }
        public string Purpose { get; set; }
        public  DateTime? InOutTime { get; set; }
        
        public int CallDuration { get; set; }
        public DateTime ForDate { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public CALLTYPES CallType { get; set; }
        public string Remarks { get; set; }
         
        public string UserName { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogMiti { get; set; }

        public string BranchName { get; set; }

    }
    public class PostalCallLogCollections : List<PostalCallLog>
    {
        public PostalCallLogCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
     
    public enum CALLTYPES
    {  //INCOMEING=1,
        INCOMING = 1,
        OUTGOING = 2
      
       
    }
}

