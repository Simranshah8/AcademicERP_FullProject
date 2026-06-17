using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Exam.Creation
{
    public class EvaluationArea : ResponeValues
    {
        public int? EvaluationId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }
    }
    public class EvaluationAreaCollections : List<EvaluationArea>
    {
        public EvaluationAreaCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }


    }
}



