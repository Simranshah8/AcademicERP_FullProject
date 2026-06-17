using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Batch : Common
    {
        public int? BatchId { get; set; }
        public int id
        {
            get
            {
                if (BatchId.HasValue)
                    return BatchId.Value;
                return 0;
            }
        }

    }

    public class BatchCollections : System.Collections.Generic.List<Batch>
    {
        public BatchCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


}
