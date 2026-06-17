using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class SubjectWiseEvaluation
    {
        public int SNo { get; set; }
        public string SubjectName { get; set; }
        public int NoOfStudent { get; set; }
        public int NoOfPass { get; set; }
        public int NoOfFail { get; set; }
        public double PassPer { get; set; }
        public double FailPer { get; set; }

    }
    public class SubjectWiseEvaluationCollections : System.Collections.Generic.List<SubjectWiseEvaluation> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
