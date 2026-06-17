using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using AcademicLib.BE.Infirmary;
using AcademicLib.DA.Infirmary;

namespace PivotalERP.Areas.Infirmary.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        public ActionResult HealthExaminer()
        {
            return View();
        }
        public ActionResult HealthCampaign()
        {
            return View();
        }
        public ActionResult HealthCheckup()
        {
            return View();
        }


        public ActionResult Disease()
        {
            return View();
        }


        public ActionResult StudentInfirmary()
        {
            return View();
        }
        public ActionResult EmployeeInfirmary()
        {
            return View();
        }
        public ActionResult ClasswiseInfirmary()
        {
            return View();
        }
        public ActionResult HealthCampaignInfirmary()
        {
            return View();
        }
        public ActionResult GeneralCheckupInfirmary()
        {
            return View();
        }
        public ActionResult HealthExam()
        {
            return View();
        }


        #region "HealthIssue"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.HealthIssue)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.HealthIssue)]
        public JsonNetResult SaveUpdateHealthIssue()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.HealthIssue>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HealthIssueId.HasValue)
                        beData.HealthIssueId = 0;

                    resVal = new AcademicERP.BL.HealthIssue(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.HealthIssue)]
        public JsonNetResult getHealthIssueById(int HealthIssueId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.HealthIssue(User.UserId, User.HostName, User.DBName).GetHealthIssueById(0, HealthIssueId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.HealthIssue)]
        public JsonNetResult DeleteHealthIssue(int HealthIssueId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (HealthIssueId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.HealthIssue(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, HealthIssueId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllHealthIssue()
        {
            AcademicERP.BE.HealthIssueCollections dataColl = new AcademicERP.BE.HealthIssueCollections();
            try
            {
                dataColl = new AcademicERP.BL.HealthIssue(User.UserId, User.HostName, User.DBName).GetAllHealthIssue(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        #region "Vaccine"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Vaccine)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Vaccine)]
        public JsonNetResult SaveUpdateVaccine()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Vaccine>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.VaccineId.HasValue)
                        beData.VaccineId = 0;

                    resVal = new AcademicERP.BL.Vaccine(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.Vaccine)]
        public JsonNetResult getVaccineById(int VaccineId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Vaccine(User.UserId, User.HostName, User.DBName).GetVaccineById(0, VaccineId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Vaccine)]
        public JsonNetResult DeleteVaccine(int VaccineId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (VaccineId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Vaccine(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, VaccineId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllVaccine()
        {
            AcademicERP.BE.VaccineCollections dataColl = new AcademicERP.BE.VaccineCollections();
            try
            {
                dataColl = new AcademicERP.BL.Vaccine(User.UserId, User.HostName, User.DBName).GetAllVaccine(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "Examiner"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Examiner)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateExaminer()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Examiner>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.AttachmentColl;
                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var UserPhoto = filesColl["UserPhoto"];

                        if (UserPhoto != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                            beData.Photo = photoDoc.Data;
                            beData.PhotoPath = photoDoc.DocPath;
                        }

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);

                                if (att != null)
                                {
                                    beData.AttachmentColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );

                                }

                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.AttachmentColl.Add(v);
                            }
                        }
                    }

                    bool isModify = false;



                    if (!beData.ExaminerId.HasValue)
                        beData.ExaminerId = 0;

                    resVal = new AcademicERP.BL.Examiner(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.Examiner)]
        public JsonNetResult getExaminerById(int ExaminerId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Examiner(User.UserId, User.HostName, User.DBName).GetExaminerById(0, ExaminerId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Examiner)]
        public JsonNetResult DeleteExaminer(int ExaminerId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ExaminerId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Examiner(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, ExaminerId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllExaminer()
        {
            AcademicERP.BE.ExaminerCollections dataColl = new AcademicERP.BE.ExaminerCollections();
            try
            {
                dataColl = new AcademicERP.BL.Examiner(User.UserId, User.HostName, User.DBName).GetAllExaminer(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        #region "UserNameList"
        [HttpGet]
        public JsonNetResult GetAllUserNameList()
        {
            AcademicERP.BE.UserNameListCollections dataColl = new AcademicERP.BE.UserNameListCollections();
            try
            {
                dataColl = new AcademicERP.BL.UserNameList(User.UserId, User.HostName, User.DBName).GetAllUserNameList(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        #region "CheckupGroup"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.CheckupGroup)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.CheckupGroup)]
        public JsonNetResult SaveUpdateCheckupGroup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.CheckupGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CheckupGroupId.HasValue)
                        beData.CheckupGroupId = 0;

                    resVal = new AcademicERP.BL.CheckupGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.CheckupGroup)]
        public JsonNetResult getCheckupGroupById(int CheckupGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.CheckupGroup(User.UserId, User.HostName, User.DBName).GetCheckupGroupById(0, CheckupGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.CheckupGroup)]
        public JsonNetResult DeleteCheckupGroup(int CheckupGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (CheckupGroupId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.CheckupGroup(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, CheckupGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllCheckupGroup()
        {
            AcademicERP.BE.CheckupGroupCollections dataColl = new AcademicERP.BE.CheckupGroupCollections();
            try
            {
                dataColl = new AcademicERP.BL.CheckupGroup(User.UserId, User.HostName, User.DBName).GetAllCheckupGroup(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        #region "TestName"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.TestName)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.TestName)]
        public JsonNetResult SaveUpdateTestName()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.TestName>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TestNameId.HasValue)
                        beData.TestNameId = 0;

                    resVal = new AcademicERP.BL.TestName(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getTestNameById(int TestNameId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.TestName(User.UserId, User.HostName, User.DBName).GetTestNameById(0, TestNameId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.TestName)]
        public JsonNetResult DeleteTestName(int TestNameId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TestNameId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.TestName(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TestNameId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllTestName()
        {
            AcademicERP.BE.TestNameCollections dataColl = new AcademicERP.BE.TestNameCollections();
            try
            {
                dataColl = new AcademicERP.BL.TestName(User.UserId, User.HostName, User.DBName).GetAllTestName(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        //[HttpGet]
        //public JsonNetResult GetAllHealthCampaignClassSections()
        //{
        //    var diseases = new AcademicLib.BL.Infirmary.HealthCampaign(User.UserId, User.HostName, User.DBName).getAllClassSection();
        //    int totalCount = Convert.ToInt32(diseases.Count);
        //    return new JsonNetResult() { Data = diseases, TotalCount = totalCount, IsSuccess = diseases.IsSuccess, ResponseMSG = diseases.ResponseMSG };
        //}



        #region "HealthOperation"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.HealthOperation)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.HealthOperation)]
        public JsonNetResult SaveUpdateHealthOperation()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.HealthOperation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HealthCampaignId.HasValue)
                        beData.HealthCampaignId = 0;

                    resVal = new AcademicERP.BL.HealthOperation(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.HealthOperation)]
        public JsonNetResult getHealthOperationById(int HealthCampaignId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.HealthOperation(User.UserId, User.HostName, User.DBName).GetHealthOperationById(0, HealthCampaignId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.HealthOperation)]
        public JsonNetResult DeleteHealthOperation(int HealthCampaignId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (HealthCampaignId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.HealthOperation(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, HealthCampaignId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllHealthOperation()
        {
            AcademicERP.BE.HealthOperationCollections dataColl = new AcademicERP.BE.HealthOperationCollections();
            try
            {
                dataColl = new AcademicERP.BL.HealthOperation(User.UserId, User.HostName, User.DBName).GetAllHealthOperation(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        //Added By Bivek on 27 Chaitra

        #region "GeneralCheckUp"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.GeneralCheckUp)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.GeneralCheckUp)]
        public JsonNetResult SaveUpdateGeneralCheckUp()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.GeneralCheckUp>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.GeneralCheckUpId.HasValue)
                        beData.GeneralCheckUpId = 0;

                    resVal = new AcademicERP.BL.GeneralCheckUp(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.GeneralCheckUp)]
        public JsonNetResult getGeneralCheckUpById(int GeneralCheckUpId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.GeneralCheckUp(User.UserId, User.HostName, User.DBName).GetGeneralCheckUpById(0, GeneralCheckUpId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.GeneralCheckUp)]
        public JsonNetResult DeleteGeneralCheckUp(int GeneralCheckUpId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (GeneralCheckUpId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.GeneralCheckUp(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, GeneralCheckUpId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllGeneralCheckUp()
        {
            AcademicERP.BE.GeneralCheckUpCollections dataColl = new AcademicERP.BE.GeneralCheckUpCollections();
            try
            {
                dataColl = new AcademicERP.BL.GeneralCheckUp(User.UserId, User.HostName, User.DBName).GetAllGeneralCheckUp(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion




        #region "HealthChekUpExam"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.HealthChekUpExam)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.HealthChekUpExam)]
        public JsonNetResult SaveUpdateHealthChekUpExam()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.HealthChekUpExam>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamCheckUpId.HasValue)
                        beData.ExamCheckUpId = 0;

                    resVal = new AcademicERP.BL.HealthChekUpExam(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.HealthChekUpExam)]
        public JsonNetResult getHealthChekUpExamById(int ExamCheckUpId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.HealthChekUpExam(User.UserId, User.HostName, User.DBName).GetHealthChekUpExamById(0, ExamCheckUpId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.HealthChekUpExam)]
        public JsonNetResult DeleteHealthChekUpExam(int ExamCheckUpId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ExamCheckUpId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.HealthChekUpExam(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, ExamCheckUpId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllHealthChekUpExam()
        {
            AcademicERP.BE.HealthChekUpExamCollections dataColl = new AcademicERP.BE.HealthChekUpExamCollections();
            try
            {
                dataColl = new AcademicERP.BL.HealthChekUpExam(User.UserId, User.HostName, User.DBName).GetAllHealthChekUpExam(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion




        #region "CopyExamType"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.CopyExamType)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.CopyExamType)]
        public JsonNetResult SaveUpdateCopyExamType()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.CopyExamType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CopyExamTypeId.HasValue)
                        beData.CopyExamTypeId = 0;

                    resVal = new AcademicERP.BL.CopyExamType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.CopyExamType)]
        public JsonNetResult getCopyExamTypeById(int CopyExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.CopyExamType(User.UserId, User.HostName, User.DBName).GetCopyExamTypeById(0, CopyExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.CopyExamType)]
        public JsonNetResult DeleteCopyExamType(int CopyExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (CopyExamTypeId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.CopyExamType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, CopyExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllCopyExamType()
        {
            AcademicERP.BE.CopyExamTypeCollections dataColl = new AcademicERP.BE.CopyExamTypeCollections();
            try
            {
                dataColl = new AcademicERP.BL.CopyExamType(User.UserId, User.HostName, User.DBName).GetAllCopyExamType(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion




        #region "StudentDetails"
        [HttpPost]
        public JsonNetResult GetAllStudentDetails(int ClassId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentForTran(this.AcademicYearId, ClassId, null, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion
        //Added By Suresh in 27 Chaitra starts from here. New Code Starts

        #region "HealthCamInfirmary"
        [HttpGet]
        public JsonNetResult GetAllStudentForHCInfirmary(int? ClassId)
        {
            AcademicERP.BE.StudentForHCInfirmaryCollections dataColl = new AcademicERP.BE.StudentForHCInfirmaryCollections();
            try
            {
                dataColl = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).GetAllStudentForHCInfirmary(0, ClassId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveHealthCampaignInfirmary()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.HealthCamInfirmary>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HealthCamInfirmaryId.HasValue)
                        beData.HealthCamInfirmaryId = 0;

                    resVal = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllHCInfirmary()
        {
            AcademicERP.BE.HealthCamInfirmaryCollections dataColl = new AcademicERP.BE.HealthCamInfirmaryCollections();
            try
            {
                dataColl = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).GetAllHealthCamInfirmary(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult getHCInfirmaryById(int HealthCamInfirmaryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).GetHealthCamInfirmaryById(0, HealthCamInfirmaryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteHCInfirmary(int HealthCamInfirmaryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (HealthCamInfirmaryId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Health Campaign Infirmary";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, HealthCamInfirmaryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.GeneralCheckUp)]
        public JsonNetResult getDataForHealthCampaignById(int HealthCampaignId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).GetDataForHealthCampaignById(0, HealthCampaignId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "GeneralCheckupInfirmary"
        [HttpGet]
        public JsonNetResult GetAllStudentForGChkupInfirmary(int? ClassId)
        {
            AcademicERP.BE.StudentForGCInfirmaryCollections dataColl = new AcademicERP.BE.StudentForGCInfirmaryCollections();
            try
            {
                dataColl = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).GetAllStudentForGChkupInfirmary(0, ClassId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult getHCInfirmaryForDetailById(int HealthCamInfirmaryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.HealthCamInfirmary(User.UserId, User.HostName, User.DBName).GetHealthCamInfirmaryForDetailById(0, HealthCamInfirmaryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveGeneralChkupInfirmary()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.GeneralCheckupInfirmary>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllGeneralChkupInfirmry()
        {
            AcademicERP.BE.GeneralCheckupInfirmaryCollections dataColl = new AcademicERP.BE.GeneralCheckupInfirmaryCollections();
            try
            {
                dataColl = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).GetAllGeneralCheckupInfirmary(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult getGeneralChkupInfirmaryById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).GetGeneralCheckupInfirmaryById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteGeneralChkupInfirmary(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default General Checkup Infirmary";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.GeneralCheckUp)]
        public JsonNetResult getDataForGeneralCheckupById(int GeneralCheckUpId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).GetDataForGeneralCheckupById(0, GeneralCheckUpId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "StudentInfirmary"
        //Student Infirmary Starts Done By Suresh
        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.HealthOperation)]
        public JsonNetResult getStudentDetForInfirmarybyId(int StudentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentDetForInfirmary(0, StudentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        //New code added for health campaign detail
        [HttpPost]
        public JsonNetResult getGeneralChkupForDetailById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.GeneralCheckupInfirmary(User.UserId, User.HostName, User.DBName).GetGeneralCheckupForDetailById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllMedicalProduct()
        {
            AcademicERP.BE.MedicalProductsCollections dataColl = new AcademicERP.BE.MedicalProductsCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetAllMEdicalProducts(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateStudentPMHistory()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.StudentPastMedicalHistory>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.StudentPastMedicalDocumentsColl;
                    beData.StudentPastMedicalDocumentsColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        //var UserPhoto = filesColl["UserPhoto"];

                        //if (UserPhoto != null)
                        //{
                        //    var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                        //    beData.Photo = photoDoc.Data;
                        //    beData.PhotoPath = photoDoc.DocPath;
                        //}

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                if (att != null)
                                {
                                    beData.StudentPastMedicalDocumentsColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.StudentPastMedicalDocumentsColl.Add(v);
                            }
                        }
                    }

                    bool isModify = false;



                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).SaveFormDataPMHistory(beData);
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


        [HttpGet]
        public JsonNetResult GetAllPastMedicalHistoryList()
        {
            AcademicERP.BE.StudentPastMedicalHistoryCollections dataColl = new AcademicERP.BE.StudentPastMedicalHistoryCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetAllStudentPastMedicalHistory(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getStudentPastMedicalHistoryById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentPastMedicalHistoryById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.TestName)]
        public JsonNetResult DeleteStudentPastMedicalHistoryById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Past History";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).DeletePastMedicalHistorybyId(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateStudentHealthIssue()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.StudentHealthIssue>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.HealthIssueAttachmentColl;
                    beData.HealthIssueAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        //var UserPhoto = filesColl["UserPhoto"];

                        //if (UserPhoto != null)
                        //{
                        //    var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                        //    beData.Photo = photoDoc.Data;
                        //    beData.PhotoPath = photoDoc.DocPath;
                        //}

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                if (att != null)
                                {
                                    beData.HealthIssueAttachmentColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.HealthIssueAttachmentColl.Add(v);
                            }
                        }
                    }

                    bool isModify = false;



                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).SaveFormDataHealthISsue(beData);
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


        [HttpGet]
        public JsonNetResult GetAllStudentHealthIssue()
        {
            AcademicERP.BE.StudentHealthIssueCollections dataColl = new AcademicERP.BE.StudentHealthIssueCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetAllStudentHealthIssue(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getStudentHealthIssueById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentHealthIssueById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.TestName)]
        public JsonNetResult DeleteStudentHealthIssueById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Past History";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).DeleteStudentHealthIssueById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateStudentImmunization()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.StudentHealthImmunization>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.StudentImmunizationAttachmentColl;
                    beData.StudentImmunizationAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        //var UserPhoto = filesColl["UserPhoto"];

                        //if (UserPhoto != null)
                        //{
                        //    var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                        //    beData.Photo = photoDoc.Data;
                        //    beData.PhotoPath = photoDoc.DocPath;
                        //}

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                if (att != null)
                                {
                                    beData.StudentImmunizationAttachmentColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.StudentImmunizationAttachmentColl.Add(v);
                            }
                        }
                    }
                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).SaveStudentImmunization(beData);
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


        [HttpGet]
        public JsonNetResult GetAllStudentImmunization()
        {
            AcademicERP.BE.StudentHealthImmunizationCollections dataColl = new AcademicERP.BE.StudentHealthImmunizationCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetAllStudentHealthImmunization(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getStudentImmunizationId(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentHealthImmunizationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.TestName)]
        public JsonNetResult DeleteStudentImmunizationById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Student Immunization";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).DeleteStudentImmunizationById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(CrmLib.BE.Global.Actions.Modify, (int)FormsEntity.CreateCustomer)]
        public JsonNetResult GetLabValueById(int TestNameId)
        {
            AcademicERP.BE.LabValueCollections dataColl = new AcademicERP.BE.LabValueCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetLabValueById(0, TestNameId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Vaccine)]
        public JsonNetResult SaveUpdateStudentCLEvaluation()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.StudentClinicalLabEvaluation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).SaveFormDataStudentCLEvaluation(beData);
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


        [HttpGet]
        public JsonNetResult GetAllStudentLabEvaluation()
        {
            AcademicERP.BE.StudentClinicalLabEvaluationCollections dataColl = new AcademicERP.BE.StudentClinicalLabEvaluationCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetAllStudentClinicalLabEvaluation(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getStudentLabEvaluationbyId(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentClinicalLabEvaluationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.TestName)]
        public JsonNetResult DeleteStudentLabEvaluationById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Student Immunization";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).DeleteStudentLabEvaluationById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveStudentGeneralCheckup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.StudentGCheckupCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).SaveStudentGeneralCheckup(beData);
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
        #endregion

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getStudentMedicalHistoryById(int StudentId)
        {
            AcademicERP.BE.StudentPastMedicalHistoryCollections dataColl = new AcademicERP.BE.StudentPastMedicalHistoryCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).getStudentMedicalHistoryById(0, StudentId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getStudentMedicalIssuesById(int StudentId)
        {
            AcademicERP.BE.StudentHealthIssueCollections dataColl = new AcademicERP.BE.StudentHealthIssueCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).getStudentMedicalIssuesById(0, StudentId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetStudentGeneralCheckup(int StudentId)
        {
            AcademicERP.BE.StudentGCheckupCollections dataColl = new AcademicERP.BE.StudentGCheckupCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentGeneralCheckup(0, StudentId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetStudentHealthImmunization(int StudentId)
        {
            AcademicERP.BE.StudentHealthImmunizationCollections dataColl = new AcademicERP.BE.StudentHealthImmunizationCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentHealthImmunization(0, StudentId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetStudentHealthImmunizationDoc(int TranId)
        {
            Dynamic.BusinessEntity.GeneralDocumentCollections dataColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentHealthImmunizationDoc(0, TranId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetStudentClinicalLabEvaluation(int StudentId)
        {
            AcademicERP.BE.StudentClinicalLabEvaluationCollections dataColl = new AcademicERP.BE.StudentClinicalLabEvaluationCollections();
            try
            {
                dataColl = new AcademicERP.BL.StudentInfirmary(User.UserId, User.HostName, User.DBName).GetStudentClinicalLabEvaluation(0, StudentId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Employee Infirmary"
        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.HealthOperation)]
        public JsonNetResult getEmployeeDetForInfirmarybyId(int EmployeeId)
        {
            AcademicLib.BE.EmployeeDetForInfirmary resVal = new AcademicLib.BE.EmployeeDetForInfirmary();
            try
            {
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).getEmployeeForInfirmaryById(0, EmployeeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateEmployeePMHistory()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.EmployeePastMedicalHistory>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.EmployeePastMedicalDocumentsColl;
                    beData.EmployeePastMedicalDocumentsColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        //var UserPhoto = filesColl["UserPhoto"];

                        //if (UserPhoto != null)
                        //{
                        //    var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                        //    beData.Photo = photoDoc.Data;
                        //    beData.PhotoPath = photoDoc.DocPath;
                        //}

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                if (att != null)
                                {
                                    beData.EmployeePastMedicalDocumentsColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.EmployeePastMedicalDocumentsColl.Add(v);
                            }
                        }
                    }

                    bool isModify = false;



                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).SaveFormDataPMHistory(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getEmployeeMedicalHistoryById(int EmployeeId)
        {
            AcademicLib.BE.EmployeePastMedicalHistoryCollections dataColl = new AcademicLib.BE.EmployeePastMedicalHistoryCollections();
            try
            {
                dataColl = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).getEmployeeMedicalHistoryById(0, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getEmployeePastMedicalHistoryById(int TranId)
        {
            AcademicLib.BE.EmployeePastMedicalHistory resVal = new AcademicLib.BE.EmployeePastMedicalHistory();
            try
            {
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).GetEmployeePastMedicalHistoryById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelEmpPastMedHty(int? EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Employee Immunization";
                    resVal.IsSuccess = false;
                }
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).DelEmpPastMedHty(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        //For Health Issue Tab code starts

        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getEmployeeMedicalIssuesById(int EmployeeId)
        {
            AcademicLib.BE.EmployeeHealthIssueCollections dataColl = new AcademicLib.BE.EmployeeHealthIssueCollections();
            try
            {
                dataColl = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).getEmployeeMedicalIssuesById(0, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateEmployeeHealthIssue()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.EmployeeHealthIssue>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.HealthIssueAttachmentColl;
                    beData.HealthIssueAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        //var UserPhoto = filesColl["UserPhoto"];

                        //if (UserPhoto != null)
                        //{
                        //    var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                        //    beData.Photo = photoDoc.Data;
                        //    beData.PhotoPath = photoDoc.DocPath;
                        //}

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                if (att != null)
                                {
                                    beData.HealthIssueAttachmentColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.HealthIssueAttachmentColl.Add(v);
                            }
                        }
                    }

                    bool isModify = false;



                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).SaveFormDataHealthISsue(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetEmployeeHealthIssueById(int TranId)
        {
            AcademicLib.BE.EmployeeHealthIssue resVal = new AcademicLib.BE.EmployeeHealthIssue();
            try
            {
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).getEmployeeHealthIssueById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult DelEmpHealthIssue(int? EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Employee Immunization";
                    resVal.IsSuccess = false;
                }
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).DeleteEmployeeHealthIssue(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveEmployeeGeneralCheckup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.EmployeeGCheckupCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).SaveEmployeeGeneralCheckup(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetEmployeeGeneralCheckup(int EmployeeId)
        {
            AcademicLib.BE.EmployeeGCheckupCollections dataColl = new AcademicLib.BE.EmployeeGCheckupCollections();
            try
            {
                dataColl = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).GetEmployeeGeneralCheckup(0, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Examiner)]
        public JsonNetResult SaveUpdateEmployeeImmunization()
        {
            string photoLocation = "/Attachments/Infirmary/HealthExaminer";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.EmployeeHealthImmunization>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.EmployeeImmunizationAttachmentColl;
                    beData.EmployeeImmunizationAttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        //var UserPhoto = filesColl["UserPhoto"];

                        //if (UserPhoto != null)
                        //{
                        //    var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                        //    beData.Photo = photoDoc.Data;
                        //    beData.PhotoPath = photoDoc.DocPath;
                        //}

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                if (att != null)
                                {
                                    beData.EmployeeImmunizationAttachmentColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.EmployeeImmunizationAttachmentColl.Add(v);
                            }
                        }
                    }
                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).SaveEmployeeImmunization(beData);
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
        public JsonNetResult GetEmployeeHealthImmunization(int EmployeeId)
        {
            AcademicLib.BE.EmployeeHealthImmunizationCollections dataColl = new AcademicLib.BE.EmployeeHealthImmunizationCollections();
            try
            {
                dataColl = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).GetEmployeeHealthImmunization(0, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getEmployeeImmunizationId(int TranId)
        {
            AcademicLib.BE.EmployeeHealthImmunization resVal = new AcademicLib.BE.EmployeeHealthImmunization();
            try
            {
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).GetEmployeeHealthImmunizationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetEmployeeHealthImmunizationDoc(int TranId)
        {
            Dynamic.BusinessEntity.GeneralDocumentCollections dataColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
            try
            {
                dataColl = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).GetEmployeeHealthImmunizationDoc(0, TranId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelEmployeeHealthImmunzation(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Employee Health Immunzation";
                    resVal.IsSuccess = false;
                }
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).DelEmpHealthImmunization(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,
                TotalCount = 0,
                IsSuccess = resVal.IsSuccess,
                ResponseMSG = resVal.ResponseMSG
            };
        }
        //Clinical Lab Evaluation 
        [HttpPost]
        public JsonNetResult SaveEmpCliLabEvaluation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.EmployeeClinicalLabEvaluation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (beData.TranId == null)
                        beData.TranId = 0;
                    resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).SaveFormDataEmployeeCLEvaluation(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,
                TotalCount = 0,
                IsSuccess = resVal.IsSuccess,
                ResponseMSG = resVal.ResponseMSG
            };
        }
        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult GetEmpLabEval(int EmployeeId)
        {
            AcademicLib.BE.EmployeeClinicalLabEvaluationCollections dataColl = new AcademicLib.BE.EmployeeClinicalLabEvaluationCollections();
            try
            {
                dataColl = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).GetEmpLabEval(0, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.TestName)]
        public JsonNetResult getEmployeeClinicalLabEvaluationById(int TranId)
        {
            AcademicLib.BE.EmployeeClinicalLabEvaluation resVal = new AcademicLib.BE.EmployeeClinicalLabEvaluation();
            try
            {
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).getEmployeeClinicalLabEvaluationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteSLEvaluationById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Employee Health Immunzation";
                    resVal.IsSuccess = false;
                }
                resVal = new AcademicLib.BL.EmployeeInfirmary(User.UserId, User.HostName, User.DBName).DeleteSLEvaluationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,
                TotalCount = 0,
                IsSuccess = resVal.IsSuccess,
                ResponseMSG = resVal.ResponseMSG
            };
        }
        #endregion
    }
}