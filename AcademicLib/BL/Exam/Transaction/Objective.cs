using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{
	public class Objective
	{
		DA.Exam.Transaction.ObjectiveDB db = null;

		int _UserId = 0;

		public Objective(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ObjectiveDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Exam.Transaction.Objective beData)
		{
			bool isModify = beData.ObjectiveId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Exam.Transaction.Objective GetObjective(int EntityId, int? ClassId, int? ExamTypeId, int? SubjectId)
		{
			return db.GetObjective(_UserId, EntityId, ClassId, ExamTypeId, SubjectId);
		}
		public BE.Exam.Transaction.ObjectiveCollections GetAllObjective(int EntityId)
		{
			return db.getAllObjective(_UserId, EntityId);
		}
		public ResponeValues DeleteById(int EntityId, int ObjectiveId)
		{
			return db.DeleteById(_UserId, EntityId, ObjectiveId);
		}
		public BE.Exam.Transaction.ObjectiveCollections GetAllObjectiveTansfer(int EntityId, int? FromExamTypeId, int? ToExamTypeId, int? FromClassId, int? ToClassId)
		{
			return db.GetAllObjectiveTansfer(_UserId, EntityId, FromExamTypeId, ToExamTypeId, FromClassId, ToClassId);
		}
		public ResponeValues TransferObjective(int? FromExamTypeId, int? ToExamTypeId, int? FromClassId, int? ToClassId, int? FromSectionId, int? ToSectionId)
		{
			return db.TransferObjective(_UserId, FromExamTypeId, ToExamTypeId, FromClassId, ToClassId, FromSectionId, ToSectionId);
		}
		public BE.Exam.Transaction.Objective GetObjectiveById(int EntityId, int ObjectiveId)
		{
			return db.GetObjectiveById(_UserId, EntityId, ObjectiveId);
		}
		public ResponeValues IsValidData(ref BE.Exam.Transaction.Objective beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.ObjectiveId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ObjectiveId != 0)
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
				else if (beData.ExamTypeId == 0 || beData.ExamTypeId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select ExamType ";
				}
				else if (beData.SubjectId == 0 || beData.SubjectId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select Subject ";
				}
				else if (beData.FullMark == 0 || beData.FullMark.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Enter Full Mark ";
				}
  				else
				{
					//Validate Child table name
                    foreach (var detail in beData.ObjectiveDetailsColl)
                    {
						if (string.IsNullOrEmpty(detail.Name))
						{
							resVal.ResponseMSG = "Please! Enter Objective Details Name";
							return resVal;
						}
					}
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

