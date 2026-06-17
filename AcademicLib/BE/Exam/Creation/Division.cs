using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{
   public class Division: BE.Academic.Common
    {
        public int? DivisionId { get; set; }     
        public double MinPer { get; set; }
        public double MaxPer { get; set; }
        public int id
        {
            get
            {
                if (DivisionId.HasValue)
                    return DivisionId.Value;
                return 0;
            }
        }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
    }
    public class DivisionCollections : System.Collections.Generic.List<Division> 
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
