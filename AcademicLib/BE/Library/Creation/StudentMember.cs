using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class StudentMember
    {
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public bool IsLibMember { get; set; }
        public string MembershipNo { get; set; }


        public DateTime? IssueDateAD { get; set; }
        public DateTime? ExpiredDateAD { get; set; }
        public string IssueDateBS { get; set; }
        public string ExpiredDateBS { get; set; }

        public string ClassSec
        {
            get
            {
                string val = "";

                if (!string.IsNullOrEmpty(ClassName))
                    val = ClassName;

                if (!string.IsNullOrEmpty(SectionName))
                    val = val + " " + SectionName;

                return val;
            }
        }
    }
    public class StudentMemberCollections : System.Collections.Generic.List<StudentMember>
    {
        public StudentMemberCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
