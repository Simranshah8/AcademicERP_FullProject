using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Transaction
{

	public class ICMarkEntry
	{

		DA.Exam.Transaction.ICMarkEntryDB db = null;

		int _UserId = 0;

		public ICMarkEntry(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Transaction.ICMarkEntryDB(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.Exam.Transaction.ICMarkEntryCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			resVal = db.SaveUpdate(_UserId, dataColl);
			return resVal;
		}
		
		public BE.Exam.Transaction.ICStudentsDetailCollections GetIStudentsDetailsSubjectsWise(int EntityId, int? AcademicYearId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, string TopicName, int? AssessmentTypeId, int? CFAssessmentTypeId)
		{
			return db.getIStudentsDetailsSubjectsWise(_UserId, EntityId, AcademicYearId, ClassId, SectionId, FilterSection, SubjectId, LessonId, TopicName, AssessmentTypeId, CFAssessmentTypeId);
		}
		public BE.Exam.Transaction.TopicForStudentWiseICCollections GetTopicForStudentWiseIC(int EntityId, int? AcademicYearId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, int StudentId, int? AssessmentTypeId, int? CFAssessmentTypeId)
		{
			return db.getTopicForStudentWiseIC(_UserId, EntityId, AcademicYearId, ClassId, SectionId, FilterSection, SubjectId, LessonId, StudentId, AssessmentTypeId, CFAssessmentTypeId);
		}

		public BE.Exam.Transaction.ICMArkEntryStatusCCollections GetICMArkSubmitStatus(int ClassId, int? SectionId, int SubjectId, int LessonId, int? AcademicYearId)
		{
			return db.getICMarkSubmitStatus(_UserId, ClassId, SectionId, SubjectId, LessonId, AcademicYearId);
		}


		public ResponeValues DeleteById(int EntityId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, string TopicName, int AssessmentTypeId)
		{
			return db.DeleteICMarkEntryById(_UserId, EntityId, ClassId, SectionId, FilterSection, SubjectId, LessonId, TopicName, AssessmentTypeId);
		}

		public ResponeValues DeleteByIdStudentWise(int EntityId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, int StudentId, int AssessmentTypeId)
		{
			return db.DeleteICMarkEntryStudentwiseById(_UserId, EntityId, ClassId, SectionId, FilterSection, SubjectId, LessonId, StudentId, AssessmentTypeId);
		}

		public ResponeValues IsValidData(ref BE.Exam.Transaction.ICMarkEntry beData, bool IsModify)
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

