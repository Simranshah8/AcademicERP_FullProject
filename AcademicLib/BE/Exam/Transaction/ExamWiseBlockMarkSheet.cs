using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ExamWiseBlockMarkSheet : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }

        public int StudentId { get; set; }

        public int ExamTypeId { get; set; }

        public string Message { get; set; }

        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }

    }
    public class ExamWiseBlockMarkSheetCollections : System.Collections.Generic.List<ExamWiseBlockMarkSheet> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
