using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
	internal class ICMarkEntryDB
	{
		DataAccessLayer1 dal = null;
		public ICMarkEntryDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(int UserId, BE.Exam.Transaction.ICMarkEntryCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			try
			{
				foreach (var beData in dataColl)
				{
					cmd.Parameters.Clear();
					
					cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
					cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
					cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
					cmd.Parameters.AddWithValue("@LessonSno", beData.LessonSno);
					cmd.Parameters.AddWithValue("@TopicName", beData.TopicName);
					cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@IndicatorSNo", beData.IndicatorSNo);
                    cmd.Parameters.AddWithValue("@Evaluation", beData.Evaluation);
                    cmd.Parameters.AddWithValue("@Marks", beData.Marks);
					cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@EvaluationAreaId", beData.EvaluationAreaId);
					cmd.Parameters.AddWithValue("@IndicatorId", beData.IndicatorId);
					cmd.Parameters.AddWithValue("@AssessmentTypeId", beData.AssessmentTypeId);
					cmd.Parameters.AddWithValue("@AssessmentDate", beData.AssessmentDate);
					cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
					cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
					cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
					cmd.CommandText = "usp_AddICMarkEntry";
					cmd.ExecuteNonQuery();
				}
				dal.CommitTransaction();
				resVal.IsSuccess = true;
				resVal.ResponseMSG = "Mark Entry Saved";
			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return resVal;
		}


		public AcademicLib.BE.Exam.Transaction.ICStudentsDetailCollections getIStudentsDetailsSubjectsWise(int UserId, int EntityId, int? AcademicYearId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, string TopicName, int? AssessmentTypeId, int? CFAssessmentTypeId,
			DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.ICStudentsDetailCollections dataColl = new AcademicLib.BE.Exam.Transaction.ICStudentsDetailCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@FilterSection", FilterSection);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@TopicName", TopicName);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.Parameters.AddWithValue("@CFAssessmentTypeId", CFAssessmentTypeId);
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetStudentsForICMarkEntry";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ICStudentsDetail beData = new AcademicLib.BE.Exam.Transaction.ICStudentsDetail();
					beData.Indicators = new List<BE.Exam.Transaction.IndicatorList>();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.AcademicYearId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.StudentName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.RollNumber = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.ClassId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.SectionId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7); 
					if (!(reader[8] is DBNull)) beData.BatchId = reader.GetInt32(7);
					if (!(reader[9] is DBNull)) beData.SemesterId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.ClassYearId = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.SubjectId = reader.GetInt32(11);
					dataColl.Add(beData);
				}
				if (reader.NextResult())
				{
					while (reader.Read())
					{
						BE.Exam.Transaction.IndicatorList det = new BE.Exam.Transaction.IndicatorList();
						if (!(reader[0] is DBNull)) det.IndicatorId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) det.IndicatorSno = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) det.IndicatorName = reader.GetString(2);
						if (!(reader[3] is DBNull)) det.LessonSno = reader.GetInt32(3);
						if (!(reader[4] is DBNull)) det.TopicName = reader.GetString(4);
						if (!(reader[5] is DBNull)) det.StudentId = reader.GetInt32(5);
						if (!(reader[6] is DBNull)) det.Evaluation = Convert.ToBoolean(reader[6]);
						if (!(reader[7] is DBNull)) det.Marks = reader.GetInt32(7);
						if (!(reader[8] is DBNull)) det.Remarks = reader.GetString(8);
						if (!(reader[9] is DBNull)) det.EvaluationAreaId = reader.GetInt32(9);
						if (!(reader[10] is DBNull)) det.AssessmentTypeId = reader.GetInt32(10);
						if (!(reader[11] is DBNull)) det.AssessmentDate = Convert.ToDateTime(reader[11]);
						var student = dataColl.FirstOrDefault(x =>x.StudentId == det.StudentId);
						if (student != null)
						{
							if (student.Indicators == null)
								student.Indicators = new List<BE.Exam.Transaction.IndicatorList>();
							// Prevent duplicate indicators
							bool exists = student.Indicators.Any(x =>x.IndicatorId == det.IndicatorId && x.IndicatorSno == det.IndicatorSno && x.StudentId == det.StudentId);
							if (!exists)
							{
								student.Indicators.Add(det);
							}
						}
					}					
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return dataColl;
		}

		public AcademicLib.BE.Exam.Transaction.TopicForStudentWiseICCollections getTopicForStudentWiseIC(int UserId, int EntityId, int? AcademicYearId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, int StudentId, int? AssessmentTypeId, int? CFAssessmentTypeId,
			DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.TopicForStudentWiseICCollections dataColl = new AcademicLib.BE.Exam.Transaction.TopicForStudentWiseICCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@FilterSection", FilterSection);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@StudentId", StudentId);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.Parameters.AddWithValue("@CFAssessmentTypeId", CFAssessmentTypeId);
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetTopicForStudentWiseIC";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				//while (reader.Read())
				//{
				//	AcademicLib.BE.Exam.Transaction.TopicForStudentWiseIC beData = new AcademicLib.BE.Exam.Transaction.TopicForStudentWiseIC();
				//	if (!(reader[0] is DBNull)) beData.IndicatorId = reader.GetInt32(0);
				//	if (!(reader[1] is DBNull)) beData.TopicName = reader.GetString(1);
				//	if (!(reader[2] is DBNull)) beData.IndicatorName = reader.GetString(2);
				//	if (!(reader[3] is DBNull)) beData.SNo = reader.GetInt32(3);
				//	if (!(reader[4] is DBNull)) beData.Marks = reader.GetInt32(4);
				//	if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
				//	if (!(reader[6] is DBNull)) beData.Evaluation = Convert.ToBoolean(reader[6]);
				//	if (!(reader[7] is DBNull)) beData.EvaluationAreaId = reader.GetInt32(7);
				//	if (!(reader[8] is DBNull)) beData.ClassId = reader.GetInt32(8);
				//	if (!(reader[9] is DBNull)) beData.SectionId = reader.GetInt32(9);
				//	if (!(reader[10] is DBNull)) beData.BatchId = reader.GetInt32(10);
				//	if (!(reader[11] is DBNull)) beData.SemesterId = reader.GetInt32(11);
				//	if (!(reader[12] is DBNull)) beData.ClassYearId = reader.GetInt32(12);
				//	if (!(reader[13] is DBNull)) beData.StudentId = reader.GetInt32(13);
				//	if (!(reader[14] is DBNull)) beData.SubjectId = reader.GetInt32(14);
				//	if (!(reader[15] is DBNull)) beData.LessonSno = reader.GetInt32(15);
				//	if (!(reader[16] is DBNull)) beData.AssessmentDate = Convert.ToDateTime(reader[16]);

				//	dataColl.Add(beData);
				//}
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.TopicForStudentWiseIC beData = new AcademicLib.BE.Exam.Transaction.TopicForStudentWiseIC();
					if (!(reader[0] is DBNull)) beData.LessonId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.LessonSno = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.TopicName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.BatchId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.SemesterId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.ClassYearId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.SubjectId = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.StudentId = reader.GetInt32(9);
					dataColl.Add(beData);
				}
				if (reader.NextResult())
				{
					while (reader.Read())
					{
						BE.Exam.Transaction.IndicatorList det = new BE.Exam.Transaction.IndicatorList();
						if (!(reader[0] is DBNull)) det.IndicatorId = reader.GetInt32(0);
						if (!(reader[1] is DBNull)) det.IndicatorSno = reader.GetInt32(1);
						if (!(reader[2] is DBNull)) det.IndicatorName = reader.GetString(2);
						if (!(reader[3] is DBNull)) det.LessonSno = reader.GetInt32(3);
						if (!(reader[4] is DBNull)) det.TopicName = reader.GetString(4);
						if (!(reader[5] is DBNull)) det.StudentId = reader.GetInt32(5);
						if (!(reader[6] is DBNull)) det.Evaluation = Convert.ToBoolean(reader[6]);
						if (!(reader[7] is DBNull)) det.Marks = reader.GetInt32(7);
						if (!(reader[8] is DBNull)) det.Remarks = reader.GetString(8);
						if (!(reader[9] is DBNull)) det.EvaluationAreaId = reader.GetInt32(9);
						if (!(reader[10] is DBNull)) det.AssessmentTypeId = reader.GetInt32(10);
						if (!(reader[11] is DBNull)) det.AssessmentDate = Convert.ToDateTime(reader[11]);
						var student = dataColl.FirstOrDefault(x => x.TopicName == det.TopicName);
						if (student != null)
						{
							if (student.Indicators == null)
								student.Indicators = new List<BE.Exam.Transaction.IndicatorList>();
							// Prevent duplicate indicators
							bool exists = student.Indicators.Any(x => x.IndicatorId == det.IndicatorId && x.IndicatorSno == det.IndicatorSno && x.TopicName == det.TopicName);
							if (!exists)
							{
								student.Indicators.Add(det);
							}
						}
					}
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return dataColl;
		}

		public AcademicLib.BE.Exam.Transaction.ICMArkEntryStatusCCollections getICMarkSubmitStatus(int UserId, int ClassId, int? SectionId, int SubjectId, int LessonId, int? AcademicYearId, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.ICMArkEntryStatusCCollections dataColl = new AcademicLib.BE.Exam.Transaction.ICMArkEntryStatusCCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);		
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetICMarkSubmitList";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ICMArkEntryStatus beData = new AcademicLib.BE.Exam.Transaction.ICMArkEntryStatus();
					if (!(reader[0] is DBNull)) beData.TopicName = reader.GetString(0);
					if (!(reader[1] is DBNull)) beData.IsPending = Convert.ToBoolean(reader[1]);
					if (!(reader[2] is DBNull)) beData.SubmitDateTime_AD = reader.GetDateTime(2);
					if (!(reader[3] is DBNull)) beData.SubmitDateTime_BS = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.UserName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.ClassTeacher = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.SubjectTeacher = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.TeacherContactNo = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.UserId = reader.GetInt32(8);
					dataColl.Add(beData);
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return dataColl;
		}

		public ResponeValues DeleteICMarkEntryById(int UserId, int EntityId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, string TopicName, int? AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@FilterSection", FilterSection);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@TopicName", TopicName);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.CommandText = "usp_DelICMarkEntry";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

				if (!(cmd.Parameters[11].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return resVal;
		}


		public ResponeValues DeleteICMarkEntryStudentwiseById(int UserId, int EntityId, int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, int StudentId, int? AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@FilterSection", FilterSection);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@StudentId", StudentId);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.CommandText = "usp_DelICMarkEntryStudentWise";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

				if (!(cmd.Parameters[11].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

			}
			catch (System.Data.SqlClient.SqlException ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return resVal;
		}

		public AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntryCollections GetStudentsForLOCMarkEntry(int UserId, int EntityId, int? AcademicYearId, int ClassId, int? SectionId, int SubjectId, int LessonId, string TopicName, int? AssessmentTypeId, int? CFAssessmentTypeId,
			DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntryCollections dataColl = new AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntryCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@TopicName", TopicName);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.Parameters.AddWithValue("@CFAssessmentTypeId", CFAssessmentTypeId);
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetStudentsForLOCMarkEntry";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntry beData = new AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntry();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.AcademicYearId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.StudentName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.RollNumber = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.ClassId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.SectionId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.BatchId = reader.GetInt32(7);
					if (!(reader[9] is DBNull)) beData.SemesterId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.ClassYearId = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.SubjectId = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) beData.LessonSno = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.TopicName = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.Marks = reader.GetInt32(14);
					if (!(reader[15] is DBNull)) beData.Remarks = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.AssessmentTypeId = reader.GetInt32(16);
					if (!(reader[17] is DBNull)) beData.AssessmentDate = Convert.ToDateTime(reader[17]);
					if (!(reader[18] is DBNull)) beData.EvaluationAreaId = reader.GetInt32(18);
					dataColl.Add(beData);
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return dataColl;
		}

		public AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntryCollections GetStdWiseLOCMarkEntry(int UserId, int EntityId, int? AcademicYearId, int ClassId, int? SectionId, int SubjectId, int LessonId, int StudentId, int? AssessmentTypeId, int? CFAssessmentTypeId,
			DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntryCollections dataColl = new AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntryCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@StudentId", StudentId);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.Parameters.AddWithValue("@CFAssessmentTypeId", CFAssessmentTypeId);
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetStdWiseLOCMarkEntry";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntry beData = new AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntry();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.AcademicYearId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.LessonSno = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.TopicName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.ClassId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SectionId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.BatchId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.SemesterId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.ClassYearId = reader.GetInt32(7);
					if (!(reader[9] is DBNull)) beData.SubjectId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.EvaluationAreaId = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.Marks = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) beData.Remarks = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.AssessmentTypeId = reader.GetInt32(13);
					if (!(reader[14] is DBNull)) beData.AssessmentDate = Convert.ToDateTime(reader[14]);
					dataColl.Add(beData);
				}
				reader.Close();
				dataColl.IsSuccess = true;
				dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

			}
			catch (Exception ee)
			{
				dataColl.IsSuccess = false;
				dataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}
			return dataColl;
		}

	}
}

