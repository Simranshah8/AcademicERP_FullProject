using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicERP.Areas.Support.Model
{
    public class SupportExecutive : ResponeValues
    {
        public int PrimaryExecutiveId { get; set; }
        public int SecondaryExecutiveId { get; set; }
        public string PCode { get; set; }
        public string PName { get; set; }
        public string PDesination { get; set; }
        public string PEmail { get; set; }
        public string PContactNo { get; set; }
        public string PPhotoPath { get; set; }
        public string SCode { get; set; }
        public string SName { get; set; }
        public string SDesination { get; set; }
        public string SEmail { get; set; }
        public string SContactNo { get; set; }
        public string SPhotoPath { get; set; }
        public string CCode { get; set; }
        public string CName { get; set; }
        public string CDesination { get; set; }
        public string CEmail { get; set; }
        public string CContactNo { get; set; }
        public string CPhotoPath { get; set; }
    }
}