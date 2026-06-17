using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;

namespace PivotalERP.Areas.Library.Controllers
{
    public class MasterController : PivotalERP.Controllers.BaseController
    {
        string photoLocation = "/Attachments/library";
        // GET: Library/Master
        public ActionResult BoookTitle()
        {        
            return View();
        }

        #region "BookTitle"

        [HttpPost]
        public JsonNetResult SaveBookTitle()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.BookTitle>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.BookTitleId.HasValue)
                        beData.BookTitleId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.BookTitle(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllBookTitleList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.BookTitle(User.UserId, User.HostName, User.DBName).GetAllBookTitle(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBookTitleById(int BookTitleId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.BookTitle(User.UserId, User.HostName, User.DBName).GetBookTitleById(0, BookTitleId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBookTitle(int BookTitleId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.BookTitle(User.UserId, User.HostName, User.DBName).DeleteById(0, BookTitleId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "MaterialType"

        [HttpPost]
        public JsonNetResult SaveMaterialType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.MaterialType>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Image = photoDoc.Data;
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(beData.ImagePath))
                        {
                            if (beData.ImagePath.StartsWith(photoLocation))
                            {
                                beData.Image = GetBytesFromFile(beData.ImagePath);
                            }
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.MaterialTypeId.HasValue)
                        beData.MaterialTypeId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.MaterialType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllMaterialTypeList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.MaterialType(User.UserId, User.HostName, User.DBName).GetAllMaterialType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetMaterialTypeById(int MaterialTypeId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.MaterialType(User.UserId, User.HostName, User.DBName).GetMaterialTypeById(0, MaterialTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelMaterialType(int MaterialTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.MaterialType(User.UserId, User.HostName, User.DBName).DeleteById(0, MaterialTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Author"

        [HttpPost]
        public JsonNetResult SaveAuthor()
        {            
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.Author>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Image = photoDoc.Data;
                            beData.ImagePath = photoDoc.DocPath;
                        }                     
                    }else
                    {
                        if (!string.IsNullOrEmpty(beData.ImagePath))
                        {
                            if (beData.ImagePath.StartsWith(photoLocation))
                            {
                                beData.Image = GetBytesFromFile(beData.ImagePath);
                            }
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.AuthorId.HasValue)
                        beData.AuthorId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.Author(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAuthorList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Author(User.UserId, User.HostName, User.DBName).GetAllAuthor(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAuthorById(int AuthorId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Author(User.UserId, User.HostName, User.DBName).GetAuthorById(0, AuthorId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAuthor(int AuthorId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.Author(User.UserId, User.HostName, User.DBName).DeleteById(0, AuthorId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Edition"

        [HttpPost]
        public JsonNetResult SaveEdition()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.Edition>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.EditionId.HasValue)
                        beData.EditionId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.Edition(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllEditionList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Edition(User.UserId, User.HostName, User.DBName).GetAllEdition(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEditionById(int EditionId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Edition(User.UserId, User.HostName, User.DBName).GetEditionById(0, EditionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelEdition(int EditionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.Edition(User.UserId, User.HostName, User.DBName).DeleteById(0, EditionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Publication"

        [HttpPost]
        public JsonNetResult SavePublication()
        {
            
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.Publication>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Logo = photoDoc.Data;
                            beData.LogoPath = photoDoc.DocPath;
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


                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            beData.AttachmentColl.Add(v);
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.PublicationId.HasValue)
                        beData.PublicationId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.Publication(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllPublicationList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Publication(User.UserId, User.HostName, User.DBName).GetAllPublication(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPublicationById(int PublicationId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Publication(User.UserId, User.HostName, User.DBName).GetPublicationById(0, PublicationId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelPublication(int PublicationId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.Publication(User.UserId, User.HostName, User.DBName).DeleteById(0, PublicationId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Donor"

        [HttpPost]
        public JsonNetResult SaveDonor()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.Donor>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Logo = photoDoc.Data;
                            beData.LogoPath = photoDoc.DocPath;
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


                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            beData.AttachmentColl.Add(v);
                        }
                    }

                    beData.CUserId = User.UserId;

                    if (!beData.DonorId.HasValue)
                        beData.DonorId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.Donor(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllDonorList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Donor(User.UserId, User.HostName, User.DBName).GetAllDonor(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDonorById(int DonorId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Donor(User.UserId, User.HostName, User.DBName).GetDonorById(0, DonorId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDonor(int DonorId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.Donor(User.UserId, User.HostName, User.DBName).DeleteById(0, DonorId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Rack"

        [HttpPost]
        public JsonNetResult SaveRack()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.Rack>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.RackId.HasValue)
                        beData.RackId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.Rack(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllRackList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Rack(User.UserId, User.HostName, User.DBName).GetAllRack(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetRackById(int RackId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Rack(User.UserId, User.HostName, User.DBName).GetRackById(0, RackId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelRack(int RackId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.Rack(User.UserId, User.HostName, User.DBName).DeleteById(0, RackId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "BookEntry"

        public ActionResult BookEntry(int bookEntryId = 0)
        {
            ViewBag.TranId = bookEntryId;
            return View();
        }

        [HttpPost]
        public JsonNetResult GetBookDetailsByBarcode(string barCode)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getBookDetailsByBarcode(barCode);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }
        [HttpPost]
        public JsonNetResult GetAccessionNo()
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getAccessionNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveBookEntry()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Library.Transaction.BookEntry>(Request["jsonData"]);
                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);                         
                            beData.FrontCoverPath = photoDoc.DocPath;
                        }

                        var photo2 = filesColl["photo2"];

                        if (photo2 != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo2, true);                          
                            beData.BackCoverPath = photoDoc.DocPath;
                        }
                    }
                   
                    beData.CUserId = User.UserId;

                    if (!beData.BookEntryId.HasValue)
                        beData.BookEntryId = 0;

                    resVal = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllBookEntryList()
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).GetAllBookEntry(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBookEntryById(int BookEntryId)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).GetBookEntryById(0, BookEntryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBookEntry(int BookEntryId,int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).DeleteById(0, BookEntryId,TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBookDetForBarcode(int fromAccessionNo,int toAccessionNo)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getBookForPrintBarcode(fromAccessionNo,toAccessionNo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintLibraryBarCode(List<AcademicLib.BE.Library.Transaction.PrintBarCode> paraData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Setting"
        public ActionResult Setting()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveSetting()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Library.Creation.Setup>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Library.Creation.LibrarySetting(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetSetting()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.LibrarySetting(User.UserId, User.HostName, User.DBName).getSetting();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        #region "Department"

         
        [HttpPost]
        public JsonNetResult SaveDepartment()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.Department>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DepartmentId.HasValue)
                        beData.DepartmentId = 0;

                    resVal = new AcademicLib.BL.Library.Creation.Department(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllDepartmentList()
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Department(User.UserId, User.HostName, User.DBName).GetAllDepartment(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDepartmentById(int DepartmentId)
        {
            var dataColl = new AcademicLib.BL.Library.Creation.Department(User.UserId, User.HostName, User.DBName).GetDepartmentById(0, DepartmentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDepartment(int DepartmentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Creation.Department(User.UserId, User.HostName, User.DBName).DeleteById(0, DepartmentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        public ActionResult PrintBarcode()
        {
            return View();
        }

        public ActionResult LibraryMembership()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GenerateMembership(AcademicLib.BE.Library.Creation.MembershipGenerate beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                beData.CUserId = User.UserId;
                resVal = new AcademicLib.BL.Library.Creation.MembershipGenerate(User.UserId, User.HostName, User.DBName).GenerateMembership(this.AcademicYearId, beData);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassWiseMemberlist(int ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            AcademicLib.BE.Library.Creation.StudentMemberCollections dataColl = new AcademicLib.BE.Library.Creation.StudentMemberCollections();
            try
            {

                dataColl = new AcademicLib.BL.Library.Creation.MembershipGenerate(User.UserId, User.HostName, User.DBName).getClassWiseMemberlist(this.AcademicYearId, ClassId, SectionId, BatchId, ClassYearId, SemesterId);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpMemberlist()
        {
            AcademicLib.BE.Library.Creation.EmployeeMemberCollections dataColl = new AcademicLib.BE.Library.Creation.EmployeeMemberCollections();
            try
            {

                dataColl = new AcademicLib.BL.Library.Creation.MembershipGenerate(User.UserId, User.HostName, User.DBName).getEmpMemberlist();
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult LibraryCard()
        {
            return View();
        }

        #region "Book Issues"
        public ActionResult BookIssue()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetBookIssueNo()
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookIssue(User.UserId, User.HostName, User.DBName).getBookIssueNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetCreditRules(int? StudentId,int? EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookIssue(User.UserId, User.HostName, User.DBName).getCreditRules(StudentId,EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveBookIssue()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Library.Transaction.BookIssue>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Library.Transaction.BookIssue(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
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

        #region "Book Received"
        public ActionResult BookReceived()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveBookReceived()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Library.Transaction.BookReceived>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Library.Transaction.BookReceived(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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

        #region "Reporting"
        public ActionResult LibraryReport()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetBookDetailsList(int MaterialTypeId,int ForType)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getBookDetailsList(MaterialTypeId,ForType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetBookIssueRegister(DateTime dateFrom,DateTime dateTo, bool PendingForReceived)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getBookIssueRegister(dateFrom, dateTo,PendingForReceived);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetBookReceivedRegister(DateTime dateFrom, DateTime dateTo)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getBookReceivedRegister(dateFrom, dateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetBookRegister(DateTime dateFrom, DateTime dateTo,int BookEntryId)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getBookRegister(BookEntryId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentEmpBookRegister(int? StudentId,int? EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getStudentEmpBookRegister(StudentId,EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetPreviousBookDetailsList(int? StudentId, int? EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookIssue(User.UserId, User.HostName, User.DBName).getPreviousBookDueList(StudentId, EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "BookCategory"
        [HttpPost]
        public JsonNetResult SaveBookCategory()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.BookCategory.Creation.BookCategory>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.BookCategoryId.HasValue)
                        beData.BookCategoryId = 0;

                    resVal = new AcademicLib.BL.BookCategory.Creation.BookCategory(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllBookCategory()
        {
            var dataColl = new AcademicLib.BL.BookCategory.Creation.BookCategory(User.UserId, User.HostName, User.DBName).getAllBookCategory(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetBookCategoryById(int BookCategoryId)
        {
            var dataColl = new AcademicLib.BL.BookCategory.Creation.BookCategory(User.UserId, User.HostName, User.DBName).getBookCategoryById(0, BookCategoryId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelBookCategory(int BookCategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.BookCategory.Creation.BookCategory(User.UserId, User.HostName, User.DBName).DeleteById(0, BookCategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion
        public ActionResult LibraryDashboard()
        {
            return View();
        }

        public ActionResult BookRequestList()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetAllBookLit(string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl, int ForType)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getAllBookList(SubjectIdColl, AuthorIdColl, PublicationIdColl, EditionIdColl, CategoryIdColl, ClassIdColl, FacultyIdColl, LevelIdColl, SemesterIdColl, ClassYearIdColl, ForType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllBooksTaken(string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl, bool PendingForReceived)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getAllBooksTaken(SubjectIdColl, AuthorIdColl, PublicationIdColl, EditionIdColl, CategoryIdColl, ClassIdColl, FacultyIdColl, LevelIdColl, SemesterIdColl, ClassYearIdColl, PendingForReceived);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllBookReturned(string SubjectIdColl, string AuthorIdColl, string PublicationIdColl, string EditionIdColl, string CategoryIdColl, string ClassIdColl, string FacultyIdColl, string LevelIdColl, string SemesterIdColl, string ClassYearIdColl)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).getAllBookReceived(SubjectIdColl, AuthorIdColl, PublicationIdColl, EditionIdColl, CategoryIdColl, ClassIdColl, FacultyIdColl, LevelIdColl, SemesterIdColl, ClassYearIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Book lost"
        public ActionResult BookLost()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveBookLostDamage()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Library.Transaction.BookLostDamage>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Library.Transaction.BookLostDamage(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetBookLostList(DateTime? DateFrom, DateTime? DateTo)
        {
            var dataColl = new AcademicLib.BL.Library.Transaction.BookLostDamage(User.UserId, User.HostName, User.DBName).GetBookLostDamages(DateFrom, DateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult UpdateBookLostAction(int IssueId, double? RecoveryAmount, bool? IsBookSubmitted)
        {
            var resVal = new AcademicLib.BL.Library.Transaction.BookLostDamage(User.UserId, User.HostName, User.DBName).UpdateBookLostAction(IssueId, RecoveryAmount, IsBookSubmitted);
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        [HttpPost]
        public JsonNetResult CheckBookInUse(int BookEntryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Library.Transaction.BookEntry(User.UserId, User.HostName, User.DBName).CheckBookInUse(BookEntryId);
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