using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Transaction
{

	public class LessonPlanning
	{

		DA.Academic.Transaction.LessonPlanningDB db = null;

		int _UserId = 0;

		public LessonPlanning(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Transaction.LessonPlanningDB(hostName, dbName);
		}
		//public ResponeValues SaveFormData(BE.Academic.Transaction.LessonPlanning beData)
		//{
		//	bool isModify = beData.TranId > 0;
		//	ResponeValues isValid = IsValidData(ref beData, isModify);
		//	if (isValid.IsSuccess)
		//		return db.SaveUpdate(beData, isModify);
		//	else
		//		return isValid;
		//}

		public ResponeValues SaveUpdate(AcademicLib.BE.Academic.Transaction.LessonPlanningCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			resVal = db.SaveUpdateLeavePlanningColl(_UserId,  dataColl);

			return resVal;
		}
		public BE.Academic.Transaction.LessonPlan GetLPClassSubjectSecWise(int ClassId, int SubjectId, string SectionIdColl, int? BatchId, int? ClassYearId, int? SemesterId, int AcademicYearId)
		{
			return db.getLPClassSubjectSecWise(_UserId, ClassId, SubjectId, SectionIdColl, BatchId, ClassYearId, SemesterId, AcademicYearId);
		}


		public ResponeValues SaveLessonPlannigContent(List<BE.Academic.Transaction.LessonTopicTeacherContent> dataColl)
		{
			return db.SaveLessonPlanningTopicContent(_UserId, dataColl);
		}
		public AcademicLib.BE.Academic.Transaction.LessonTopicTeacherContentCollections getLessonPlanningTopicContent(int TranId)
		{
			return db.getLPTopicContent(_UserId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
        public AcademicLib.BE.Academic.Transaction.LessonPlan getLessonPlanForUpdate(int ClassId, int SubjectId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AcademicYearId)
        {
            var lessonPlan = db.getLessonPlanByForUpdate(_UserId, ClassId, SubjectId, SectionId, BatchId, ClassYearId, SemesterId, AcademicYearId);

            foreach (var lt in lessonPlan.DetailsColl)
            {
                double totalTopicContent = 0;
                double totalInProgress = 0, totalCompleted = 0, totalPending = 0;
                if (lt.TopicColl != null)
                {
                    lt.TotalDays = lt.TopicColl.Sum(p1 => p1.TotalDays);
                    foreach (var tc in lt.TopicColl)
                    {
                        totalTopicContent += tc.ContentsColl.Count;
                        totalCompleted += tc.ContentsColl.Where(p1 => p1.StatusValue == 3).Count();
                        totalInProgress += tc.ContentsColl.Where(p1 => p1.StatusValue == 2).Count();
                        totalPending += tc.ContentsColl.Where(p1 => p1.StatusValue == 1).Count();
                    }
                }

                if (totalTopicContent > 0)
                {
                    if (totalPending > 0)
                    {
                        lt.PendingPer = Math.Round(totalPending / totalTopicContent * 100, 2);
                    }
                    if (totalCompleted > 0)
                    {
                        lt.CompletedPer = Math.Round(totalCompleted / totalTopicContent * 100, 2);
                    }
                    if (totalInProgress > 0)
                    {
                        lt.InProgressPer = Math.Round(totalInProgress / totalTopicContent * 100, 2);
                    }
                }
            }

            return lessonPlan;
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.LessonPlanning beData, bool IsModify)
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

