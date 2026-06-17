using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.DA.Exam.Transaction
{
	internal class ThemeMarkEntryDB
    {
		DataAccessLayer1 dal = null;
		public ThemeMarkEntryDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		
		}
		public ResponeValues SaveUpdate(int UserId, List<BE.Exam.Transaction.ThemeMarkEntry> dataColl)
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

					cmd.Parameters.AddWithValue("@TranId", beData.TranId);
					cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
					cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
					cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
					cmd.Parameters.AddWithValue("@LessonSno", beData.LessonSno);
					cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
					cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
					cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
					cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
					cmd.Parameters.AddWithValue("@AssessmentTypeId", beData.AssessmentTypeId);
					cmd.Parameters.AddWithValue("@AssessmentDate", beData.AssessmentDate);
					cmd.Parameters.AddWithValue("@AchievementNo", beData.AchievementNo);
					cmd.Parameters.AddWithValue("@FullMarks", beData.FullMarks);
					cmd.Parameters.AddWithValue("@ObtainMarks", beData.ObtainMarks);
					cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
					cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.CommandText = "usp_AddThemeMarkEntry";
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


		public AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntryCollections GetThemeWiseMarkEntry(int UserId, int EntityId, int? AcademicYearId, int ClassId, int? SectionId, int SubjectId, int LessonId, int AssessmentTypeId, int? CFAssessmentTypeId,
			DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntryCollections dataColl = new AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntryCollections();
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
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.Parameters.AddWithValue("@CFAssessmentTypeId", CFAssessmentTypeId);
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetThemeWiseMarkEntry";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntry beData = new AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntry();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.AcademicYearId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.StudentName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.ClassId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.SectionId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.BatchId = reader.GetInt32(7);
					if (!(reader[9] is DBNull)) beData.SemesterId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.ClassYearId = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.SubjectId = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) beData.LessonSno = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.AchievementNo = Convert.ToDouble(reader[13]);
					if (!(reader[14] is DBNull)) beData.FullMarks = Convert.ToDouble(reader[14]);
					if (!(reader[15] is DBNull)) beData.ObtainMarks = Convert.ToDouble(reader[15]);
					if (!(reader[16] is DBNull)) beData.Remarks = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.AssessmentTypeId = reader.GetInt32(17);
					if (!(reader[18] is DBNull)) beData.AssessmentDate = Convert.ToDateTime(reader[18]);
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

		public ResponeValues DelThemeMarkEntry(int UserId, int EntityId, int ClassId, int? SectionId, int SubjectId, int LessonId, int? AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonId", LessonId);
			cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
			cmd.Parameters.AddWithValue("@AssessmentTypeId", AssessmentTypeId);
			cmd.CommandText = "usp_DelThemeMarkEntry";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
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


	}
}
