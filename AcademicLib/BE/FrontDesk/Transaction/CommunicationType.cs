using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class CommunicationType : ResponeValues
    {
        public int? CommunicationTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public int Status { get; set; }
    }
    public class CommunicationTypeCollections : List<CommunicationType>
    {
        public CommunicationTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
