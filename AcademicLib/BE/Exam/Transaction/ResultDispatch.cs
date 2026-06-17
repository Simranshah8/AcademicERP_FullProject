using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ResultDispatch : ResponeValues
    {
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public bool IsDispatch { get; set; }
        public DateTime? DispatchDate { get; set; }
        public string Remarks { get; set; }
        public int AcademicYearId { get; set; }
    }
    public class ResultDispatchCollections : List<ResultDispatch>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
