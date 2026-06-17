using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class EmployeeMember
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }

        public bool IsLibMember { get; set; }
        public string MembershipNo { get; set; }
        public DateTime? IssueDateAD { get; set; }
        public DateTime? ExpiredDateAD { get; set; }
        public string IssueDateBS { get; set; }
        public string ExpiredDateBS { get; set; }
    }
    public class EmployeeMemberCollections : System.Collections.Generic.List<EmployeeMember>
    {
        public EmployeeMemberCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
