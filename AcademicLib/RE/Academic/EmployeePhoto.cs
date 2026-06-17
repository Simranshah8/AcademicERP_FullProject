using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Academic
{
    public class EmployeePhoto
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int AutoNumber { get; set; }

        public string PhotoPath { get; set; }
        public string EmployeeCode { get; set; }

        public int? TranId { get; set; }
        public DateTime? DownloadDate { get; set; }
        public int DownloadById { get; set; }

        public string DownloadMiti { get; set; }
        public string DepartmentName { get; set; }
        public string DownloaderName { get; set; }



    }
    public class EmployeePhotoCollections : System.Collections.Generic.List<EmployeePhoto>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}