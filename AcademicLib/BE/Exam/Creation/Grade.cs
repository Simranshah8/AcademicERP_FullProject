using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{
   public class Grade : BE.Academic.Common
    {
        public  int? GradeId { get; set; }      
        public  double MinPer { get; set; }
        public double MaxPer { get; set; }
        public double GradePoint { get; set; }
        public int id
        {
            get
            {
                if (GradeId.HasValue)
                    return GradeId.Value;
                return 0;
            }
        }
        public int SubjectType { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }

    }
    public class GradeCollections : List<Grade> 
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
