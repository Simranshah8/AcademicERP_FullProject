using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class GatePassEmployee : ResponeValues
    {
        public int? GatePassEmployeeId { get;set;}
        public string Name { get;set;}
        public int DesignationId { get;set;}
        public int PurposeId { get;set;}
        public DateTime OutTime { get;set;}
        public DateTime ValidationTime { get;set;}
        public DateTime InTime { get;set;}
        public string Remarks { get;set;}
        public string AttachDoc { get;set;}
        public string AttachDocPath { get;set;}
    }
    public class GatePassEmployeeCollections : List<GatePassEmployee> {
        public GatePassEmployeeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
                   