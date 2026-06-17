using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.ReadingMaterials
{
	public class ReadingMaterials
	{
		DA.ReadingMaterials.ReadingMaterialsDB db = null;
		int _UserId = 0;

		public ReadingMaterials(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.ReadingMaterials.ReadingMaterialsDB(hostName, dbName);
		}
		public ResponeValues SaveVideo(List<BE.ReadingMaterials.ReadingMaterials.ReadMatVideo> dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			if (dataColl == null || dataColl.Count == 0)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = "No  data to save.";
				return resVal;
			}
			if (dataColl.Any(x => x.ClassId == null || x.ClassId.HasValue == false))
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = "Please Select Class!";
				return resVal;
			}
			if (dataColl.Any(x => x.SubjectId == null || x.SubjectId.HasValue == false))
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = "Please Select Subject!";
				return resVal;
			}
			if (dataColl.Any(x => string.IsNullOrWhiteSpace(x.VideoTitle)))
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = "Please Enter Video Title!";
				return resVal;
			}

			return db.SaveUpdateVideo(dataColl, _UserId);

		}
		public BE.ReadingMaterials.ReadingMaterials.ReadMatVideo GetReadMatVideoById(int EntityId, int ReadMatVideoId)
		{
			return db.GetReadMatVideoById(_UserId, EntityId, ReadMatVideoId);
		}
		public ResponeValues DeleteVideoById(int EntityId, int ReadMatVideoId)
		{
			return db.DeleteVideoById(_UserId, EntityId, ReadMatVideoId);
		}
		public ResponeValues SaveContent(BE.ReadingMaterials.ReadingMaterials.ReadMatContent beData)
		{
			bool isModify = beData.ReadMatContId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdateContent(beData, isModify);
			else
				return isValid;
		}
		public BE.ReadingMaterials.ReadingMaterials.ReadMatContent GetReadMatContById(int EntityId, int ReadMatContId)
		{
			return db.GetReadMatContById(_UserId, EntityId, ReadMatContId);
		}
		public ResponeValues DeleteContentById(int EntityId, int ReadMatContId)
		{
			return db.DeleteContentById(_UserId, EntityId, ReadMatContId);
		}
        public BE.ReadingMaterials.ReadingMaterials GetClassSubWiseData(int EntityId, int ClassId, int? ClassYearId, int? SemesterId, int SubjectId, int? BatchId)
        {
            return db.GetClassSubWiseData(ClassId, ClassYearId, SemesterId, SubjectId, _UserId, EntityId, BatchId);
        }
        public ResponeValues IsValidData(ref BE.ReadingMaterials.ReadingMaterials.ReadMatContent beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.ReadMatContId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ReadMatContId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.ClassId == 0 || beData.ClassId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select Class ";
				}
				else if (beData.SubjectId == 0 || beData.SubjectId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select Subject ";
				}
				else if (string.IsNullOrEmpty(beData.Title))
				{
					resVal.ResponseMSG = "Please ! Enter Title ";
				}
				else
				{
					resVal.IsSuccess = true;
					resVal.ResponseMSG = "Valid";
				}
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			return resVal;
		}
	}
}
