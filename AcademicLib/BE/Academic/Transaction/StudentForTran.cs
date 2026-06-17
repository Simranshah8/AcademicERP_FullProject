using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class StudentForTran
    {
        public StudentForTran()
        {
            RegNo = "";
            Name = "";
            SymbolNo = "";
            BoardRegNo = "";
            ClassName = "";
            SectionName = "";

        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string SymbolNo { get; set; }
        public string BoardRegNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public string Gender { get; set; }
        public bool IsLeft { get; set; }
        public string FatherName { get; set; } = "";
        public string Address { get; set; } = "";
    }
    public class StudentForTranCollections : System.Collections.Generic.List<StudentForTran>
    {
        public StudentForTranCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
