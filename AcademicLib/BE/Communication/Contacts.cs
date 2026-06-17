using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Communication.Creation
{
    public class ContactDetails : ResponeValues
    {

        public int? ContactId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; } = "";
        public int GroupId { get; set; }
        public string ContactNo { get; set; } = "";
        public int OrderNo { get; set; }
        public string Description { get; set; } = "";
        public string ContactGroup { get; set; } = "";


    }
    public class ContactDetailsCollection : List<ContactDetails>
    {
        public ContactDetailsCollection()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }


    }
}