using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
using PivotalERP;

namespace AcademicERP.Areas.Support.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        [HttpGet]
        public JsonNetResult GetSplash()
        {
            Support.Model.Splash resVal = new Support.Model.Splash();
            try
            {

                AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(CrmURL + "Common/GetSplash", "POST");
                var para = new
                {
                    CustomerCode = "",
                    UrlName = GetURL,
                    DBCode = DBCode,
                    AppUserName = User.UserName
                };
                var response = request.Execute<Support.Model.Splash>(para);
                if (response != null)
                {
                    resVal = (Support.Model.Splash)response;
                }
            }
            catch (System.Net.WebException wex)
            {

                resVal.IsSuccess = false;

                var pageContent = new System.IO.StreamReader(wex.Response.GetResponseStream())
                                      .ReadToEnd();

                resVal.ResponseMSG = pageContent;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetDBCode()
        {
            return new JsonNetResult() { Data = DBCode, TotalCount = 0, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        public ActionResult Expired()
        {
            return View();
        }
         
        public ActionResult SupportDashboard()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetDashboard()
        {
            Support.Model.CustomerDashboard resVal = new Support.Model.CustomerDashboard();
            try
            {
                string url = CrmURL + "Common/GetCustomerDashboard";
                AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(url, "POST");
                var para = new
                {
                    CustomerCode = "",
                    UrlName = GetURL,
                    DBCode = DBCode,
                    AppUserName = User.UserName
                };
                System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                var response = request.ExecuteWithHeader<Support.Model.CustomerDashboard>(para, headers);
                if (response != null)
                {
                    resVal = (Support.Model.CustomerDashboard)response;

                    if (!string.IsNullOrEmpty(resVal.Executive.PPhotoPath))
                        resVal.Executive.PPhotoPath = CrmMainURL + resVal.Executive.PPhotoPath;

                    if (!string.IsNullOrEmpty(resVal.Executive.SPhotoPath))
                        resVal.Executive.SPhotoPath = CrmMainURL + resVal.Executive.SPhotoPath;

                    if (!string.IsNullOrEmpty(resVal.Executive.CPhotoPath))
                        resVal.Executive.CPhotoPath = CrmMainURL + resVal.Executive.CPhotoPath;
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult SupportNotice()
        {
            return View();
        }
        public ActionResult SupportExecutive()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetSuppExe()
        {
            Support.Model.SupportExecutive resVal = new Support.Model.SupportExecutive();
            try
            {     
                string url = CrmURL + "Common/GetSupportExecutive";
                AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(url, "POST");
                var para = new
                {
                    CustomerCode = "",
                    UrlName = GetURL
                };
                System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                var response = request.ExecuteWithHeader<Support.Model.SupportExecutive>(para, headers);
                if (response != null)
                {
                    resVal = (Support.Model.SupportExecutive)response;

                    if (resVal.IsSuccess)
                    {
                        if (!string.IsNullOrEmpty(resVal.PPhotoPath))
                            resVal.PPhotoPath = CrmMainURL + resVal.PPhotoPath;

                        if (!string.IsNullOrEmpty(resVal.SPhotoPath))
                            resVal.SPhotoPath = CrmMainURL + resVal.SPhotoPath;

                        if (!string.IsNullOrEmpty(resVal.CPhotoPath))
                            resVal.CPhotoPath = CrmMainURL + resVal.CPhotoPath;
                    }
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult CreateTicket()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetTicketLst()
        {
            Support.Model.GenerateTicketCollections resVal = new Support.Model.GenerateTicketCollections();
            try
            {
                string url = CrmURL + "Common/GetTicketList";
                AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(url, "POST");
                var para = new
                {
                    CustomerCode = "",
                    UrlName = GetURL
                };
                System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                var response = request.ExecuteWithHeader<Support.Model.GenerateTicketCollections>(para, headers);
                if (response != null)
                {
                    resVal = (Support.Model.GenerateTicketCollections)response;
                     
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GenFonepayQR()
        {
            Dynamic.BusinessEntity.NICBank.FonepayQRResponse resVal = new Dynamic.BusinessEntity.NICBank.FonepayQRResponse();
            try
            {
                //customerCode = "10";
                //urlName = "";

                string full_url = CrmURL + "/Common/GetFonePayQry";
                AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(full_url, "POST");
                var para = new
                {
                    CustomerCode = CompanyCode,
                    UrlName = GetURL,
                    DBCode = DBCode,
                    PassKey = "qr@2021$#Dynamic",
                    AppUserName = User.UserName
                };
                System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                var response = request.ExecuteWithHeader<Dynamic.BusinessEntity.NICBank.FonepayQRResponse>(para, headers);
                if (response != null)
                {
                    resVal = (Dynamic.BusinessEntity.NICBank.FonepayQRResponse)response;
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetKYC()
        {
            Model.Kyc resVal = new Model.Kyc();
            try
            {
                resVal = getKYC();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        private Model.Kyc getKYC()
        {
            Model.Kyc resVal = new Model.Kyc();
            try
            {
                //customerCode = "10";
                //urlName = "";
                AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest("https://crm.dynamicerp.online/v1/Common/GetKycDet", "POST");
                var para = new
                {
                    CustomerCode = CompanyCode,
                    UrlName = GetURL,
                    DBCode = DBCode
                };
                System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                var response = request.ExecuteWithHeader<Model.Kyc>(para, headers);
                if (response != null)
                {
                    resVal = (Model.Kyc)response;

                    if (resVal != null)
                    {
                        resVal.attachFile = "https://crm.dynamicerp.online/";
                    }
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return resVal;
        }

        [HttpPost]
        public JsonNetResult SaveGenerateTicket()
        {            
            ResponeValues resVal = new ResponeValues();

            try
            {
                string usrName = User.UserName.Trim().ToLower();
                var kyc = new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName).getKYC("", GetURL);
                if (!kyc.IsSuccess)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Please ! 1st udate kyc.";
                }
                else if (kyc.ContactDetColl == null || kyc.ContactDetColl.Count==0)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Please ! 1st udate kyc.";
                }
                else
                {
                    var findContact = kyc.ContactDetColl.Find(p1 => p1.UserName.Trim().ToLower() == usrName);
                    if (findContact == null)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Please ! 1st udate kyc.";
                    }
                    else
                    {
                        var beData = DeserializeObject<Support.Model.GenerateTicket>(Request["jsonData"]);
                        if (beData != null)
                        {
                            if (Request.Files.Count > 0)
                            {
                                var filesColl = Request.Files;
                            }
                            beData.CompanyCode = CompanyCode;
                            beData.UrlName = GetURL;
                            beData.TicketId = 0;
                            beData.SourceId = 5;
                            beData.Source = "Web";
                            beData.UserName = usrName;                           
                            beData.ContactMobileNo = findContact.MobileNo;
                            beData.EmailId = findContact.EmailId;
                            beData.ContactDesignation = findContact.Designation;
                            beData.ContactName = findContact.Name;
                            beData.CustomerId = 0;
                            beData.AssignToId = null;
                            try
                            {
                                var jsonbeData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(beData);

                                string url = CrmURL + "Common/GenerateTicket";
                                var method = new System.Net.Http.HttpMethod("POST");
                                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                                client.DefaultRequestHeaders.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                                System.Net.Http.MultipartFormDataContent form = new System.Net.Http.MultipartFormDataContent();

                                System.Net.Http.HttpContent content1 = new System.Net.Http.StringContent(jsonbeData);
                                form.Add(content1, "paraDataColl");
                                if (Request.Files.Count > 0)
                                {
                                    var filesColl = Request.Files;
                                    var logo = filesColl[0];

                                    if (logo != null)
                                    {
                                        content1 = new System.Net.Http.StreamContent(logo.InputStream);
                                        content1.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                        {
                                            FileName = logo.FileName,
                                            Name = "LogoImg"
                                        };
                                        form.Add(content1, "LogoImg");
                                    }

                                }
                                var response = (client.PostAsync(url, form)).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    var resStr = response.Content.ReadAsStringAsync().Result;
                                    if (!string.IsNullOrEmpty(resStr))
                                    {
                                        resVal = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponeValues>(resStr);
                                    }
                                }
                                else
                                {
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = "Unable To Generate Ticket ";
                                }
                            }
                            catch (Exception ex1)
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = ex1.Message;
                            }
                            
                        }
                        else
                        {
                            resVal.ResponseMSG = "Blank Data Can't be Accepted";
                        }
                    }
                }

                
                
           
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveTicketComment()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var para = DeserializeObject<Support.Model.TicketComment>(Request["jsonData"]);
                if (para != null)
                {

                    string url = CrmURL + "Common/AddTicketComment";
                    AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(url, "POST");                   
                    System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                    var response = request.ExecuteWithHeader<ResponeValues>(para, headers);
                    if (response != null)
                    {
                        resVal = (ResponeValues)response; 
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveTicketApproved()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var para = DeserializeObject<Support.Model.TicketApproved>(Request["jsonData"]);
                if (para != null)
                {
                    para.ApprovedBy = User.UserName;
                    para.CompanyCode = CompanyCode;
                    para.UrlName = GetURL;

                    string url = CrmURL + "Common/AddTicketApproved";
                    AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(url, "POST");
                    System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                    var response = request.ExecuteWithHeader<ResponeValues>(para, headers);
                    if (response != null)
                    {
                        resVal = (ResponeValues)response;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        public ActionResult Feedback()
        {
            return View();
        }
        public ActionResult AccountStatement()
        {
            return View();
        }
        public ActionResult KycForm()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult UpdateKyc()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Support.Model.Kyc>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.KYCUpdateBy = User.UserName;
                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();

                    try
                    {
                        var jsonbeData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(beData);

                        string url = CrmURL + "Common/UpdateKyc";
                        var method = new System.Net.Http.HttpMethod("POST");
                        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                        //client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                        System.Net.Http.MultipartFormDataContent form = new System.Net.Http.MultipartFormDataContent();

                        System.Net.Http.HttpContent content1 = new System.Net.Http.StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");
                        if (Request.Files.Count > 0)
                        {
                            var filesColl = Request.Files;
                            var logo = filesColl["LogoImg"];
                            var reg = filesColl["RegImg"];
                            var pan = filesColl["PanImg"];
                            var tax = filesColl["TaxImg"];

                            if (logo != null)
                            {
                                content1 = new System.Net.Http.StreamContent(logo.InputStream);
                                content1.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = logo.FileName,
                                    Name = "LogoImg"
                                }; //  //form-data
                                form.Add(content1, "LogoImg");
                            }

                            if (reg != null)
                            {
                                content1 = new System.Net.Http.StreamContent(reg.InputStream);
                                content1.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = reg.FileName,
                                    Name = "RegImg"
                                }; //  //form-data
                                form.Add(content1, "RegImg");
                            }

                            if (pan != null)
                            {
                                content1 = new System.Net.Http.StreamContent(pan.InputStream);
                                content1.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = pan.FileName,
                                    Name = "PanImg"
                                }; //  //form-data
                                form.Add(content1, "PanImg");
                            }

                            if (tax != null)
                            {
                                content1 = new System.Net.Http.StreamContent(tax.InputStream);
                                content1.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = tax.FileName,
                                    Name = "TaxImg"
                                }; //  //form-data
                                form.Add(content1, "TaxImg");
                            }
                        }
                        var response = (client.PostAsync(url, form)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var resStr = response.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(resStr))
                            {
                                resVal = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponeValues>(resStr);
                            }
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Unable To Update KYC ";
                        }
                    }
                    catch (Exception ex1)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = ex1.Message;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveReceipt(double Amount, string Response, string TransactionNo, string DeviceId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var usr = User;
                var para = new
                {
                    PassKey = "qrPayment@2021$#Dynamic",
                    CustomerId = CrmCustomerId,
                    ReceiptBy = usr.UserName + " " + GetURL,
                    Amount = Amount,
                    Narration = Response,
                    ReceiptNo = TransactionNo,
                    ReceiptAs = "Fonepay",
                    TransactionNo = DeviceId,
                    CustomerCode = "",
                    UrlName = GetURL,
                    DBCode = DBCode
                };

                try
                {
                    string full_url = CrmURL + "/Common/SaveReceipt";
                    AcademicERP.Models.APIRequest request = new AcademicERP.Models.APIRequest(full_url, "POST");
                    System.Collections.Generic.Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add(CRM_HEADER_KEY, CRM_HEADER_VALUE);
                    var response = request.ExecuteWithHeader<ResponeValues>(para, headers);
                    if (response != null)
                    {
                        resVal = (ResponeValues)response;
                        if (resVal.IsSuccess)
                        {
                            var closingRes = new Dynamic.DataAccess.Global.GlobalDB(usr.HostName, usr.DBName).CostClassClosing(usr.UserId);
                            resVal.IsSuccess = closingRes.IsSuccess;
                            resVal.ResponseMSG = closingRes.ResponseMSG;
                        }
                    }
                }
                catch (Exception ex1)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = ex1.Message;
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
    }
}