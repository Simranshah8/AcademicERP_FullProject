using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class Indicator  
	{ 

		DA.Exam.Transaction.IndicatorDB db = null;

		int _UserId = 0;

		public Indicator(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.IndicatorDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Exam.Transaction.Indicator beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Exam.Transaction.IndicatorCollections GetAllIndicator(int EntityId, int ClassId, int SubjectId, int? LessonId, string TopicName)
		{
			return db.getAllIndicator(_UserId, EntityId, ClassId, SubjectId,LessonId,TopicName);
		}
		
		public BE.Exam.Transaction.SubjectLessonWiseCollections GetSubjectLessonWise(int EntityId, int ClassId, int SubjectId)
		{
			return db.getSubjectLessonWise(_UserId, EntityId, ClassId, SubjectId);
		}

		public BE.Exam.Transaction.LessonTopicDetailsWiseCollections GetLessonTopicDetailsWise(int EntityId, int? LessonId)
		{
			return db.getLessonTopicDetailsWise(_UserId, EntityId, LessonId);
		}

		//Code added by suresh on 12 jestha saturday
		public BE.Exam.Transaction.TopicWiseIndicatorCollections GetTopicWiseIndicator(int? LessonId, string TopicName)
		{
			return db.getTopicWiseIndicators(_UserId, LessonId, TopicName);
		}

		//new code added by bibek for IndicatorSummary on 06 june

		public BE.Exam.Transaction.IndicatorSummary GetIndicatorSummary(int EntityId, int? ClassId, int? SubjectId)
		{
			return db.getIndicatorSummary(_UserId, EntityId, ClassId, SubjectId);
		}

		public ResponeValues IsValidData(ref BE.Exam.Transaction.Indicator beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else
				{

					foreach (var ld in beData.IndicatorDetailsColl)
					{
						if (string.IsNullOrEmpty(ld.IndicatorName))
						{
							resVal.IsSuccess = false;
							resVal.ResponseMSG = "Please ! Enter Indicator Name for empty row";
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

