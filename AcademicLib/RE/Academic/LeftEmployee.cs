using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class LeftEmployee
    {
        public LeftEmployee()
        {
            DocumentsColl = new List<Dynamic.BusinessEntity.GeneralDocument>();
        }
        public int EmployeeId { get; set; }
        public DateTime? DateOfJoining_AD { get; set; }
        public string DateOfJoining_BS { get; set; }

        public string LeftRemarks { get; set; }
        public DateTime? LeftDate_AD { get; set; }
        public string LeftDate_BS { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string ContactNo { get; set; } 
        //Added on 25 Poush 2082
        public string LeftType { get; set; }
        public List<Dynamic.BusinessEntity.GeneralDocument> DocumentsColl { get; set; }
    }
    public class LeftEmployeeCollections : System.Collections.Generic.List<LeftEmployee>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
