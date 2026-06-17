
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

namespace PivotalERP.Controllers
{
    public class ScholarshipController : BaseController
    {
        // GET: Scholarship

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ScholarshipSetup, false)]
        public ActionResult Setup()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult ApplyScholarship()
        {
            return View();
        }

        public ActionResult ScholarshipVerify()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ScholarshipVerify, false)]
        public ActionResult ScholarshipDocVerify()
        {
            return View();
        }

        public ActionResult ViewAdmitCard()
        {
            return View();
        }

        public ActionResult GenerateRollNo()
        {
            return View();
        }
        public ActionResult ExamCenter()
        {
            return View();
        }

        public ActionResult ExamCenterMapping()
        {
            return View();
        }
        public ActionResult StudentListForExam()
        {
            return View();
        }

        #region "EquivalentBoard"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.EquivalentBoard)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.EquivalentBoard)]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ScholarshipSetup, false)]
        [ValidateInput(false)]
        public JsonNetResult SaveEquivalentBoard()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.EquivalentBoard>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.BoardId.HasValue)
                        beData.BoardId = 0;

                    resVal = new AcademicLib.BL.Scholarship.EquivalentBoard(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.EquivalentBoard)]
        public JsonNetResult getEquivalentBoardById(int BoardId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.EquivalentBoard(User.UserId, User.HostName, User.DBName).GetEquivalentBoardById(0, BoardId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult DeleteEquivalentBoard(int BoardId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BoardId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.EquivalentBoard(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BoardId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllEquivalentBoard()
        {
            AcademicLib.BE.Scholarship.EquivalentBoardCollections dataColl = new AcademicLib.BE.Scholarship.EquivalentBoardCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.EquivalentBoard(1, hostName, dbName).GetAllEquivalentBoard(0);
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

        #region "AppliedSchool"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.AppliedSchool)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ScholarshipSetup, false)]
        [ValidateInput(false)]
        public JsonNetResult SaveAppliedSchool()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.AppliedSchool>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.SchoolId.HasValue)
                        beData.SchoolId = 0;

                    resVal = new AcademicLib.BL.Scholarship.AppliedSchool(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.AppliedSchool)]
        public JsonNetResult getAppliedSchoolById(int SchoolId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.AppliedSchool(User.UserId, User.HostName, User.DBName).GetAppliedSchoolById(0, SchoolId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult DeleteAppliedSchool(int SchoolId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (SchoolId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.AppliedSchool(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, SchoolId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllAppliedSchool()
        {
            AcademicLib.BE.Scholarship.AppliedSchoolCollections dataColl = new AcademicLib.BE.Scholarship.AppliedSchoolCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.AppliedSchool(1, hostName, dbName).GetAllAppliedSchool(0);
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

        #region "ReservationGroup"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.ReservationGroup)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult SaveReservationGroup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.ReservationGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Scholarship.ReservationGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.ReservationGroup)]
        public JsonNetResult getReservationGroupById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.ReservationGroup(User.UserId, User.HostName, User.DBName).GetReservationGroupById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult DeleteReservationGroup(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.ReservationGroup(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllReservationGroup()
        {
            AcademicLib.BE.Scholarship.ReservationGroupCollections dataColl = new AcademicLib.BE.Scholarship.ReservationGroupCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.ReservationGroup(1, hostName, dbName).GetAllReservationGroup(0);
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

        #region "ReservationType"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.ReservationType)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult SaveReservationType()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.ReservationType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Scholarship.ReservationType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.ReservationType)]
        public JsonNetResult getReservationTypeById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.ReservationType(User.UserId, User.HostName, User.DBName).GetReservationTypeById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult DeleteReservationType(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.ReservationType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllReservationType()
        {
            AcademicLib.BE.Scholarship.ReservationTypeCollections dataColl = new AcademicLib.BE.Scholarship.ReservationTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.ReservationType(1, hostName, dbName).GetAllReservationType(0);
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

        #region "Authority"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Authority)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult SaveAuthority()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.Authority>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AuthorityId.HasValue)
                        beData.AuthorityId = 0;

                    resVal = new AcademicLib.BL.Scholarship.Authority(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.Authority)]
        public JsonNetResult getAuthorityById(int AuthorityId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.Authority(User.UserId, User.HostName, User.DBName).GetAuthorityById(0, AuthorityId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ScholarshipSetup, false)]
        public JsonNetResult DeleteAuthority(int AuthorityId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (AuthorityId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.Authority(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, AuthorityId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllAuthority()
        {
            AcademicLib.BE.Scholarship.AuthorityCollections dataColl = new AcademicLib.BE.Scholarship.AuthorityCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.Authority(1, hostName, dbName).GetAllAuthority(0);
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

        #region "Scholarship"

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public JsonNetResult SaveScholarship()
        {
            string photoLocation = "/Attachments/fronddesk";
            ResponeValues resVal = new ResponeValues();

            try
            {
               

                var beData = DeserializeObject<AcademicLib.BE.Scholarship.Scholarship>(Request["jsonData"]);

                if (beData != null)
                {
                    if(!beData.TranId.HasValue || beData.TranId == 0)
                    {
                        var lastDate = new DateTime(2024, 7, 29, 01, 01, 0);
                        var nowDate = DateTime.Now;
                        if (nowDate > lastDate)
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "मिति नाघेको कारण आवेदन दिन असमर्थ |";
                            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                        }
                    }
                    //else if (beData.TranId > 0)
                    //{
                    //    var lastDate = new DateTime(2024, 09, 16, 23, 59, 0);
                    //    var nowDate = DateTime.Now;
                    //    if (nowDate > lastDate)
                    //    {
                    //        resVal.IsSuccess = false;
                    //        resVal.ResponseMSG = "मिति नाघेको कारण आवेदन दिन असमर्थ |";
                    //        return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}
                   

                    beData.IPAddress = GetIp();
                    beData.Agent = Request.UserAgent;
                    beData.Browser = Request.Browser.Browser;

                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        var signature = filesColl["signature"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.PhotoPath = photoDoc.DocPath;
                        }

                        if (signature != null)
                        {
                            var signatureDoc = GetAttachmentDocuments(photoLocation, signature, true);
                            beData.SignaturePath = signatureDoc.DocPath;
                        }

                        var UserPhoto = filesColl["UserPhoto"];
                        var UserPhoto1 = filesColl["UserPhoto1"];
                        var UserPhoto2 = filesColl["UserPhoto2"];
                        var UserPhoto3 = filesColl["UserPhoto3"];
                        var UserPhoto4 = filesColl["UserPhoto4"];
                        var UserPhoto5 = filesColl["UserPhoto5"];
                        var UserPhoto6 = filesColl["UserPhoto6"];
                        var UserPhoto7 = filesColl["UserPhoto7"];
                        //var UserPhoto6 = filesColl["UserPhoto6"];
                        var UserPhoto8 = filesColl["UserPhoto8"];

                        var RelatedSchoolFile = filesColl["RelatedSchoolFile"];

                        if (RelatedSchoolFile != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, RelatedSchoolFile, true);
                            beData.RelatedSchoolFilePath = photoDoc.DocPath;
                        }

                        var GradeSheetFile = filesColl["GradeSheetFile"];

                        if (GradeSheetFile != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, GradeSheetFile, true);
                            beData.GradeSheet_CertiPath = photoDoc.DocPath;
                        }

                        if (UserPhoto != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                            beData.BC_FilePath = photoDoc.DocPath;
                        }
                        //Citizenship front
                        if (UserPhoto1 != null)
                        {
                            var photoDoc1 = GetAttachmentDocuments(photoLocation, UserPhoto1, true);
                            beData.CtznshipFront_FilePath = photoDoc1.DocPath;
                        }

                        //Citizenship Back
                        if (UserPhoto2 != null)
                        {
                            var photoDoc2 = GetAttachmentDocuments(photoLocation, UserPhoto2, true);
                            beData.CtznshipBack_FilePath = photoDoc2.DocPath;
                        }

                        //Character / Transfer Certificate / Recommendation Letter Upload
                        if (UserPhoto3 != null)
                        {
                            var photoDoc3 = GetAttachmentDocuments(photoLocation, UserPhoto3, true);
                            beData.Character_Transfer_Certi = photoDoc3.DocPath;
                        }

                        /*Poverty Certificate by Municipality*/
                        if (UserPhoto4 != null)
                        {
                            var photoDoc4 = GetAttachmentDocuments(photoLocation, UserPhoto4, true);
                            beData.Poverty_CertiFilePath = photoDoc4.DocPath;
                        }

                        /*Government School Certificate*/
                        if (UserPhoto5 != null)
                        {
                            var photoDoc5 = GetAttachmentDocuments(photoLocation, UserPhoto5, true);
                            beData.GovSchoolCertiPath = photoDoc5.DocPath;
                        }


                        /*Migration Document Certificate*/
                        if (UserPhoto6 != null)
                        {
                            var photoDoc6 = GetAttachmentDocuments(photoLocation, UserPhoto6, true);
                            beData.MigDocPath = photoDoc6.DocPath;
                        }

                        /*Landfill Document Certificate*/
                        if (UserPhoto7 != null)
                        {
                            var photoDoc7 = GetAttachmentDocuments(photoLocation, UserPhoto7, true);
                            beData.LandFillDocPath = photoDoc7.DocPath;
                        }

                        if (UserPhoto8 != null)
                        {
                            var photoDoc8 = GetAttachmentDocuments(photoLocation, UserPhoto8, true);
                            beData.Anusuchi3DocPath = photoDoc8.DocPath;
                        }

                        if (beData.ReservationGroupList != null)
                        {
                            foreach (var rs in beData.ReservationGroupList)
                            {
                                if (rs.ReservationGroupId.HasValue)
                                {
                                    var fn = "CA" + rs.ReservationGroupId.ToString();
                                    var newDoc = filesColl[fn];
                                    if (newDoc != null)
                                    {
                                        var attD = GetAttachmentDocuments(photoLocation, newDoc, true);
                                        rs.GroupWiseCerti_Path = attD.DocPath;
                                    }
                                }
                            }
                        }

                    }

                    bool isModify = false;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    else if (beData.TranId > 0)
                        isModify = true;

                    beData.CUserId = 1;

                    resVal = new AcademicLib.BL.Scholarship.Scholarship(1, hostName, dbName).SaveFormData(beData);

                    if (resVal.IsSuccess && isModify == false)
                    {
                        // SendEmail(beData,resVal.ResponseId); 
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accepted";
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

        #region "Scholarship Verify"


        [HttpPost]
        public JsonNetResult ResetPwd(List<int> TranIdColl)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                string idcoll = "";
                if (TranIdColl != null)
                {
                    foreach (var v in TranIdColl)
                    {
                        if (!string.IsNullOrEmpty(idcoll))
                            idcoll = idcoll + ",";

                        idcoll = idcoll + v.ToString();
                    }

                    resVal = new AcademicLib.BL.Scholarship.Scholarship(User.UserId, User.HostName, User.DBName).ResetPwd(idcoll);
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid data.";
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
        public JsonNetResult GetAllScholarship(TableFilter filter, int? StatusId, int? SubjectId,int? ClassId)
        {
            AcademicLib.BE.Scholarship.ScholarshipCollections dataColl = new AcademicLib.BE.Scholarship.ScholarshipCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.Scholarship(User.UserId, User.HostName, User.DBName).getAllScholarship(filter, StatusId, SubjectId,ClassId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.Scholarship)]
        public JsonNetResult getScholarshipById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.Scholarship(User.UserId, User.HostName, User.DBName).GetScholarshipById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ScholarshipVerify)]
        [ValidateInput(false)]
        public JsonNetResult SaveScholarshipVerify()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.ScholarshipVerify>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Scholarship.ScholarshipVerify(User.UserId, User.HostName, User.DBName).SaveFormData(beData);


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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.ScholarshipVerify)]
        public JsonNetResult getScholarshipVerifyById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.ScholarshipVerify(User.UserId, User.HostName, User.DBName).GetScholarshipVerifyById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonNetResult GetSchoolSubjectWise(int? SubjectId, int? ClassId)
        {
            AcademicLib.BE.Scholarship.SchoolSubjectwiseCollections dataColl = new AcademicLib.BE.Scholarship.SchoolSubjectwiseCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.Scholarship(1, hostName, dbName).GetSchoolSubjectWise(0, SubjectId, ClassId);
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
        [AllowAnonymous]
        public JsonNetResult GetAllGradeSheet(string SEESymbolNo, string Alphabet, DateTime DOB_AD, string DOB_BS, double? GPA, string Pwd, int? ClassId)
        {
            AcademicLib.BE.Scholarship.GradeSheetCollections dataColl = new AcademicLib.BE.Scholarship.GradeSheetCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.Scholarship(1, hostName, dbName).GetAllGradeSheet(0, SEESymbolNo, Alphabet, DOB_AD, DOB_BS, GPA, Pwd, ClassId);
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
        [AllowAnonymous]
        public JsonNetResult CheckScholarshipApply(string SEESymbolNo, string Alphabet, int? ClassId)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.Scholarship(1, hostName, dbName).CheckScholarshipApply(SEESymbolNo, Alphabet, ClassId);
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ScholarshipVerify, false)]
        [ValidateInput(false)]
        public JsonNetResult SaveScholarshipDocVerify()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.ScholarshipDocVerify>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.VerifyId.HasValue)
                        beData.VerifyId = 0;

                    beData.SMSText = getDocVerifyTextForSMS(beData);
                    resVal = new AcademicLib.BL.Scholarship.ScholarshipDocVerify(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    try
                    {
                        if (resVal.IsSuccess)
                        {
                            Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                            auditLog.TranId = beData.TranId.Value;
                            auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StudentProfile;
                            auditLog.Action = Actions.Save;
                            auditLog.LogText = "Scholarship Doc. Verification ";
                            auditLog.AutoManualNo = beData.TranId.Value.ToString() + "(" + beData.ScholarshipDet.SEESymbolNo + " " + beData.ScholarshipDet.Alphabet + ")";
                            SaveAuditLog(auditLog);
                        }
                    }
                    catch { }


                    if (resVal.IsSuccess)
                    {
                        SendVerifyEmail(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.ScholarshipVerify)]
        public JsonNetResult getScholarshipDocVerifyById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.ScholarshipDocVerify(User.UserId, User.HostName, User.DBName).GetScholarshipDocVerifyById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        private void SendEmail(AcademicLib.BE.Scholarship.Scholarship beData, string pwd)
        {
            try
            {
                Dynamic.BusinessEntity.Global.MailDetails mailDet = new Dynamic.BusinessEntity.Global.MailDetails();
                mailDet.To = beData.Email;
                mailDet.Subject = "छात्रवृत्ति आवेदन फाराम सम्बन्धमा।";
                mailDet.CUserId = 1;
                mailDet.Message = $@"<div>      

             <p>प्यारा विद्यार्थी <span style=""padding-left: 15px;""><b>{beData.FullName}</b></span></p>
            <p>SEE SN <span style=""padding-left: 33px;""> <b style=""border-bottom: 2px dashed #000000; padding-right: 20px;"">: {beData.SEESymbolNo} {beData.Alphabet}</b></span> </p>
            <p>DOB <span style=""padding-left: 53px;""><b style=""border-bottom: 2px dashed #000000; padding-right: 20px;"">: {beData.DOBMiti} </b></span></p>

            <p style=""margin-top:14px"">
                तपाईले वीरगंज महानगरपालिकाको सूचना अनुसार कक्षा ११ मा <span style=""border-bottom: 2px dashed #000000;padding-left: 20px;padding-right: 20px;""> <b>{beData.SubjectName}</b> </span> विषय अध्ययन गर्न छात्रवृत्तिको लागि पेस गर्नु भएको आवेदन फाराम प्राप्त
                भयो । यो आवेदनमा तपाइले पेस गर्नु भएको विवरण र अपलोड गरेका कागजात विज्ञबाट रूजु यकिन गरे पछी फाराम स्वीकृत भएमा मात्र छात्रवृत्ति परीक्षामा सहभागी हुन पाउनु हुन्छ । त्यसैले देहाय बमोजिम गर्नुहोला ।
            </p>

            <ul style=""list-style: none;padding-left: 15px;"">
                <li>
                    १) छात्रवृत्तिको अनलाईन फारम भर्दा <span style=""border-bottom: 2px dashed #000000;padding-left: 20px;padding-right: 20px;""><b>{beData.SubjectName}</b> </span> विषय <span style=""border-bottom: 2px dashed #000000;padding-left: 20px;padding-right: 20px;""><b>{beData.SchoolPriorityListColl.Count.ToString()}</b> </span>                    वटा विद्यालय/कलेजमा अध्ययन गर्न फाराम भर्नु भएको छ । आवेदन फाराम भर्दा त्रुटि भएमा वा कुनै विवरण सच्याउनु परेमा यसै इमेल मार्फत महानगरपालिकाको शिक्षा विभागमा अनुरोध गरी विवरण अद्यावधिक (अपडेट) गर्ने ।
                </li>
                <li>२) अनलाईनमा तपाईले पेस गरेको विवरणको अवस्था बारे जानकारी लिन युजर <b>{beData.SEESymbolNo} {beData.Alphabet}</b> र पासवोर्ड <span style=""border-bottom: 2px dashed #000000;padding-left: 20px;padding-right: 20px;""><b>{pwd}</b> </span> प्रयोग गर्नुहोला।</li>
                <li>३) विज्ञले फाराम रुजु यकिन गर्दा तोकिए बमोजिम सबै विवरण एवम् कागजात मिलेमा मात्र Confirmation इमेल आउने त्यसपछी विवरण सच्याउन नसकिने।</li>
                <li>४) विज्ञले फाराम रुजु यकिन गर्दा तोकिए बमोजिम विवरण वा कागजात नमिलेमा इमेल मार्फत पुरा गर्न भनिएका विवरण कागज प्रमाण अविलम्ब अपडेट गर्ने ।</li>
                <li>५) Confirmation इमेल आए पछी मात्र छात्रवृत्ति छनोट परीक्षा दिन पाइने भएकाले इमेल नियमित चेक गर्ने ।</li>
                <li>६) Confirmation इमेल पाएपछी २०८१।०४।२ गतेपछी प्रवेशपत्र डाउनलोड गर्ने र तोकिएको परीक्षा केन्द्रमा २०८१ साउन ०४ गते हुने परीक्षामा सहभागी हुने ।</li>

                <li>७) नियमित रूपमा विभागबाट प्राप्त हुने ईमेल समेत चेक गर्ने ।</li>
            </ul>

            <p style=""margin-top:15px"">छात्रवृत्ति छनोट समिति</p>
            <p>वीरगंज महानगरपालिका</p>
            <p>वीरगंज, पर्सा</p>
        </div>";

                mailDet.EntityId = 0;
                var global = new PivotalERP.Global.GlobalFunction(1, hostName, dbName, "");
                var resVal = global.SendEMail(mailDet);
            }
            catch (Exception ee)
            {

            }
        }


        private string ToAlpha(int sno)
        {
            return Dynamic.ReportEngine.RDL.VBFunctions.ToAlpha(sno) + ". ";
        }
        [HttpPost]
        public JsonNetResult getDocVerifyText(AcademicLib.BE.Scholarship.ScholarshipDocVerify doc)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string msg = $@"<p>श्री <span style=""padding-left: 21px;""><b>{doc.ScholarshipDet.FullName} </b></span></p>
            <p>SEE Symbol No.  <span style=""padding-left: 5px;""> <b style=""border-bottom: 2px dashed #000000; padding-right: 5px;"">: {doc.ScholarshipDet.SEESymbolNo} </b></span> </p>
            <p>Date of Birth <span style=""padding-left: 32px;""><b style=""border-bottom: 2px dashed #000000; padding-right: 5px;"">: {doc.ScholarshipDet.DOBMiti}</b></span></p> 
           
            <p style=""margin-top:14px"">
                तपाईंले वीरगंज  महानगरपालिकाबाट सूचना गरिएबमोजिम वीरगंज  महानगरपालिकाभित्रका  <span style=""border-bottom: 2px dashed #000000;padding-left: 20px;padding-right: 20px;""> <b>{doc.ScholarshipDet.SchoolPriorityListColl.Count.ToString()} </b> </span> ओटा संस्थागत शिक्षण संस्थामा कक्षा  का लागि प्रदान गरिने 
                <span style=""border-bottom: 2px dashed #000000;padding-left: 5px;padding-right: 5px;""> <b>{doc.ScholarshipDet.SubjectName} </b> </span> विषयको छात्रवृत्तिमा दिएको आवेदन सम्बन्धमा रुजु गर्दा निम्नानुसारको गर्नुहुन अनुरोध छ ।     
           </p> ";

                if (doc.V_Status == 1)
                {
                    msg = msg + $@"<ul style=""list-style: none;padding-left: 15px;"">
                 <li>
                     १)  तपाईंको आवेदन फाराम र संलग्न कागजात रुजु गर्दा देहायको अवस्था पाइएकाले <span style=""padding-left: 5px;padding-right: 5px;""><b>Under Verified </b> </span> अवस्थामा रहेको छ । मिति <span style=""padding-left: 5px;padding-right: 5px;""><b>{doc.ScholarshipDet.DayThree} </b> </span> बजेभित्र दिइएको लिङ्क प्रयोग गरी <b> ";

                    int sno = 1;
                    if (doc.V_Photo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " फोटो";
                        sno++;
                    }

                    if (doc.V_Signature)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " हस्ताक्षर";
                        sno++;
                    }
                    if (doc.V_Document)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + "जन्मदर्ता प्रमाणपत्र वा नागरिकता कागजात";
                        sno++;
                    }
                    if (doc.V_CandidateName)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " आवेदकको नाम";
                        sno++;
                    }
                    if (doc.V_Gender)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " लिङ्ग";
                        sno++;
                    }
                    if (doc.V_DOB)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " जन्म मिति";
                        sno++;
                    }
                    if (doc.V_FatherName)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " बुवाको नाम";
                        sno++;
                    }
                    if (doc.V_MotherName)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " आमाको नाम";
                        sno++;
                    }
                    if (doc.V_GrandfatherName)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " हजुरबुबाको नाम";
                        sno++;
                    }
                    if (doc.V_Email)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " इमेल";
                        sno++;
                    }
                    if (doc.V_MobileNo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " मोबाइल नम्बर";
                        sno++;
                    }
                    if (doc.V_PAddress)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " स्थायी ठेगाना";
                        sno++;
                    }
                    if (doc.V_TempAddress)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " अस्थायी ठेगाना";
                        sno++;
                    }
                    if (doc.V_BCCNo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता नं";
                        sno++;
                    }
                    if (doc.V_BCCIssuedDate)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी मिति";
                        sno++;
                    }
                    if (doc.V_BCCIssuedDistrict)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी जिल्ला";
                        sno++;
                    }

                    if (doc.V_BCCIssuedLocalLevel)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र  जारी स्थानीय तह";
                        sno++;
                    }


                    if (doc.V_SchoolName)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " विद्यालयको नाम";
                        sno++;
                    }
                    if (doc.V_SchoolType)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " विद्यालयको प्रकार";
                        sno++;
                    }
                    if (doc.V_SchoolDistrict)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " विद्यालयको जिल्ला";
                        sno++;
                    }
                    if (doc.V_SchoolLocalLevel)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " विद्यालयको स्थानीय तह";
                        sno++;
                    }
                    if (doc.V_Character_Transfer_Certi)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र";
                        sno++;
                    }
                    if (doc.V_Character_Transfer_CertiDate)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र जारी मिति";
                        sno++;
                    }
                    if (doc.V_ScholarshipType)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " छात्रवृत्तिको प्रकार";
                        sno++;
                    }
                    if (doc.V_GovSchoolCertiPath)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र";
                        sno++;
                    }
                    if (doc.V_GovSchoolCertiMiti)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र जारी मिति ";
                        sno++;
                    }
                    if (doc.V_GovSchoolCerti_RefNo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र चलानी नं";
                        sno++;
                    }
                    if (doc.V_Anusuchi3DocPath)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र";
                        sno++;
                    }
                    if (doc.V_Anusuchi3Doc_IssuedMiti)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र जारी मिति";
                        sno++;
                    }
                    if (doc.V_Anusuchi3Doc_RefNo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र चलानी नं";
                        sno++;
                    }
                    if (doc.V_MigDocPath)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " वीरगंज महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र ";
                        sno++;
                    }
                    if (doc.V_Mig_WardId)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा नं";
                        sno++;
                    }
                    if (doc.V_MigDoc_IssuedMiti)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र जारी मिति";
                        sno++;
                    }
                    if (doc.V_MigDoc_RefNo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र चलानी नं";
                        sno++;
                    }
                    // if (doc.V_LandFillDocPath)
                    // {
                    //     msg = msg + "<br>" + ToAlpha(sno) + "ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र";
                    //     sno++;
                    // }
                    // if (doc.V_LandfilDistrict)
                    // {
                    //     msg = msg + "<br>" + ToAlpha(sno) + " ल्याण्डफिल्ड साइड प्रभावित जिल्ला";
                    //     sno++;
                    // }
                    // if (doc.V_LandfillLocalLevel)
                    // {
                    //     msg = msg + "<br>" + ToAlpha(sno) + " ल्याण्डफिल्ड साइड प्रभावित स्थानीय तह";
                    //     sno++;
                    // }
                    // if (doc.V_LandfillWardNo)
                    // {
                    //     msg = msg + "<br>" + ToAlpha(sno) + " ल्याण्डफिल्ड साइड वडा नं";
                    //     sno++;
                    // }
                    // if (doc.V_LandfillDoc_IssuedMiti)
                    // {
                    //     msg = msg + "<br>" + ToAlpha(sno) + " ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र जारी मिति";
                    //     sno++;
                    // }
                    // if (doc.V_LandFill_RefNo)
                    // {
                    //     msg = msg + "<br>" + ToAlpha(sno) + " ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र चलानी नं";
                    //     sno++;
                    // }

                    //Added By Suresh on 2 Shrawan
                    if (doc.V_Gradesheet_Certi)
                    {
                        if(doc.ScholarshipDet.ClassId==1)
                            msg = msg + "<br>" + ToAlpha(sno) + " कक्षा 10 को ग्रेड शीट";
                        else
                            msg = msg + "<br>" + ToAlpha(sno) + " कक्षा ८ को ग्रेड शीट";

                        sno++;
                    }

                    if (doc.V_RelatedSchoolFilePath)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र";
                        sno++;
                    }
                    if (doc.V_RelatedSchoolIssueMiti)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र जारी मिति";
                        sno++;
                    }
                    if (doc.V_RelatedSchoolRefNo)
                    {
                        msg = msg + "<br>" + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र चलानी नं";
                        sno++;
                    }
                    //Ends

                    msg = msg + "<br>";

                    if (doc.ReservationGroupList != null)
                    {
                        foreach (var v in doc.ReservationGroupList)
                        {
                            string gname = "";
                            string aname = "";
                            var findRS = doc.ScholarshipDet.ReservationGroupList.Find(p1 => p1.ReservationGroupId == v.ReservationGroupId);
                            if (findRS != null)
                            {
                                gname = findRS.ReservationGroupName;
                                aname = findRS.ConcernedAuthorityName;
                            }

                            if (v.V_ConcernedAuthorityId)
                            {
                                // msg = msg + "<br>" + ToAlpha(sno) + " आरक्षण समूहको सरोकारवाला निकाय";
                                msg = msg + "<br>" + ToAlpha(sno) + gname + " " + aname;
                                sno++;
                            }

                            if (v.V_GroupWiseCertiMiti)
                            {
                                msg = msg + "<br>" + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी मिति";
                                sno++;
                            }

                            if (v.V_GroupWiseCerti_Path)
                            {
                                msg = msg + "<br>" + ToAlpha(sno) + gname + " समूह सिफारिसपत्र";
                                sno++;
                            }

                            if (v.V_GroupWiseCerti_RefNo)
                            {
                                msg = msg + "<br>" + ToAlpha(sno) + gname + " समूह सिफारिसपत्र चलानी नं";
                                sno++;
                            }

                            if (v.V_GrpCerti_IssuedDistrict)
                            {
                                msg = msg + "<br>" + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी जिल्ला";
                                sno++;
                            }

                            if (v.V_ISSUED_LOCALLEVEL)
                            {
                                msg = msg + "<br>" + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी स्थानीय तह";
                                sno++;
                            }
                        }
                    }

                    msg = msg + $@"</b> सहि विवरण भरी <b>Submit </b> गर्नुहोला । तोकिएको समयावधिभित्र विवरण अपलोड नगरेमा र विवरण सहि साँचो नभरेमा  तपाईंको फाराम अस्वीकृत गरिने जानकारी गराइन्छ । 

                 </li>
                 <li>२) तपाईंले आवेदन फाराममा भरेको विवरण र संलग्न कागजात सहितको जानकारीका लागि यहाँ <a href=""https://scholarship.birgunjmun.gov.np""> click </a> गर्नुहोला ।

                 </li>                 
             </ul>";

                }
                else if (doc.V_Status == 2)
                {
                    msg = msg + $@"<ul style=""list-style: none;padding-left: 15px;"">
                            <li>
                                १)  तपाईंको आवेदन फाराम <span style=""border-bottom: 2px dashed #000000;padding-left: 5px;padding-right: 5px;""><b>स्वीकृत (verified) </b> </span> भएको छ ।   
                            </li>
                            <li>२) मिति <b> २०८१/०४/०४ </b> गते शुक्रबार बिहान  <b>८:०० बजे </b> </span> देखि सञ्चालन हुने परीक्षामा <b>२०८१/०४/०२ </b>  गतेपश्चात प्रवेशपत्र डाउनलोड गरी परीक्षामा सहभागी हुनुहोला । 
                            </li>
                            <li>३)  तपाईंले आवेदन फाराममा भरेको विवरण र संलग्न कागजात सहितको जानकारीका लागि यहाँ <a href=""https://scholarship.birgunjmun.gov.np""> click </a> गर्नुहोला । 
                            </li>
                            <li>४) परीक्षामा प्रयोग हुने <b>OMR sheet</b> को नमूनाका लागि यहाँ <a href=""https://scholarship.birgunjmun.gov.np/sample/OMR Sheet.pdf""><b></b>Click </a> गरी हेर्नुहोला ।</li>
                
                        </ul>";


                }
                else if (doc.V_Status == 3)
                {
                    msg = msg + $@"<ul style=""list-style: none;padding-left: 15px;"">
                 <li>
                    १. तपाईंको आवेदन फाराम देहायको कारणबाट अस्वीकृत भएको छ : 
                    <div>
                        <input type=""checkbox"" id=""upload"" name=""upload"" value=""upload"">
                        <label for=""upload""> तोकिएको समयावधिभित्र सहि विवरण अपलोड नगरेको</label>
                    </div>
                    <div>
                        <input type=""checkbox"" id=""upload1"" name=""upload1"" value=""upload1"">
                        <label for=""upload1""> विवरण भरेको भएतापनि सहि साँचो नभरेको  
                        </label>
                    </div>

                    <div>
                        <input type=""checkbox"" id=""upload2"" name=""upload2"" value=""upload2"">
                        <label for=""upload2"">गलत कागजात पेश गरेको   
                        </label>
                    </div>

                    <div>
                        <input type=""checkbox"" id=""upload3"" name=""upload3"" value=""upload3"">
                        <label for=""upload3"">कागजात र भरेको विवरण नमिलेको </label>
                    </div>

                    <div>
                        <input type=""checkbox"" id=""upload4"" name=""upload4"" value=""upload4"">
                        <label for=""upload4""> छात्रवृत्तिका लागि आवेदन पेश गर्न नमिल्ने देखिएको 
                        </label>
                    </div>
                    <div>
                        <input type=""checkbox"" id=""upload5"" name=""upload5"" value=""upload5"">
                        <label for=""upload5""> जथाभावी विवरण भरी आवेदन दिएको  
                        </label>
                    </div>

                    ";

                    if (doc.V_Photo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">फोटो</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }

                    if (doc.V_Signature)
                    {

                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">हस्ताक्षर</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Document)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">जन्मदर्ता प्रमाणपत्र वा नागरिकता कागजात</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_CandidateName)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">आवेदकको नाम</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Gender)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">लिङ्ग</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_DOB)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">जन्म मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_FatherName)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">बुवाको नाम</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_MotherName)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">आमाको नाम</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_GrandfatherName)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">हजुरबुबाको नाम</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Email)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">इमेल</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_MobileNo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">मोबाइल नम्बर</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_PAddress)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">स्थायी ठेगाना</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_TempAddress)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">अस्थायी ठेगाना</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_BCCNo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">जन्मदर्ता प्रमाणपत्र वा नागरिकता नं</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_BCCIssuedDate)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_BCCIssuedDistrict)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी जिल्ला</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }

                    if (doc.V_BCCIssuedLocalLevel)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">जन्मदर्ता प्रमाणपत्र  जारी स्थानीय तह</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }


                    if (doc.V_SchoolName)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">विद्यालयको नाम</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_SchoolType)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">विद्यालयको प्रकार</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_SchoolDistrict)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">विद्यालयको जिल्ला</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_SchoolLocalLevel)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">विद्यालयको स्थानीय तह</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Character_Transfer_Certi)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Character_Transfer_CertiDate)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र जारी मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_ScholarshipType)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">छात्रवृत्तिको प्रकार</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_GovSchoolCertiPath)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_GovSchoolCertiMiti)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र जारी मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_GovSchoolCerti_RefNo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र चलानी नं</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Anusuchi3DocPath)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Anusuchi3Doc_IssuedMiti)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र जारी मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Anusuchi3Doc_RefNo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र चलानी नं</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_MigDocPath)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_Mig_WardId)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">वीरगंज  महानगरपालिकाको सम्बन्धित वडा नं</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_MigDoc_IssuedMiti)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र जारी मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    if (doc.V_MigDoc_RefNo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र चलानी नं</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    // if (doc.V_LandFillDocPath)
                    // { 
                    //     msg = msg + @"<div>
                    //         <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                    //         <label for=""upload6""><span style=""border-bottom: 1px dashed;"">ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडा</span>   नभएको वा नमिलेको 
                    //         </label>
                    //         </div>";
                    // }
                    // if (doc.V_LandfilDistrict)
                    // { 
                    //     msg = msg + @"<div>
                    //         <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                    //         <label for=""upload6""><span style=""border-bottom: 1px dashed;"">ल्याण्डफिल्ड साइड प्रभावित जिल्ला</span>   नभएको वा नमिलेको 
                    //         </label>
                    //         </div>";
                    // }
                    // if (doc.V_LandfillLocalLevel)
                    // { 
                    //     msg = msg + @"<div>
                    //         <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                    //         <label for=""upload6""><span style=""border-bottom: 1px dashed;"">ल्याण्डफिल्ड साइड प्रभावित स्थानीय तह</span>   नभएको वा नमिलेको 
                    //         </label>
                    //         </div>";
                    // }
                    // if (doc.V_LandfillWardNo)
                    // { 
                    //     msg = msg + @"<div>
                    //         <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                    //         <label for=""upload6""><span style=""border-bottom: 1px dashed;"">ल्याण्डफिल्ड साइड वडा नं</span>   नभएको वा नमिलेको 
                    //         </label>
                    //         </div>";
                    // }
                    // if (doc.V_LandfillDoc_IssuedMiti)
                    // {                        
                    //     msg = msg + @"<div>
                    //         <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                    //         <label for=""upload6""><span style=""border-bottom: 1px dashed;"">ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र जारी मिति</span>   नभएको वा नमिलेको 
                    //         </label>
                    //         </div>";
                    // }
                    // if (doc.V_LandFill_RefNo)
                    // { 
                    //     msg = msg + @"<div>
                    //         <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                    //         <label for=""upload6""><span style=""border-bottom: 1px dashed;"">ल्याण्डफिल्ड साइड प्रभावित क्षेत्रको विद्यार्थीका लागि सम्बन्धित वडाको सिफारिस पत्र चलानी नं</span>   नभएको वा नमिलेको 
                    //         </label>
                    //         </div>";
                    // }

                    //Added By Suresh on 2 Shrawan starts
                    if (doc.V_Gradesheet_Certi)
                    {
                        if (doc.ScholarshipDet.ClassId == 1)
                        {
                            msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">कक्षा 10 को  ग्रेड शीट</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                        }
                        else
                        {
                            msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">कक्षा ८ को  ग्रेड शीट</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                        }
                        
                    }

                    if (doc.V_RelatedSchoolFilePath)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">सम्बन्धित विद्यालयको सिफारिसपत्र </span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }

                    if (doc.V_RelatedSchoolIssueMiti)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">सम्बन्धित विद्यालयको सिफारिसपत्र जारी मिति </span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }

                    if (doc.V_RelatedSchoolRefNo)
                    {
                        msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;"">सम्बन्धित विद्यालयको सिफारिसपत्र चलानी नं </span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                    }
                    //Ends

                    if (doc.ReservationGroupList != null)
                    {
                        foreach (var v in doc.ReservationGroupList)
                        {
                            string gname = "";
                            string aname = "";
                            var findRS = doc.ScholarshipDet.ReservationGroupList.Find(p1 => p1.ReservationGroupId == v.ReservationGroupId);
                            if (findRS != null)
                            {
                                gname = findRS.ReservationGroupName;
                                aname = findRS.ConcernedAuthorityName;
                            }



                            if (v.V_GroupWiseCertiMiti)
                            {

                                msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;""> समूह सिफारिसपत्र जारी मिति</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                            }

                            if (v.V_GroupWiseCerti_Path)
                            {
                                msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;""> समूह सिफारिसपत्र</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                            }

                            if (v.V_GroupWiseCerti_RefNo)
                            {
                                msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;""> समूह सिफारिसपत्र चलानी नं</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                            }

                            if (v.V_GrpCerti_IssuedDistrict)
                            {

                                msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;""> समूह सिफारिसपत्र जारी जिल्ला</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                            }

                            if (v.V_ISSUED_LOCALLEVEL)
                            {
                                msg = msg + @"<div>
                            <input type=""checkbox"" id=""upload6"" name=""upload6"" value=""upload6"">
                            <label for=""upload6""><span style=""border-bottom: 1px dashed;""> समूह सिफारिसपत्र जारी स्थानीय तह</span>   नभएको वा नमिलेको 
                            </label>
                            </div>";
                            }
                        }
                    }

                    msg = msg + @"
                    <div>
                        <input type=""checkbox"" id=""upload7"" name=""upload7"" value=""upload7"">
                        <label for=""upload7"">अन्य <span style=""border-bottom: 1px dashed;"">.....</span>  
                        </label>
                    </div>
                 </li>
                 <li>२. यदि तपाईंसँग फाराम अस्वीकृत भएको व्यहोराबमोजिम कागजात भएमा scholarship.birgunjmun@gmail.com मा  इमेलमार्फत अनुरोध गर्नुहोला ।   
                 </li>
                 <li>३. तपाईंले आवेदन फाराममा भरेको विवरण र संलग्न कागजात सहितको जानकारीका लागि यहाँ <a href=""https://scholarship.birgunjmun.gov.np""> click </a> गर्नुहोला । </li>                 
             </ul>";


                }

                msg = msg + $@"<p style=""margin-top:15px"">छात्रवृत्ति छनोट समिति</p>
            <p>शैक्षिक प्रशासन महाशाखा, वीरगंज  महानगरपालिका</p>
            ";

                resVal.IsSuccess = true;
                resVal.ResponseMSG = msg;
                resVal.ResponseId = getDocVerifySMSText(doc);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
         
        private string getDocVerifySMSText(AcademicLib.BE.Scholarship.ScholarshipDocVerify doc)
        {
           
            string msg = "";
            if (doc.V_Status == 1)
            {
                msg = $"तपाई {doc.ScholarshipDet.FirstName} ले कक्षा {doc.ScholarshipDet.ClassName} को {doc.ScholarshipDet.SubjectName} विषयका छात्रवृत्ति आवेदन रुजु गर्दा निम्न त्रुटि रहेको छ । ";
                
                int sno = 1;
                if (doc.V_Photo)
                {
                    msg = msg + ToAlpha(sno) + " फोटो";
                    sno++;
                }

                if (doc.V_Signature)
                {
                    msg = msg + ToAlpha(sno) + " हस्ताक्षर";
                    sno++;
                }
                if (doc.V_Document)
                {
                    msg = msg + ToAlpha(sno) + "जन्मदर्ता प्रमाणपत्र वा नागरिकता कागजात";
                    sno++;
                }
                if (doc.V_CandidateName)
                {
                    msg = msg + ToAlpha(sno) + " आवेदकको नाम";
                    sno++;
                }
                if (doc.V_Gender)
                {
                    msg = msg + ToAlpha(sno) + " लिङ्ग";
                    sno++;
                }
                if (doc.V_DOB)
                {
                    msg = msg + ToAlpha(sno) + " जन्म मिति";
                    sno++;
                }
                if (doc.V_FatherName)
                {
                    msg = msg + ToAlpha(sno) + " बुवाको नाम";
                    sno++;
                }
                if (doc.V_MotherName)
                {
                    msg = msg + ToAlpha(sno) + " आमाको नाम";
                    sno++;
                }
                if (doc.V_GrandfatherName)
                {
                    msg = msg + ToAlpha(sno) + " हजुरबुबाको नाम";
                    sno++;
                }
                if (doc.V_Email)
                {
                    msg = msg + ToAlpha(sno) + " इमेल";
                    sno++;
                }
                if (doc.V_MobileNo)
                {
                    msg = msg + ToAlpha(sno) + " मोबाइल नम्बर";
                    sno++;
                }
                if (doc.V_PAddress)
                {
                    msg = msg +  ToAlpha(sno) + " स्थायी ठेगाना";
                    sno++;
                }
                if (doc.V_TempAddress)
                {
                    msg = msg +  ToAlpha(sno) + " अस्थायी ठेगाना";
                    sno++;
                }
                if (doc.V_BCCNo)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता नं";
                    sno++;
                }
                if (doc.V_BCCIssuedDate)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी मिति";
                    sno++;
                }
                if (doc.V_BCCIssuedDistrict)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी जिल्ला";
                    sno++;
                }

                if (doc.V_BCCIssuedLocalLevel)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र  जारी स्थानीय तह";
                    sno++;
                }


                if (doc.V_SchoolName)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको नाम";
                    sno++;
                }
                if (doc.V_SchoolType)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको प्रकार";
                    sno++;
                }
                if (doc.V_SchoolDistrict)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको जिल्ला";
                    sno++;
                }
                if (doc.V_SchoolLocalLevel)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको स्थानीय तह";
                    sno++;
                }
                if (doc.V_Character_Transfer_Certi)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र";
                    sno++;
                }
                if (doc.V_Character_Transfer_CertiDate)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_ScholarshipType)
                {
                    msg = msg + ToAlpha(sno) + " छात्रवृत्तिको प्रकार";
                    sno++;
                }
                if (doc.V_GovSchoolCertiPath)
                {
                    msg = msg + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र";
                    sno++;
                }
                if (doc.V_GovSchoolCertiMiti)
                {
                    msg = msg + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र जारी मिति ";
                    sno++;
                }
                if (doc.V_GovSchoolCerti_RefNo)
                {
                    msg = msg + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र चलानी नं";
                    sno++;
                }
                if (doc.V_Anusuchi3DocPath)
                {
                    msg = msg + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र";
                    sno++;
                }
                if (doc.V_Anusuchi3Doc_IssuedMiti)
                {
                    msg = msg + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_Anusuchi3Doc_RefNo)
                {
                    msg = msg + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र चलानी नं";
                    sno++;
                }
                if (doc.V_MigDocPath)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र ";
                    sno++;
                }
                if (doc.V_Mig_WardId)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा नं";
                    sno++;
                }
                if (doc.V_MigDoc_IssuedMiti)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_MigDoc_RefNo)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र चलानी नं";
                    sno++;
                }


                //Added By Suresh on 2 Shrawan
                if (doc.V_Gradesheet_Certi)
                {
                    if (doc.ScholarshipDet.ClassId == 1)
                        msg = msg + ToAlpha(sno) + " कक्षा 10 को ग्रेड शीट";
                    else
                        msg = msg + ToAlpha(sno) + " कक्षा ८ को ग्रेड शीट";

                    sno++;
                }

                if (doc.V_RelatedSchoolFilePath)
                {
                    msg = msg + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र";
                    sno++;
                }
                if (doc.V_RelatedSchoolIssueMiti)
                {
                    msg = msg + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_RelatedSchoolRefNo)
                {
                    msg = msg + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र चलानी नं";
                    sno++;
                }
                //Ends



                if (doc.ReservationGroupList != null)
                {
                    foreach (var v in doc.ReservationGroupList)
                    {
                        string gname = "";
                        string aname = "";
                        var findRS = doc.ScholarshipDet.ReservationGroupList.Find(p1 => p1.ReservationGroupId == v.ReservationGroupId);
                        if (findRS != null)
                        {
                            gname = findRS.ReservationGroupName;
                            aname = findRS.ConcernedAuthorityName;
                        }

                        if (v.V_ConcernedAuthorityId)
                        {
                            msg = msg + ToAlpha(sno) + gname + " " + aname;
                            sno++;
                        }

                        if (v.V_GroupWiseCertiMiti)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी मिति";
                            sno++;
                        }

                        if (v.V_GroupWiseCerti_Path)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र";
                            sno++;
                        }

                        if (v.V_GroupWiseCerti_RefNo)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र चलानी नं";
                            sno++;
                        }

                        if (v.V_GrpCerti_IssuedDistrict)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी जिल्ला";
                            sno++;
                        }

                        if (v.V_ISSUED_LOCALLEVEL)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी स्थानीय तह";
                            sno++;
                        }
                    }
                }

                msg = msg + " उक्त त्रुटि मिति 2081-05-10 गते भित्र सच्याउनु होला अन्यथा फाराम Reject हुनेछ । -वीरगंज महानगरपालिका ";

            }
            else if (doc.V_Status == 2)
            {
                msg= $"तपाई {doc.ScholarshipDet.FirstName} ले कक्षा {doc.ScholarshipDet.ClassName} को {doc.ScholarshipDet.SubjectName} विषयका लागि पेश गरेको छात्रवृत्तिको फाराम स्वीकृत भयो ।  -वीरगंज महानगरपालिका";
            }
            else if (doc.V_Status == 3)
            {
                msg = $"तपाई {doc.ScholarshipDet.FirstName} ले कक्षा {doc.ScholarshipDet.ClassName} को {doc.ScholarshipDet.SubjectName} विषयका लागि पेश गरेको छात्रवृत्तिको फाराम अस्वीकृत भयो ।  -वीरगंज महानगरपालिका";
            }
            return msg;
             
        }


        private string getDocVerifyTextForSMS(AcademicLib.BE.Scholarship.ScholarshipDocVerify doc)
        {
            string msg = "";
            if (doc.V_Status == 1)
            {

                int sno = 1;
                if (doc.V_Photo)
                {
                    msg = msg + ToAlpha(sno) + " फोटो";
                    sno++;
                }

                if (doc.V_Signature)
                {
                    msg = msg + ToAlpha(sno) + " हस्ताक्षर";
                    sno++;
                }
                if (doc.V_Document)
                {
                    msg = msg + ToAlpha(sno) + "जन्मदर्ता प्रमाणपत्र वा नागरिकता कागजात";
                    sno++;
                }
                if (doc.V_CandidateName)
                {
                    msg = msg + ToAlpha(sno) + " आवेदकको नाम";
                    sno++;
                }
                if (doc.V_Gender)
                {
                    msg = msg + ToAlpha(sno) + " लिङ्ग";
                    sno++;
                }
                if (doc.V_DOB)
                {
                    msg = msg + ToAlpha(sno) + " जन्म मिति";
                    sno++;
                }
                if (doc.V_FatherName)
                {
                    msg = msg + ToAlpha(sno) + " बुवाको नाम";
                    sno++;
                }
                if (doc.V_MotherName)
                {
                    msg = msg + ToAlpha(sno) + " आमाको नाम";
                    sno++;
                }
                if (doc.V_GrandfatherName)
                {
                    msg = msg + ToAlpha(sno) + " हजुरबुबाको नाम";
                    sno++;
                }
                if (doc.V_Email)
                {
                    msg = msg + ToAlpha(sno) + " इमेल";
                    sno++;
                }
                if (doc.V_MobileNo)
                {
                    msg = msg + ToAlpha(sno) + " मोबाइल नम्बर";
                    sno++;
                }
                if (doc.V_PAddress)
                {
                    msg = msg +  ToAlpha(sno) + " स्थायी ठेगाना";
                    sno++;
                }
                if (doc.V_TempAddress)
                {
                    msg = msg +  ToAlpha(sno) + " अस्थायी ठेगाना";
                    sno++;
                }
                if (doc.V_BCCNo)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता नं";
                    sno++;
                }
                if (doc.V_BCCIssuedDate)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी मिति";
                    sno++;
                }
                if (doc.V_BCCIssuedDistrict)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र वा नागरिकता जारी जिल्ला";
                    sno++;
                }

                if (doc.V_BCCIssuedLocalLevel)
                {
                    msg = msg + ToAlpha(sno) + " जन्मदर्ता प्रमाणपत्र  जारी स्थानीय तह";
                    sno++;
                }


                if (doc.V_SchoolName)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको नाम";
                    sno++;
                }
                if (doc.V_SchoolType)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको प्रकार";
                    sno++;
                }
                if (doc.V_SchoolDistrict)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको जिल्ला";
                    sno++;
                }
                if (doc.V_SchoolLocalLevel)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयको स्थानीय तह";
                    sno++;
                }
                if (doc.V_Character_Transfer_Certi)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र";
                    sno++;
                }
                if (doc.V_Character_Transfer_CertiDate)
                {
                    msg = msg + ToAlpha(sno) + " विद्यालयले जारी गरेको चारित्रिक वा स्थानान्तरण प्रमाणपत्र वा सिफारिसपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_ScholarshipType)
                {
                    msg = msg + ToAlpha(sno) + " छात्रवृत्तिको प्रकार";
                    sno++;
                }
                if (doc.V_GovSchoolCertiPath)
                {
                    msg = msg + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र";
                    sno++;
                }
                if (doc.V_GovSchoolCertiMiti)
                {
                    msg = msg + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र जारी मिति ";
                    sno++;
                }
                if (doc.V_GovSchoolCerti_RefNo)
                {
                    msg = msg + ToAlpha(sno) + " शिक्षा विकास तथा समन्वय इकाई वा स्थानीय तहबाट सामुदायिक विद्यालय प्रमाणित प्रमाणपत्र चलानी नं";
                    sno++;
                }
                if (doc.V_Anusuchi3DocPath)
                {
                    msg = msg + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र";
                    sno++;
                }
                if (doc.V_Anusuchi3Doc_IssuedMiti)
                {
                    msg = msg + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_Anusuchi3Doc_RefNo)
                {
                    msg = msg + ToAlpha(sno) + " संस्थागत विद्यालयमा छात्रवृत्तिमा अध्ययन गरेको भनी अनुसूची - २ बमोजिमको ढाँचामा विद्यालयको सिफारिसपत्र चलानी नं";
                    sno++;
                }
                if (doc.V_MigDocPath)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र ";
                    sno++;
                }
                if (doc.V_Mig_WardId)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा नं";
                    sno++;
                }
                if (doc.V_MigDoc_IssuedMiti)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_MigDoc_RefNo)
                {
                    msg = msg + ToAlpha(sno) + " वीरगंज  महानगरपालिकाको सम्बन्धित वडा कार्यालयबाट जारी भएको जन्मदर्ता वा बसाईसराईको प्रमाणपत्र वा जिल्ला प्रशासन कार्यालयबाट वीरगंज  महानगरपालिका अन्तर्गत स्थायी ठेगाना भएको नागरिकताको प्रमाणपत्र चलानी नं";
                    sno++;
                }


                //Added By Suresh on 2 Shrawan
                if (doc.V_Gradesheet_Certi)
                {
                    if(doc.ScholarshipDet.ClassId==1)
                        msg = msg + ToAlpha(sno) + " कक्षा 10 को ग्रेड शीट";
                    else
                        msg = msg + ToAlpha(sno) + " कक्षा ८ को ग्रेड शीट";

                    sno++;
                }

                if (doc.V_RelatedSchoolFilePath)
                {
                    msg = msg + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र";
                    sno++;
                }
                if (doc.V_RelatedSchoolIssueMiti)
                {
                    msg = msg + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र जारी मिति";
                    sno++;
                }
                if (doc.V_RelatedSchoolRefNo)
                {
                    msg = msg + ToAlpha(sno) + " सम्बन्धित विद्यालयको सिफारिसपत्र चलानी नं";
                    sno++;
                }
                //Ends



                if (doc.ReservationGroupList != null)
                {
                    foreach (var v in doc.ReservationGroupList)
                    {
                        string gname = "";
                        string aname = "";
                        var findRS = doc.ScholarshipDet.ReservationGroupList.Find(p1 => p1.ReservationGroupId == v.ReservationGroupId);
                        if (findRS != null)
                        {
                            gname = findRS.ReservationGroupName;
                            aname = findRS.ConcernedAuthorityName;
                        }

                        if (v.V_ConcernedAuthorityId)
                        {
                            msg = msg + ToAlpha(sno) + gname + " " + aname;
                            sno++;
                        }

                        if (v.V_GroupWiseCertiMiti)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी मिति";
                            sno++;
                        }

                        if (v.V_GroupWiseCerti_Path)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र";
                            sno++;
                        }

                        if (v.V_GroupWiseCerti_RefNo)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र चलानी नं";
                            sno++;
                        }

                        if (v.V_GrpCerti_IssuedDistrict)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी जिल्ला";
                            sno++;
                        }

                        if (v.V_ISSUED_LOCALLEVEL)
                        {
                            msg = msg + ToAlpha(sno) + gname + " समूह सिफारिसपत्र जारी स्थानीय तह";
                            sno++;
                        }
                    }
                }

            }
            return msg;
        }
        private void SendVerifyEmail(AcademicLib.BE.Scholarship.ScholarshipDocVerify beData)
        {
            try
            {
                Dynamic.BusinessEntity.Global.MailDetails mailDet = new Dynamic.BusinessEntity.Global.MailDetails();
                mailDet.To = beData.Email;
                mailDet.Subject = beData.V_Subject;
                mailDet.CUserId = 1;
                mailDet.Message = beData.Remarks;
                mailDet.EntityId = 0;
                var global = new PivotalERP.Global.GlobalFunction(1, hostName, dbName, "");
                var resVal = global.SendEMail(mailDet);
            }
            catch (Exception ee)
            {

            }
        }


        #region "ExamCenter"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.ExamCenter)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ExamCenter)]
        public JsonNetResult SaveExamCenter()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.ExamCenter>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamCenterId.HasValue)
                        beData.ExamCenterId = 0;

                    resVal = new AcademicLib.BL.Scholarship.ExamCenter(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.ExamCenter)]
        public JsonNetResult getExamCenterById(int ExamCenterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.ExamCenter(User.UserId, User.HostName, User.DBName).GetExamCenterById(0, ExamCenterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.ExamCenter)]
        public JsonNetResult DeleteExamCenter(int ExamCenterId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ExamCenterId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.ExamCenter(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, ExamCenterId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllExamCenter()
        {
            AcademicLib.BE.Scholarship.ExamCenterCollections dataColl = new AcademicLib.BE.Scholarship.ExamCenterCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.ExamCenter(1, hostName, dbName).GetAllExamCenter(0);
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


        #region "ExamCenterMapping"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.ExamCenterMapping)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ExamCenterMapping)]
        public JsonNetResult SaveExamCenterMapping()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Scholarship.ExamCenterMapping>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Scholarship.ExamCenterMapping(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Global.Actions.Modify, (int)FormsEntity.ExamCenterMapping)]
        public JsonNetResult getExamCenterMappingById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.ExamCenterMapping(User.UserId, User.HostName, User.DBName).GetExamCenterMappingById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.ExamCenterMapping)]
        public JsonNetResult DeleteExamCenterMapping(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Scholarship.ExamCenterMapping(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonNetResult GetAllExamCenterMapping()
        {
            AcademicLib.BE.Scholarship.ExamCenterMappingCollections dataColl = new AcademicLib.BE.Scholarship.ExamCenterMappingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Scholarship.ExamCenterMapping(1, hostName, dbName).GetAllExamCenterMapping(0);
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
    }
}