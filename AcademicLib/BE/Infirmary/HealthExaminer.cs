using Dynamic.BusinessEntity.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace AcademicLib.BE.Infirmary
{
    public class HealthExaminerDocument : ResponeValues
    {
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentTypeText { get; set; }
        public string DocumentPath { get; set; }
        public int DocumentSize { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
    }
    public class HealthExaminerDocumentCollections : System.Collections.Generic.List<HealthExaminerDocument>
    {
        public HealthExaminerDocumentCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class HealthExaminer : ResponeValues
    {
        public int ExaminerId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string RegNo { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public string Specialization { get; set; }
        public int Username { get; set; }
        public string UsernameText { get; set; }
        public string Remarks { get; set; }
        public HealthExaminerDocumentCollections HealthExaminerDocuments { get; set; }
        public string PhotoPath { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? LogDateTime { get; set; }
        public DateTime? UpdateLogDateTime { get; set; }
    }
    public class HealthExaminerCollections : System.Collections.Generic.List<HealthExaminer>
    {
        public HealthExaminerCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int TotalRows { get; set; }
    }

    public class HealthExaminerSingle : HealthExaminer
    {
        public HealthExaminerSingle()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class Select2Data
    {
        public int id { get; set; }
        public string text { get; set; }
    }
    public class Select2DataCollections : System.Collections.Generic.List<Select2Data>
    {
        public Select2DataCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
  
}