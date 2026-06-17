using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class TransportHostelAnalysis
    {
        public TransportHostelAnalysis()
        {
            HostelColl = new List<TransportHostelDetails>();
            TransportColl = new List<TransportHostelDetails>();
        }

        public List<TransportHostelDetails> HostelColl { get; set; }
        public List<TransportHostelDetails> TransportColl { get; set; }

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class TransportHostelDetails
    {
        public int SNo { get; set; }
        public string ClassName { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
    }


}
