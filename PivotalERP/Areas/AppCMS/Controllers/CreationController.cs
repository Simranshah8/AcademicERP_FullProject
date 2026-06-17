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
namespace PivotalERP.Areas.AppCMS.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: AppCMS/Creation
        string photoLocation = "/Attachments/appcms";

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Notice, false)]
        public ActionResult Notice()
        {
            return View();
        }

        #region "Notice"

        [HttpPost] 
        public JsonNetResult GetAutoNoticeNo()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Notice(User.UserId, User.HostName, User.DBName).GetAutoNoticeNo(User.UserId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost,ValidateInput(false)]
        public JsonNetResult SaveNotice()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Notice>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;
                    
                    if (Request.Files.Count > 0)
                    {
                        beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];                       
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);                            
                            beData.ImagePath = photoDoc.DocPath;
                        }

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
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
                            fInd++;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.NoticeId.HasValue)
                        beData.NoticeId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Notice(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    try
                    {
                        if (beData.ShowInApp)
                        {
                            Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                            notification.Content = beData.Description;
                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE);
                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.NOTICE.ToString();
                            notification.Heading = beData.HeadLine;
                            notification.Subject = "Notice";
                            notification.UserId = User.UserId;
                            notification.UserName = User.UserName;
                            notification.UserIdColl = "";

                            //if(User.DBName.ToLower().StartsWith("cems"))
                            //     notification.BranchCode = User.CustomerCode;

                            if (ActiveBranch)
                                notification.BranchCode = User.CustomerCode;

                            new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification, true);
                        }
                       
                    }
                    catch (Exception ee)
                    {
                       // resVal.ResponseMSG = ee.Message;
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
        public JsonNetResult GetAllNoticeList()
        {
            var totalRows = 0;
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Notice(User.UserId, User.HostName, User.DBName).GetAllNotice(0,null,ref totalRows,1,99999,"admin");

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetNoticeById(int NoticeId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Notice(User.UserId, User.HostName, User.DBName).GetNoticeById(0, NoticeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelNotice(int NoticeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Notice(User.UserId, User.HostName, User.DBName).DeleteById(0, NoticeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Gallery, false)]
        public ActionResult Gallery()
        {
            return View();
        }
        #region "Gallery"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveGallery()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Gallery>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.ImageColl;

                    if (Request.Files.Count > 0)
                    {
                        beData.ImageColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                        var filesColl = Request.Files;
                       
                        int fInd = 0;
                        foreach (var v in filesColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.ImageColl.Add
                                    (
                                     new Dynamic.BusinessEntity.GeneralDocument()
                                     {
                                         Data = att.Data,
                                         DocPath = att.DocPath,
                                         DocumentTypeId = null,
                                         Extension = att.Extension,
                                         Name = att.Name,
                                         Description = ""
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.GalleryId.HasValue)
                        beData.GalleryId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Gallery(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllGalleryList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Gallery(User.UserId, User.HostName, User.DBName).GetAllGallery(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetGalleryById(int GalleryId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Gallery(User.UserId, User.HostName, User.DBName).GetGalleryById(0, GalleryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelGallery(int GalleryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Gallery(User.UserId, User.HostName, User.DBName).DeleteById(0, GalleryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Videos, false)]
        public ActionResult Videos()
        {
            return View();
        }
        #region "Videos"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveVideos()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Videos>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var fInd = 0;
                        foreach (var det in beData.VideosURLColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                det.ThumbnailPath = att.DocPath;
                            }
                            fInd++;
                        }
                    }
                     
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.AttachFilePath = photoDoc.DocPath;
                        }                        
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.VideosId.HasValue)
                        beData.VideosId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Videos(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllVideosList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Videos(User.UserId, User.HostName, User.DBName).GetAllVideos(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetVideosById(int VideosId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Videos(User.UserId, User.HostName, User.DBName).GetVideosById(0, VideosId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelVideos(int VideosId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Videos(User.UserId, User.HostName, User.DBName).DeleteById(0, VideosId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Slider, false)]
        public ActionResult Slider()
        {
            return View();
        }
        #region "Slider"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveSlider()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Slider>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.SliderId.HasValue)
                        beData.SliderId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Slider(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllSliderList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Slider(User.UserId, User.HostName, User.DBName).GetAllSlider(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSliderById(int SliderId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Slider(User.UserId, User.HostName, User.DBName).GetSliderById(0, SliderId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelSlider(int SliderId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Slider(User.UserId, User.HostName, User.DBName).DeleteById(0, SliderId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion


        public ActionResult Banner()
        {
            return View();
        }
        #region "Banner"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveBanner()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Banner>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.BannerId.HasValue)
                        beData.BannerId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Banner(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    try
                    {
                        Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                        notification.Content = beData.Description;
                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.BANNER);
                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.BANNER.ToString();
                        notification.Heading = beData.Title;
                        notification.Subject = "Banner";
                        notification.UserId = User.UserId;
                        notification.UserName = User.UserName;
                        notification.UserIdColl = "";
                         new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);
                    }
                    catch (Exception ee)
                    {
                        // resVal.ResponseMSG = ee.Message;
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
        public JsonNetResult GetAllBannerList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Banner(User.UserId, User.HostName, User.DBName).GetAllBanner(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBannerById(int BannerId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Banner(User.UserId, User.HostName, User.DBName).GetBannerById(0, BannerId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBanner(int BannerId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Banner(User.UserId, User.HostName, User.DBName).DeleteById(0, BannerId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion


        [PermissionsAttribute(Actions.View, (int)ENTITIES.ServiceFacilities, false)]
        public ActionResult ServicesAndFacilities()
        {
            return View();
        }
        #region "ServicesAndFacilities"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveServicesAndFacilities()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.ServicesAndFacilities>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.ServicesAndFacilities(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllServicesAndFacilitiesList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.ServicesAndFacilities(User.UserId, User.HostName, User.DBName).GetAllServicesAndFacilities(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetServicesAndFacilitiesById(int ServicesAndFacilitiesId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.ServicesAndFacilities(User.UserId, User.HostName, User.DBName).GetServicesAndFacilitiesById(0, ServicesAndFacilitiesId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelServicesAndFacilities(int ServicesAndFacilitiesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.ServicesAndFacilities(User.UserId, User.HostName, User.DBName).DeleteById(0, ServicesAndFacilitiesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        
        public ActionResult AcademicProgram()
        {
            return View();
        }
        #region "AcademicProgram"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveAcademicProgram()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AcademicProgram>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.AcademicProgram(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAcademicProgramList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AcademicProgram(User.UserId, User.HostName, User.DBName).GetAllAcademicProgram(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAcademicProgramById(int AcademicProgramId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AcademicProgram(User.UserId, User.HostName, User.DBName).GetAcademicProgramById(0, AcademicProgramId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAcademicProgram(int AcademicProgramId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.AcademicProgram(User.UserId, User.HostName, User.DBName).DeleteById(0, AcademicProgramId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion
        
        public ActionResult ExecutiveMember()
        {
            return View();
        }
        #region "ExecutiveMember"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveExecutiveMember()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.ExecutiveMembers>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.ExecutiveMembers(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllExecutiveMemberList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.ExecutiveMembers(User.UserId, User.HostName, User.DBName).GetAllExecutiveMembers(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExecutiveMemberById(int ExecutiveMemberId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.ExecutiveMembers(User.UserId, User.HostName, User.DBName).GetExecutiveMembersById(0, ExecutiveMemberId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExecutiveMember(int ExecutiveMemberId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.ExecutiveMembers(User.UserId, User.HostName, User.DBName).DeleteById(0, ExecutiveMemberId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        public ActionResult AboutUs()
        {
            return View();
        }
        #region "AboutUs"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveAboutUs()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AboutUs>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var logo = filesColl["logo"];
                        var img = filesColl["image"];
                        var banner = filesColl["banner"];
                        var affiliated = filesColl["affiliated"];
                        var schoolphoto = filesColl["school"];

                        if (logo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, logo, true);                            
                            beData.LogoPath = photoDoc.DocPath;
                        }

                        if (img != null)
                        {
                            var signatureDoc = GetAttachmentDocuments(photoLocation, img, true);                            
                            beData.ImagePath = signatureDoc.DocPath;
                        }

                        if (banner != null)
                        {
                            var signatureDoc = GetAttachmentDocuments(photoLocation, banner, true);                            
                            beData.BannerPath = signatureDoc.DocPath;
                        }

                        if (affiliated != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, affiliated, true);
                            beData.AffiliatedLogo = photoDoc.DocPath;
                        }

                        if (schoolphoto != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, schoolphoto, true);
                            beData.SchoolPhoto = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.AboutUsId.HasValue)
                        beData.AboutUsId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.AboutUs(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAboutUsList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AboutUs(User.UserId, User.HostName, User.DBName).GetAllAboutUs(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAboutUsById(int AboutUsId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AboutUs(User.UserId, User.HostName, User.DBName).GetAboutUsById(0, AboutUsId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAboutUs(int AboutUsId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.AboutUs(User.UserId, User.HostName, User.DBName).DeleteById(0, AboutUsId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        public ActionResult Introduction()
        {
            return View();
        }
        #region "VisionStatement"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveVisionStatement()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.VisionStatement>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.VisionStatementId.HasValue)
                        beData.VisionStatementId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.VisionStatement(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllVisionStatementList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.VisionStatement(User.UserId, User.HostName, User.DBName).GetAllVisionStatement(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetVisionStatementById(int VisionStatementId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.VisionStatement(User.UserId, User.HostName, User.DBName).GetVisionStatementById(0, VisionStatementId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelVisionStatement(int VisionStatementId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.VisionStatement(User.UserId, User.HostName, User.DBName).DeleteById(0, VisionStatementId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "FounderMessage"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveFounderMessage()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.FounderMessage>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.FounderMessageId.HasValue)
                        beData.FounderMessageId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.FounderMessage(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllFounderMessageList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.FounderMessage(User.UserId, User.HostName, User.DBName).GetAllFounderMessage(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFounderMessageById(int FounderMessageId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.FounderMessage(User.UserId, User.HostName, User.DBName).GetFounderMessageById(0, FounderMessageId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFounderMessage(int FounderMessageId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.FounderMessage(User.UserId, User.HostName, User.DBName).DeleteById(0, FounderMessageId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "StaffHierarchy"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveStaffHierarchy()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.StaffHierarchy>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.StaffHierarchyId.HasValue)
                        beData.StaffHierarchyId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.StaffHierarchy(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllStaffHierarchyList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.StaffHierarchy(User.UserId, User.HostName, User.DBName).GetAllStaffHierarchy(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStaffHierarchyById(int StaffHierarchyId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.StaffHierarchy(User.UserId, User.HostName, User.DBName).GetStaffHierarchyById(0, StaffHierarchyId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelStaffHierarchy(int StaffHierarchyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.StaffHierarchy(User.UserId, User.HostName, User.DBName).DeleteById(0, StaffHierarchyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion


        #region "CommitteHierarchy"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveCommitteHierarchy()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.CommitteHierarchy>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.CommitteHierarchyId.HasValue)
                        beData.CommitteHierarchyId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.CommitteHierarchy(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllCommitteHierarchyList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.CommitteHierarchy(User.UserId, User.HostName, User.DBName).GetAllCommitteHierarchy(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCommitteHierarchyById(int CommitteHierarchyId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.CommitteHierarchy(User.UserId, User.HostName, User.DBName).GetCommitteHierarchyById(0, CommitteHierarchyId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelCommitteHierarchy(int CommitteHierarchyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.CommitteHierarchy(User.UserId, User.HostName, User.DBName).DeleteById(0, CommitteHierarchyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        public ActionResult WhoWeAre()
        {
            return View();

        }
        #region "AdmissionProcedure"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveAdmissionProcedure()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AdmissionProcedure>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.AdmissionProcedureId.HasValue)
                        beData.AdmissionProcedureId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.AdmissionProcedure(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAdmissionProcedureList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AdmissionProcedure(User.UserId, User.HostName, User.DBName).GetAllAdmissionProcedure(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAdmissionProcedureById(int AdmissionProcedureId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AdmissionProcedure(User.UserId, User.HostName, User.DBName).GetAdmissionProcedureById(0, AdmissionProcedureId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAdmissionProcedure(int AdmissionProcedureId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.AdmissionProcedure(User.UserId, User.HostName, User.DBName).DeleteById(0, AdmissionProcedureId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "RulesRegulation"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveRulesRegulation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.RulesRegulation>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.RulesRegulationId.HasValue)
                        beData.RulesRegulationId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.RulesRegulation(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllRulesRegulationList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.RulesRegulation(User.UserId, User.HostName, User.DBName).GetAllRulesRegulation(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetRulesRegulationById(int RulesRegulationId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.RulesRegulation(User.UserId, User.HostName, User.DBName).GetRulesRegulationById(0, RulesRegulationId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelRulesRegulation(int RulesRegulationId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.RulesRegulation(User.UserId, User.HostName, User.DBName).DeleteById(0, RulesRegulationId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Contact"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveContact()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Contact>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            //beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.ContactId.HasValue)
                        beData.ContactId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Contact(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetContact()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Contact(User.UserId, User.HostName, User.DBName).GetContactById(0, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelContact(int ContactId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Contact(User.UserId, User.HostName, User.DBName).DeleteById(0, ContactId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.EventType, false)]
        public ActionResult EventType()
        {
            return View();
        }
        #region "EventType"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveEventType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.EventType>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.EventTypeId.HasValue)
                        beData.EventTypeId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.EventType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllEventTypeList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.EventType(User.UserId, User.HostName, User.DBName).GetAllEventType(0,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEventTypeById(int EventTypeId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.EventType(User.UserId, User.HostName, User.DBName).GetEventTypeById(0, EventTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelEventType(int EventTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.EventType(User.UserId, User.HostName, User.DBName).DeleteById(0, EventTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.AcademicCalendar, false)]
        public ActionResult AcademicCalender()
        {
            return View();
        }

        public ActionResult Weekend()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.EventType, false)]
        public ActionResult EventList()
        {
            return View();
        }
        #region "EventHoliday"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveEventHoliday()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.EventHoliday>(Request["jsonData"]);
                if (beData != null)
                {

                    beData.CUserId = User.UserId;

                    if (!beData.EventHolidayId.HasValue)
                        beData.EventHolidayId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.EventHoliday(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllEventHolidayList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.EventHoliday(User.UserId, User.HostName, User.DBName).GetAllEventHoliday(0,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEventHolidayById(int EventHolidayId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.EventHoliday(User.UserId, User.HostName, User.DBName).GetEventHolidayById(0, EventHolidayId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelEventHoliday(int EventHolidayId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.EventHoliday(User.UserId, User.HostName, User.DBName).DeleteById(0, EventHolidayId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveWeekend()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.WeekendCollections>(Request["jsonData"]);
                if (beData != null)
                {

                   foreach(var v in beData)
                    {
                        v.CUserId = User.UserId;
                    }

                    resVal = new AcademicLib.BL.AppCMS.Creation.EventHoliday(User.UserId, User.HostName, User.DBName).SaveWeekend(beData);
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
        public JsonNetResult GetWeekendList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.EventHoliday(User.UserId, User.HostName, User.DBName).getWeekend(null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion
        [HttpPost]
        public JsonNetResult GetFeedbackList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AboutUs(User.UserId, User.HostName, User.DBName).getFeedbackList(null,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetNepaliCalendar(int? YearId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AcademicCalendar(User.UserId, User.HostName, User.DBName).getNepaliCalendar(YearId,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult UpdateFeedback()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {              
                var beData = DeserializeObject<AcademicLib.API.AppCMS.FeedbackSuggestion>(Request["jsonData"]);
                if (beData != null)
                {
                    if (beData.TranId == 0)
                    {
                        resVal.ResponseMSG = "Please ! Select Valid Feedback";
                    }else if (string.IsNullOrEmpty(beData.Response))
                    {
                        resVal.ResponseMSG = "Please ! Enter Feedback Response";
                    }
                    else                
                     resVal = new AcademicLib.BL.AppCMS.Creation.AboutUs(User.UserId, User.HostName, User.DBName).UpdateFeedback(beData);
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



        //[PermissionsAttribute(Actions.View, (int)ENTITIES.Quotes, false)]
        public ActionResult Quotes()
        {
            return View();
        }

        #region "Quotes"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveQuotes()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Quotes>(Request["jsonData"]);
                if (beData != null)
                {
                    
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.QuotesId.HasValue)
                        beData.QuotesId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Quotes(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                   
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
        public JsonNetResult GetAllQuotesList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Quotes(User.UserId, User.HostName, User.DBName).GetAllQuotes(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetQuotesById(int QuotesId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Quotes(User.UserId, User.HostName, User.DBName).GetQuotesById(0, QuotesId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelQuotes(int QuotesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Quotes(User.UserId, User.HostName, User.DBName).DeleteById(0, QuotesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion



        //new MEnu added from below
        public ActionResult VisionStatement()
        {
            return View();
        }
        public ActionResult FounderMessage()
        {
            return View();
        }

        public ActionResult StaffHierarchy()
        {
            return View();
        }
        public ActionResult CommitteHierarchy()
        {
            return View();
        }
        public ActionResult AdmissionProcedure()
        {
            return View();
        }
       
        public ActionResult RulesRegulation()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult FeedbackSuggestion()
        {
            return View();
        }

        #region "Testimonial"

        public ActionResult Testimonial()
        {
            return View();
        }


        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveTestimonial()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Testimonial>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["file0"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.TestimonialId.HasValue)
                        beData.TestimonialId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Testimonial(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllTestimonialList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Testimonial(User.UserId, User.HostName, User.DBName).GetAllTestimonial(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTestimonialById(int TestimonialId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Testimonial(User.UserId, User.HostName, User.DBName).GetTestimonialById(0, TestimonialId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelTestimonial(int TestimonialId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Testimonial(User.UserId, User.HostName, User.DBName).DeleteById(0, TestimonialId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion
        public ActionResult MenuConfiguration()
        {
            return View();
        }
        [HttpPost]
        //[PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveMenuConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AppCMSEntityCollections>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.AppCMS.Creation.AppCMSEntity(User.UserId, User.HostName, User.DBName).SaveFormData(dataColl);
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
        public JsonNetResult GetAppCMSEntity()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AppCMSEntity(User.UserId, User.HostName, User.DBName).GetEntity("");
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "SocialMedia"

        public ActionResult SocialMedia()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        //[PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveSocialMedia()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<List<AcademicLib.BE.AppCMS.Creation.SocialMedia>>(Request["jsonData"]);
                if (dataColl != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var fInd = 0;
                        foreach(var beData in dataColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.IconPath = att.DocPath;
                            }
                            fInd++;
                        }
                    }
                    foreach(var beData in dataColl)
                    {
                        beData.CUserId = User.UserId;
                        if (!beData.SocialMediaId.HasValue)
                            beData.SocialMediaId = 0;

                        resVal = new AcademicLib.BL.AppCMS.Creation.SocialMedia(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllSocialMedia()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.SocialMedia(User.UserId, User.HostName, User.DBName).GetAllSocialMedia(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        
        [HttpPost]
        public JsonNetResult GetSocialMediaById(int SocialMediaId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.SocialMedia(User.UserId, User.HostName, User.DBName).GetSocialMediaById(0, SocialMediaId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelSocialMedia(int SocialMediaId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.SocialMedia(User.UserId, User.HostName, User.DBName).DeleteById(0, SocialMediaId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        #region "NepaliMonth"

        public ActionResult NepaliMonth()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        //[PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveNepaliMonth()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (User.UserId == 1)
                {
                    var dataColl = DeserializeObject<AcademicLib.BE.AppCMS.Creation.NepaliMonthCollections>(Request["jsonData"]);
                    if (dataColl != null)
                    {
                        resVal = new AcademicLib.BL.AppCMS.Creation.NepaliMonth(User.UserId, User.HostName, User.DBName).SaveFormData(dataColl);
                    }
                    else
                    {
                        resVal.ResponseMSG = "Blank Data Can't be Accept";
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Access denied";
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
        public JsonNetResult GetNepaliMonth()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.NepaliMonth(User.UserId, User.HostName, User.DBName).GetAllMonth(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
 

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelNepaliMonth(int NM)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (User.UserId == 1)
                    resVal = new AcademicLib.BL.AppCMS.Creation.NepaliMonth(User.UserId, User.HostName, User.DBName).DeleteById(0, NM);
                else
                    resVal.ResponseMSG = "Access denied"; 
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "ThemeConfiguration"
        public ActionResult ThemeConfiguration()
        {
            return View();
        }

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ThemeConfig)]
        public JsonNetResult SaveThemeConfig()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.ThemeConfig>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.ThemeConfig(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.ThemeConfig)]
        public JsonNetResult GetThemeConfiguration()
        {
            AcademicLib.BE.AppCMS.Creation.ThemeConfig resVal = new AcademicLib.BE.AppCMS.Creation.ThemeConfig();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.ThemeConfig(User.UserId, User.HostName, User.DBName).GetAllThemeConfig(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion


        #region"Category"

        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveNewsCategory()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.NewsCategory>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CategoryId.HasValue)
                        beData.CategoryId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.NewsCategory(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllNewsCategory()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.NewsCategory(User.UserId, User.HostName, User.DBName).GetAllNewsCategory(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetNewsCategoryById(int CategoryId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.NewsCategory(User.UserId, User.HostName, User.DBName).getNewsCategoryById(0, CategoryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelNewCategory(int CategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.NewsCategory(User.UserId, User.HostName, User.DBName).DeleteById(0, CategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region"NewsSection"

        public ActionResult NewsSection()
        {
            return View();
        }


        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveNewsSection()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.NewsSection>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Photo = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.NewsSectionId.HasValue)
                        beData.NewsSectionId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.NewsSection(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllNewsSection()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.NewsSection(User.UserId, User.HostName, User.DBName).GetAllNewsSection(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetNewsSectionById(int NewsSectionId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.NewsSection(User.UserId, User.HostName, User.DBName).getNewsSectionById(0, NewsSectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelNewsSection(int NewsSectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.NewsSection(User.UserId, User.HostName, User.DBName).DeleteById(0, NewsSectionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region"Achievement Section"
        public ActionResult AchievementSection()
        {
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public JsonNetResult SaveAchievementSection()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AchievementSection>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Photo = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.AchievementSectionId.HasValue)
                        beData.AchievementSectionId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.AchievementSection(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAchievementSection()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AchievementSection(User.UserId, User.HostName, User.DBName).GetAllAchievementSection(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAchievementSectionById(int AchievementSectionId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AchievementSection(User.UserId, User.HostName, User.DBName).getAchievementSectionById(0, AchievementSectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAchievementSection(int AchievementSectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.AchievementSection(User.UserId, User.HostName, User.DBName).DeleteById(0, AchievementSectionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        #region "MandatoryDisclosure"
        public ActionResult MandatoryDisclosure()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveMandatoryDisclosure()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.MandatoryDisclosure>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    resVal = new AcademicLib.BL.AppCMS.Creation.MandatoryDisclosure(User.UserId, User.HostName, User.DBName)
                        .SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank data can't be accepted.";
                }
            }
            catch (Exception e)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = e.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetMandatoryDisclosures()
        {
            AcademicLib.BE.AppCMS.Creation.MandatoryDisclosureCollections dataColl = new AcademicLib.BE.AppCMS.Creation.MandatoryDisclosureCollections();
            try
            {
                dataColl = new AcademicLib.BL.AppCMS.Creation.MandatoryDisclosure(User.UserId, User.HostName, User.DBName).GetMandatoryDisclosures(0);
                return new JsonNetResult()
                {
                    Data = dataColl,
                    TotalCount = dataColl.Count,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
            }
            catch (Exception ex)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = null,
                TotalCount = 0,
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG
            };
        }
        [HttpPost]
        public JsonNetResult GetMandatoryDisclosureById(int TranId)
        {
            AcademicLib.BE.AppCMS.Creation.MandatoryDisclosure mandatoryDisclosure = new AcademicLib.BE.AppCMS.Creation.MandatoryDisclosure();
            try
            {
                mandatoryDisclosure = new AcademicLib.BL.AppCMS.Creation.MandatoryDisclosure(User.UserId, User.HostName, User.DBName)
                    .GetMandatoryDisclosureById(0, TranId);
                return new JsonNetResult()
                {
                    Data = mandatoryDisclosure,
                    TotalCount = 0,
                    IsSuccess = mandatoryDisclosure.IsSuccess,
                    ResponseMSG = mandatoryDisclosure.ResponseMSG
                };
            }
            catch (Exception ex)
            {
                mandatoryDisclosure.IsSuccess = false;
                mandatoryDisclosure.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = null,
                TotalCount = 0,
                IsSuccess = mandatoryDisclosure.IsSuccess,
                ResponseMSG = mandatoryDisclosure.ResponseMSG
            };
        }
        //Added by Shishant Shrestha on Dec 17, 2024
        [HttpPost]
        public JsonNetResult DeleteMandatoryDisclosure(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.MandatoryDisclosure(User.UserId, User.HostName, User.DBName)
                    .DeleteMandatoryDisclosure(0, TranId);
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
        //
        #endregion

        #region "Download"
        public ActionResult Download()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveDownload()
        {
            ResponeValues resVal = new ResponeValues();
            var attachmentLocation = "/Attachments/appcms";
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Download>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var fileColl = Request.Files;
                        var attachment = fileColl["attachment"];
                        if (attachment != null)
                        {
                            var attach = GetAttachmentDocuments(attachmentLocation, attachment, true);
                            beData.AttachmentPath = attach.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    resVal = new AcademicLib.BL.AppCMS.Creation.Download(User.UserId, User.HostName, User.DBName)
                        .SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank data can't be accepted.";
                }
            }
            catch (Exception e)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = e.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDownloads()
        {
            AcademicLib.BE.AppCMS.Creation.DownloadCollections dataColl = new AcademicLib.BE.AppCMS.Creation.DownloadCollections();
            try
            {
                dataColl = new AcademicLib.BL.AppCMS.Creation.Download(User.UserId, User.HostName, User.DBName).GetDownloads(0);
                return new JsonNetResult()
                {
                    Data = dataColl,
                    TotalCount = dataColl.Count,
                    IsSuccess = dataColl.IsSuccess,
                    ResponseMSG = dataColl.ResponseMSG
                };
            }
            catch (Exception ex)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = null,
                TotalCount = 0,
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG
            };
        }
        [HttpPost]
        public JsonNetResult GetDownloadById(int TranId)
        {
            AcademicLib.BE.AppCMS.Creation.Download beData = new AcademicLib.BE.AppCMS.Creation.Download();
            try
            {
                beData = new AcademicLib.BL.AppCMS.Creation.Download(User.UserId, User.HostName, User.DBName)
                    .GetDownloadById(0, TranId);
                return new JsonNetResult()
                {
                    Data = beData,
                    TotalCount = 0,
                    IsSuccess = beData.IsSuccess,
                    ResponseMSG = beData.ResponseMSG
                };
            }
            catch (Exception ex)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = null,
                TotalCount = 0,
                IsSuccess = beData.IsSuccess,
                ResponseMSG = beData.ResponseMSG
            };
        }
        [HttpPost]
        public JsonNetResult DeleteDownload(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Download(User.UserId, User.HostName, User.DBName)
                    .DeleteDownload(0, TranId);
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
        #endregion

        #region"Program"

        public ActionResult Program()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveProgram()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.Program>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ProgramId.HasValue)
                        beData.ProgramId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Program(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllProgram()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Program(User.UserId, User.HostName, User.DBName).GetAllProgram(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetProgramById(int ProgramId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Program(User.UserId, User.HostName, User.DBName).getProgramById(0, ProgramId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelProgram(int ProgramId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Program(User.UserId, User.HostName, User.DBName).DeleteById(0, ProgramId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Syllabus Plan"
        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveSyllabus()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.SyllabusPlan>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.Syllabus(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllSyllabus()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Syllabus(User.UserId, User.HostName, User.DBName).getAllSyllabus(0, null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelSyllabus(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.Syllabus(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSyllabusPlanById(int TranId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.Syllabus(User.UserId, User.HostName, User.DBName).GetSyllabusPlanById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #endregion

        public ActionResult ProudAlumni()
        {
            return View();
        }

        public ActionResult MeritAchievers()
        {
            return View();
        }

        #region "MeritAchievers"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveMeritAchievers()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.MeritAchievers>(Request["jsonData"]);
                if (beData != null)
                {

                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["Sphoto"];
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.MeritAchievers(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllMeritAchieversList()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.MeritAchievers(User.UserId, User.HostName, User.DBName).GetAllMeritAchievers(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetMeritAchieversById(int TranId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.MeritAchievers(User.UserId, User.HostName, User.DBName).GetMeritAchieversById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelMeritAchievers(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.MeritAchievers(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion



        #region "ProudAlumni"
        [HttpPost]
        public JsonNetResult SaveProudAlumni()
        {
            ResponeValues resVal = new ResponeValues();

            var photoLocation = "/Attachments/appcms";

            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.ProudAlumni>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var fileColl = Request.Files;
                        var sPhoto = fileColl["Sphoto"];
                        if (sPhoto != null)
                        {
                            var photo = GetAttachmentDocuments(photoLocation, sPhoto, true);
                            beData.ImagePath = photo.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.AppCMS.ProudAlumni(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllProudAlumni()
        {
            var dataColl = new AcademicLib.BL.AppCMS.ProudAlumni(User.UserId, User.HostName, User.DBName).GetAllProudAlumni(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetProudAlumniById(int TranId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.ProudAlumni(User.UserId, User.HostName, User.DBName).GetProudAlumniById(0, TranId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DeleteProudAlumni(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.ProudAlumni(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        public ActionResult HeaderDetails()
        {
            return View();
        }

        #region "HeaderDetails"
        [HttpPost]
        public JsonNetResult SaveHeaderDetails()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.HeaderDetails>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filecoll = Request.Files;
                        var sPhoto = filecoll["Sphoto"];
                        if (sPhoto != null)
                        {
                            var photo = GetAttachmentDocuments(photoLocation, sPhoto, true);
                            beData.LogoPhoto = photo.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;
                    if (!beData.HeaderDetailId.HasValue)
                        beData.HeaderDetailId = 0;



                    resVal = new AcademicLib.BL.AppCMS.Creation.HeaderDetails(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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


        public JsonNetResult GetHeaderDetailsById()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.HeaderDetails(User.UserId, User.HostName, User.DBName).GetHeaderDetailsById(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        public ActionResult Alumni()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveAlumniReg()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.AlumniReg>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;

                        var marksheetPhoto = filesColl["MarksheetPhoto"];
                        if (marksheetPhoto != null && marksheetPhoto.ContentLength > 0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, marksheetPhoto, true);
                            beData.MarksheetB = photoDoc.Data;
                            beData.MarksheetPath = photoDoc.DocPath;
                        }

                        var profilePhoto = filesColl["ProfilePhoto"];
                        if (profilePhoto != null && profilePhoto.ContentLength > 0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, profilePhoto, true);
                            beData.ProfileB = photoDoc.Data;
                            beData.ProfilePhoto = photoDoc.DocPath;
                        }

                        var memoryPhoto1 = filesColl["MemoryPhoto1"];
                        if (memoryPhoto1 != null && memoryPhoto1.ContentLength > 0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, memoryPhoto1, true);
                            beData.Memory1B = photoDoc.Data;
                            beData.MemoryPhoto1 = photoDoc.DocPath;
                        }

                        var memoryPhoto2 = filesColl["MemoryPhoto2"];
                        if (memoryPhoto2 != null && memoryPhoto2.ContentLength > 0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, memoryPhoto2, true);
                            beData.Memory2B = photoDoc.Data;
                            beData.MemoryPhoto2 = photoDoc.DocPath;
                        }
                        var achievement_Doc = filesColl["Achievement_Doc"];
                        if (achievement_Doc != null && achievement_Doc.ContentLength > 0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, achievement_Doc, true);
                            beData.AchievementB = photoDoc.Data;
                            beData.Achievement_Doc = photoDoc.DocPath;
                        }
                    }

                    beData.CUserId = User.UserId;
                    if (!beData.AlumniRegId.HasValue)
                        beData.AlumniRegId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.AlumniReg(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllAlumni()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AlumniReg(User.UserId, User.HostName, User.DBName).GetAllAlumni();

            return new JsonNetResult()
            {
                Data = dataColl,
                TotalCount = dataColl.Count,
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG
            };
        }
        [HttpPost]
        public JsonNetResult DelAlumniById(int AlumniRegId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.AlumniReg(User.UserId, User.HostName, User.DBName).DeleteById(AlumniRegId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAlumniRegById(int AlumniRegId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.AlumniReg(User.UserId, User.HostName, User.DBName).GetAlumniRegById(AlumniRegId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult GrievanceRedressalTeam()
        {
            return View();
        }
        #region "GrievanceRedressalTeam"
        [HttpPost]
        public JsonNetResult SaveGrievanceRedressalTeam()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AppCMS.Creation.GrievanceRedressalTeam>(Request["jsonData"]);
                if (beData != null)
                {
                    // To save single photo set file location
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;

                        var empPhoto = filesColl["photo"];

                        if (empPhoto != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, empPhoto, true);
                            beData.PhotoB = photoDoc.Data;
                            beData.Image = photoDoc.DocPath;
                        }
                    }
                    bool isModify = false;
                    beData.CUserId = User.UserId;
                    if (!beData.GrievanceRedressalId.HasValue)
                        beData.GrievanceRedressalId = 0;

                    resVal = new AcademicLib.BL.AppCMS.Creation.GrievanceRedressalTeam(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllGrievanceRedressalTeam()
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.GrievanceRedressalTeam(User.UserId, User.HostName, User.DBName).GetAllGrievanceRedressalTeam(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]

        public JsonNetResult GetGrievanceRedressalTeamById(int GrievanceRedressalId)
        {
            var dataColl = new AcademicLib.BL.AppCMS.Creation.GrievanceRedressalTeam(User.UserId, User.HostName, User.DBName).GetGrievanceRedressalTeamById(0, GrievanceRedressalId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]

        public JsonNetResult DelGrievanceRedressalTeam(int GrievanceRedressalId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.AppCMS.Creation.GrievanceRedressalTeam(User.UserId, User.HostName, User.DBName).DeleteById(0, GrievanceRedressalId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


    }
}