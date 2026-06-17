using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class EmployeeLeft : ResponeValues
    {
        public EmployeeLeft()
        {
            AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();

        }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PersonalContactNo { get; set; }
        public DateTime? JoinDate_AD { get; set; }
        public string JoinDate_BS { get; set; } 
        public DateTime? LeftDate_AD { get; set; }
        public string LeftDate_BS { get; set; }
        public string Remarks { get; set; }
        public int? LeftTypeId { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
    }

    public class EmployeeLeftCollections : System.Collections.Generic.List<EmployeeLeft>
    {

    }
}
