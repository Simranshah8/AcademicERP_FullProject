using Dynamic.BusinessEntity.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace AcademicLib.BE.Infirmary
{
    public class HealthCheckupGroup : ResponeValues
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public int StudentInfirmaryOrderNo { get; set; }
        public int EmployeeInfirmaryOrderNo { get; set; }
        public string Mode { get; set; }
    }
    public class HealthCheckupGroupCollections : System.Collections.Generic.List<HealthCheckupGroup>
    {
        public HealthCheckupGroupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int TotalRows { get; set; }
    }
    public class HealthCheckupTest : ResponeValues
    {
        public int TestNameId { get; set; }
        public string TestName { get; set; }
        public int CheckupGroupId { get; set; }
        public string CheckupGroupText { get; set; }
        public int OrderNo { get; set; }
        public string InputTextType { get; set; }
        public string SampleCollection { get; set; }
        public string SampleUnitOrVolume { get; set; }
        public string Description { get; set; }
        public HealthCheckupLabParametersCollections LabParameters { get; set; }
        public string Mode { get; set; }
    }
    public class HealthCheckupTestCollections : System.Collections.Generic.List<HealthCheckupTest>
    {
        public HealthCheckupTestCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int TotalRows { get; set; }
    }
    public class HealthCheckupLabParameters : ResponeValues
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string DefaultValue { get; set; }
        public string NormalRange { get; set; }
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }
        public string GroupName { get; set; }
        public string Remarks { get; set; }
        public int TestId { get; set; }
        public string Mode { get; set; }
    }
    public class HealthCheckupLabParametersCollections : System.Collections.Generic.List<HealthCheckupLabParameters>
    {
        public HealthCheckupLabParametersCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int TotalRows { get; set; }
    }
}