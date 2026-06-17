using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ExtraEntity: ResponeValues
    {
        public ExtraEntity()
        {
            ExtraEntityAttributeColl = new List<ExtraEntityAttribute>();
        }
        public int? TranId { get; set; }
        public int? BranchId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int? For { get; set; }
        public List<ExtraEntityAttribute> ExtraEntityAttributeColl { get; set; }
    }
    public class ExtraEntityCollections : System.Collections.Generic.List<ExtraEntity>
    {
        public ExtraEntityCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ExtraEntityAttribute
    {
        public int? TranId { get; set; }
        public string Name { get; set; }
        public int? DataType { get; set; }
        public string DefaultValue { get; set; }
        public bool? IsMandatory { get; set; }
        public int? MinLen { get; set; }
        public int? MaxLen { get; set; }
        public string SelectOptions { get; set; }
        public int SNo { get; set; }
    }
}