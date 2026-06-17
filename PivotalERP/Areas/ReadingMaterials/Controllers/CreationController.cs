using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.ReadingMaterials.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
		// GET: ReadingMaterials/Creation

		#region "Reading Materials"

		public ActionResult ReadingMaterials()
		{
			return View();
		}
		string DocLocation = "/Attachments/ReadingMaterials";

		[HttpPost]
		public JsonNetResult SaveReadMatVideo()
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				var beDataColl = DeserializeObject<List<AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatVideo>>(Request["jsonData"]);
				if (beDataColl != null)
				{
					resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).SaveVideo(beDataColl);
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
		public JsonNetResult GetReadMatVideoById(int ReadMatVideoId)
		{
			var dataColl = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).GetReadMatVideoById(0, ReadMatVideoId);
			return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
		}
		[HttpPost]
		public JsonNetResult DelReadMatVideo(int ReadMatVideoId)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).DeleteVideoById(0, ReadMatVideoId);
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
		}

		[HttpPost]
		public JsonNetResult SaveReadMatCont()
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				var beData = DeserializeObject<AcademicLib.BE.ReadingMaterials.ReadingMaterials.ReadMatContent>(Request["jsonData"]);
				if (beData != null)
				{
					if (Request.Files.Count > 0)
					{
						var allFiles = Request.Files;
						int find = 0;
						foreach (var doc in beData.ContentAttach)
						{
							HttpPostedFileBase file = allFiles["file" + find];
							if (file != null)
							{
								var newDoc = GetAttachmentDocuments(DocLocation, file);
								if (newDoc != null)
								{
									doc.Name = newDoc.Name;
									doc.Extension = newDoc.Extension;
									doc.DocPath = newDoc.DocPath;
									doc.DocFullPath = newDoc.DocFullPath;
								}
							}
							find++;
						}
					}
					beData.CUserId = User.UserId;
					if (!beData.ReadMatContId.HasValue)
						beData.ReadMatContId = 0;

					resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).SaveContent(beData);

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
		public JsonNetResult GetReadMatContById(int ReadMatContId)
		{
			var dataColl = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).GetReadMatContById(0, ReadMatContId);
			return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
		}
		[HttpPost]
		public JsonNetResult DelReadMatCont(int ReadMatContId)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				resVal = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).DeleteContentById(0, ReadMatContId);
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
		}

		[HttpPost]
		public JsonNetResult GetClassSubWiseData(int ClassId, int? ClassYearId, int? SemesterId, int SubjectId, int BatchId)
		{
			var dataColl = new AcademicLib.BL.ReadingMaterials.ReadingMaterials(User.UserId, User.HostName, User.DBName).GetClassSubWiseData(0, ClassId, ClassYearId, SemesterId, SubjectId,BatchId);
			return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
		}
		#endregion
	}
}