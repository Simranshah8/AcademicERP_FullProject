using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class ReportTempletesAttachments

    {

        public ReportTempletesAttachments()

        {

            EntityName = "";

            ReportName = "";

            IsDefault = false;

        }

        public int SNo { get; set; }

        public int RptTranId { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public string ReportName { get; set; }

        public string Path { get; set; }

        public bool IsDefault { get; set; }

        public string MainFileName { get; set; }

        public byte[] MainFile { get; set; }

        public string SubFileName1 { get; set; }

        public byte[] SubFile1 { get; set; }

        public string SubFileName2 { get; set; }

        public byte[] SubFile2 { get; set; }

        public string SubFileName3 { get; set; }

        public byte[] SubFile3 { get; set; }

        public string SubFileName4 { get; set; }

        public byte[] SubFile4 { get; set; }

        public string SubFileName5 { get; set; }

        public byte[] SubFile5 { get; set; }

        public bool IsRDLC { get; set; }

        public bool ForEmail { get; set; }

        public int? Rpt_Type { get; set; }

        public bool IsActive { get; set; }

        public string VoucherName { get; set; }

        public string VoucherType { get; set; }

        public string PDFFileName { get; set; } = "download.pdf";

    }

    public class ReportTempletesAttachmentsCollections : System.Collections.Generic.List<ReportTempletesAttachments>
    {

        public string ResponseMSG { get; set; }

        public bool IsSuccess { get; set; }

    }

}