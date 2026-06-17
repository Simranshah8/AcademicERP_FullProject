using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class Download : ResponeValues
    {
        public Download()
        {
            downdloadColl = new DownloadCollections();
        }
        public int? TranId { get; set; }
        public string Title { get; set; } = "";
        public int? OrderNo { get; set; }
        public bool? IsActive { get; set; }
        public string AttachmentPath { get; set; } = "";
        public DownloadCollections downdloadColl { get; set; }
    }
    public class DownloadCollections : List<Download>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}