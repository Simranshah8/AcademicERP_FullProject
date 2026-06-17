using Dynamic.BusinessEntity.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace AcademicLib.BE.Infirmary
{


    /*
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
    public class Select2DataJsonNetResult : System.Web.Mvc.JsonResult
    {
        public bool pagination { get; set; }
        public object results { get; set; }
        public Select2DataJsonNetResult()
        {
            pagination = false;
            results = "";
        }
        public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (results != null)
            {

                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };

                JsonSerializerSettings setting = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateFormatString = "yyyy-MM-dd HH:mm"
                };
                JsonSerializer serializer = JsonSerializer.Create(setting);

                var resData = new
                {
                    results = this.results,
                    pagination = this.pagination
                };
                serializer.Serialize(writer, resData);
                writer.Flush();
            }
        }

    } */

    // ===================================================================================================================// 

    // =================================== Health Checkup (Exam) starts ===================================================// 

    // ===================================================================================================================// 

    public class Entity : ResponeValues
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EntityCollections : System.Collections.Generic.List<Entity>
    {
        public EntityCollections() { ResponseMSG = ""; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ExamTermClass : ResponeValues
    {
        public int? ClassId { get; set; }
        public string Name { get; set; }
    }
    public class ExamTermClassCollections : System.Collections.Generic.List<ExamTermClass>
    {
        public ExamTermClassCollections()  {   ResponseMSG = "";  }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ExamTermTest : ResponeValues
    {
        public int? TestId { get; set; }
        public string Name { get; set; }
    }
    public class ExamTermTestCollections : System.Collections.Generic.List<ExamTermTest>
    {
        public ExamTermTestCollections() { ResponseMSG = ""; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


    public class ExamTermDefaultInput : ResponeValues
    {
        public int? TestId { get; set; } = -1;
        public string DefaultValue { get; set; }
        public string TestName { get; set; }
        public string DefaultRemarks { get; set; }

        public ExamTermDefaultInput()
        {

        }
    }
    public class ExamTermDefaultInputCollections : System.Collections.Generic.List<ExamTermDefaultInput>
    {
        public ExamTermDefaultInputCollections() { ResponseMSG = ""; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


    public class AddHealthExamParam : ResponeValues
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int? TestId { get; set; }
        public string TestName { get; set; }
        public int? ExamTypeId { get; set; }
        public string ExamTypeName { get; set; }
        public string DefaultRemarks { get; set; }
        public string DefaultValue { get; set; }
    }
    public class AddHealthExamParamCollections : System.Collections.Generic.List<AddHealthExamParam>
    {
        public AddHealthExamParamCollections() { ResponseMSG = ""; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }



    public class TwoPara : ResponeValues
    {
        public string ClassSection { get; set; }
        public string TestName { get; set; }
    }
    public class TwoParaCollections : System.Collections.Generic.List<TwoPara>
    {
        public TwoParaCollections() { ResponseMSG = ""; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }



} // namespace ends