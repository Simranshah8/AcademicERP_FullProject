using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Exam.Transaction
{
	internal class ClassSummaryDB
	{

		DataAccessLayer1 dal = null;
		public ClassSummaryDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public AcademicLib.BE.Exam.Transaction.ClassSummaryCollections getViewClassSummaryDetails(int UserId, int EntityId, int? ClassId,int? SectionId, int? SubjectId, DateTime DateFrom, DateTime DateTo, int? BatchId, int? ClassYearId, int? SemesterId)
		{
			AcademicLib.BE.Exam.Transaction.ClassSummaryCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummaryCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
			cmd.Parameters.AddWithValue("@DateTo", DateTo);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.CommandText = "usp_GetViewClassSummaryDetails";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ClassSummary beData = new AcademicLib.BE.Exam.Transaction.ClassSummary();
					//if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[0] is DBNull)) beData.TestDate = Convert.ToDateTime(reader[0]);
					if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.LessonSno = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.LessonName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.EmployeeId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.EmployeeName = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.FullMarks = Convert.ToDouble(reader[7]);
					if (!(reader[8] is DBNull)) beData.PassMarks = Convert.ToDouble(reader[8]);
					if (!(reader[9] is DBNull)) beData.PresentStudent = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.TotalStudent = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.TestDateBS = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.TotalPassStudent = reader.GetInt32(12);
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


		public AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWiseCollections getClassSummarySubjectClassWise(int UserId, int EntityId, int? ClassId)
		{
			AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWiseCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.CommandText = "usp_GetClassSummarySubjectClassWise";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWise beData = new AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWise();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
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


		public AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections getClassSummaryDetailsById(int UserId, int EntityId, int AcademicYearId, int ClassId, int EmployeeId, int SubjectId, int? LessonSno, DateTime TestDate, int? BatchId, int? ClassYearId, int? SemesterId)
		{
			AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@LessonSno", LessonSno);
			cmd.Parameters.AddWithValue("@TestDate", TestDate);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.CommandText = "usp_GetClassSummaryDetailsById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsById beData = new AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsById();
					beData.TranId = reader.GetInt32(0);
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.RollNo = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.BoardRegdNumber = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.StudentName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.ObtMarks = Convert.ToDouble(reader[6]);
					if (!(reader[7] is DBNull)) beData.SubjectId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.ClassId = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.SubjectName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.ClassName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.SectionName = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.IsAbsent = Convert.ToBoolean(reader[12]);
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

