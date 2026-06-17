using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Exam.Reporting
{
    public class ExamAnalysisDB
    {
		DataAccessLayer1 dal = null;
		public ExamAnalysisDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public RE.Exam.Reporting.ExamAnalysis GetExamAnalysisData(int UserId, int EntityId)
		{
			RE.Exam.Reporting.ExamAnalysis beDataColl = new RE.Exam.Reporting.ExamAnalysis();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetExamAnalysis";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					RE.Exam.Reporting.ExamPerAnalysis beData = new RE.Exam.Reporting.ExamPerAnalysis();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.EPAAverage = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.EPAPass = reader.GetInt32(3);
					beDataColl.ExamPerAnalysisColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.MarkDistByExam beData = new RE.Exam.Reporting.MarkDistByExam();
					if (!(reader[0] is DBNull)) beData.ExamTypeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ExamName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.MDELowestMark = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.MDEHighestMark = reader.GetInt32(3);
					beDataColl.MarkDistByExamColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.AttendAnalysis beData = new RE.Exam.Reporting.AttendAnalysis();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.AATotal = reader.GetInt32(2);
					beDataColl.AttendAnalysisColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.ReExamMarkAnalysis beData = new RE.Exam.Reporting.ReExamMarkAnalysis();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.REMARegular = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.REMAReExam = reader.GetInt32(3);
					beDataColl.ReExamMarkAnalysisColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.BulkAttend beData = new RE.Exam.Reporting.BulkAttend();
					if (!(reader[0] is DBNull)) beData.ExamTypeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ExamName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.BulkAttendTot = reader.GetInt32(2);
					beDataColl.BulkAttendColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.ETResultDispatch beData = new RE.Exam.Reporting.ETResultDispatch();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ETRDTotal = reader.GetInt32(2);
					beDataColl.ETResultDispatchColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.CWPerAnalysis beData = new RE.Exam.Reporting.CWPerAnalysis();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.CWPAAvg = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.CWPAPass = reader.GetInt32(3);
					beDataColl.CWPerAnalysisColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.ExamAnalysisTP beData = new RE.Exam.Reporting.ExamAnalysisTP();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.PhotoPath = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.TPPer = reader.GetDouble(3);
					if (!(reader[4] is DBNull)) beData.TPSubjectId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.TPSubjectName = reader.GetString(5);
					beDataColl.ExamAnalysisTPColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.CWAchievement beData = new RE.Exam.Reporting.CWAchievement();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.CWANoOfStud = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.CWASubjectName = reader.GetString(5);
					beDataColl.CWAchievementColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.CASPerformanceAnalysisDet.CASPASetup = reader.GetInt32(0);
					if (!(reader[0] is DBNull)) beDataColl.CASPerformanceAnalysisDet.CASPAEntry = reader.GetInt32(0);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.ClassWiseCASSum beData = new RE.Exam.Reporting.ClassWiseCASSum();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.CWCASTot = reader.GetInt32(2);
					beDataColl.ClassWiseCASSumColl.Add(beData);
				}
				beDataColl.IsSuccess = true;
				beDataColl.ResponseMSG = "Data Retrieved Successfully.";
				reader.Close();

			}
			catch (Exception ee)
			{
				beDataColl.IsSuccess = false;
				beDataColl.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return beDataColl;

		}
	}
}