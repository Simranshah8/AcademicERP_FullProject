using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Academic.Setup
{
    public class HTMLTemplateConfig : ResponeValues
    {
        public int? TemplateId { get; set; }
        public int? EntityId { get; set; }
        public int? SNo { get; set; }
        public bool IsAllowed { get; set; }
        public bool ForApp { get; set; }
        public string TemplateName { get; set; } = "";
        public byte[] PreviewD { get; set; }
        public string PreviewPath { get; set; } = "";
    }
    public class HTMLTemplateConfigCollection : List<HTMLTemplateConfig>
    {
        public HTMLTemplateConfigCollection()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}