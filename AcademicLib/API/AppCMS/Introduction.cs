using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.AppCMS
{
    public class Introduction : ResponeValues
    {
        public Introduction()
        {
            VisionList = new List<BE.AppCMS.Creation.VisionStatement>();
            FounderMSGList = new List<BE.AppCMS.Creation.FounderMessage>();
            StaffList = new List<BE.AppCMS.Creation.StaffHierarchy>();
        }
        public List<AcademicLib.BE.AppCMS.Creation.VisionStatement> VisionList { get; set; }
        public List<AcademicLib.BE.AppCMS.Creation.FounderMessage> FounderMSGList { get; set; }
        public List<AcademicLib.BE.AppCMS.Creation.StaffHierarchy> StaffList { get; set; }

        public List<AcademicLib.BE.AppCMS.Creation.StaffHierarchy> CommitteList { get; set; } = new List<BE.AppCMS.Creation.StaffHierarchy>();

        public List<AcademicLib.BE.AppCMS.Creation.FounderMessage> TestimonialList { get; set; } = new List<BE.AppCMS.Creation.FounderMessage>();
    }
}

