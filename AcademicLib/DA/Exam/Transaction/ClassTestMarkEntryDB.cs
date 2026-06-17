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

	internal class ClassTestMarkEntryDB
	{
		DataAccessLayer1 dal = null;
		public ClassTestMarkEntryDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}		
		public ResponeValues SaveUpdate(int UserId, BE.Exam.Transaction.ClassTestMarkEntryCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			dal.BeginTransaction();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			try
			{
				foreach (var beData in dataColl)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
					cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
					cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
					cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
					cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
					cmd.Parameters.AddWithValue("@LessonSNo", beData.LessonSNo);
					cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
					cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
					cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
					cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
					cmd.Parameters.AddWithValue("@TestDate", beData.TestDate);
					cmd.Parameters.AddWithValue("@FullMarks", beData.FullMarks);
					cmd.Parameters.AddWithValue("@PassMarks", beData.PassMarks);
					cmd.Parameters.AddWithValue("@ObtMarks", beData.ObtMarks);
					cmd.Parameters.AddWithValue("@IsAbsent", beData.IsAbsent);
					cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
					cmd.Parameters.AddWithValue("@UserId", UserId);	
					cmd.CommandText = "usp_AddClassTestMarkEntry";
					cmd.ExecuteNonQuery();
				}
				dal.CommitTransaction();
				resVal.IsSuccess = true;
				resVal.ResponseMSG = "Mark Entry Updated Successfully";
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

		public AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections getStudentsDetailsClassWise(int UserId, int EntityId, int AcademicYearId, int ClassId, int? SectionId, int SubjectId, bool FilterSection, int? LessonSNo, DateTime TestDate, int EmployeeId, int? BatchId, int? SemesterId, int? ClassYearId)
		{
			AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@FilterSection", FilterSection);
			cmd.Parameters.AddWithValue("@LessonSNo", LessonSNo);
			cmd.Parameters.AddWithValue("@TestDate", TestDate);
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.CommandText = "usp_GetStudentsForCTMarkEntry";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWise beData = new AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWise();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SectionId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.RollNumber = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.StudentName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.BoardRegdNumber = reader.GetString(6);					
					if (!(reader[7] is DBNull)) beData.FullMarks = Convert.ToDouble(reader[7]);
					if (!(reader[8] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[8]);
					if (!(reader[9] is DBNull)) beData.ObtainMarks = Convert.ToDouble(reader[9]);
					if (!(reader[10] is DBNull)) beData.RemarksType = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.IsAbsent = Convert.ToBoolean(reader[11]);
					if (!(reader[12] is DBNull)) beData.ClassId = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.BatchId = reader.GetInt32(13);
					if (!(reader[14] is DBNull)) beData.SemesterId = reader.GetInt32(14);
					if (!(reader[15] is DBNull)) beData.ClassYearId = reader.GetInt32(15);

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
		
		public AcademicLib.BE.Exam.Transaction.CSubjectWiseLessonCollections getCSubjectWiseLesson(int UserId, int EntityId, int? SubjectId)
		{
			AcademicLib.BE.Exam.Transaction.CSubjectWiseLessonCollections dataColl = new AcademicLib.BE.Exam.Transaction.CSubjectWiseLessonCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.CommandText = "usp_GetCSubjectWiseLesson";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.CSubjectWiseLesson beData = new AcademicLib.BE.Exam.Transaction.CSubjectWiseLesson();
					if (!(reader[0] is DBNull)) beData.LessonId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.LessonName = reader.GetString(1);
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

