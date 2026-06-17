using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{
  public  class CASType :ResponeValues
  {
        public int? CASTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }

        public int id
        {
            get
            {
                if (CASTypeId.HasValue)
                    return CASTypeId.Value;
                return 0;
            }
        }

        public double FullMark { get; set; }
        public int Under { get; set; }
        public int Scheme { get; set; }
    }
    public class CASTypeCollections : System.Collections.Generic.List<CASType> 
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
 