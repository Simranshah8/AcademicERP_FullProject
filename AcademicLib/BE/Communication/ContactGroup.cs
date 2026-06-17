using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Communication.Creation
{
    public class ContactGroup : ResponeValues
    {
        public int? GroupId { get; set; }
        public string Name { get; set; } = "";
        public int OrderNo { get; set; }
        public string Description { get; set; } = "";
    }
    public class ContactGroupCollection : List<ContactGroup>
    {
        public ContactGroupCollection()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }


    }


}