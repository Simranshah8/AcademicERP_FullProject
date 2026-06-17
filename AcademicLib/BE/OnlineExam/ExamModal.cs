using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.OnlineExam
{
    public class ExamModal : ResponeValues
    {
        public int CategoryId { get; set; }
        public EXAMMODALTYPES ExamModalType { get; set; }
        public int OrderNo { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int? NumberingMethod { get; set; }
    }
    public class ExamModalCollections : List<ExamModal> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum EXAMMODALTYPES
    {
        SUBJECTIVE=1,
        OBJECTIVE=2
    }

}
