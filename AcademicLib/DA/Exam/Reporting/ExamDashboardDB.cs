using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Exam.Reporting
{
    public class ExamDashboardDB
    {
		DataAccessLayer1 dal = null;
		public ExamDashboardDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public RE.Exam.Reporting.ExamDashboard GetExamDashboardData(int UserId, int EntityId)
		{
			RE.Exam.Reporting.ExamDashboard beDataColl = new RE.Exam.Reporting.ExamDashboard();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetExamDashboard";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.QuoteDet.QuotesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.QuoteDet.Title = reader.GetString(1);
					if (!(reader[2] is DBNull)) beDataColl.QuoteDet.ImagePath = reader.GetString(2);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.ResultSumDet.RSTotalRes = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.ResultSumDet.RSPublished = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beDataColl.ResultSumDet.RSPending = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beDataColl.ResultSumDet.UERThisWeek = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beDataColl.ResultSumDet.UERThisMonth = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beDataColl.ResultSumDet.RETotal = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beDataColl.ResultSumDet.REThisWeek = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beDataColl.ResultSumDet.REThisMonth = reader.GetInt32(7);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.ExamCompVsPendDet.ExamComVsPenC = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.ExamCompVsPendDet.ExamComVsPenP = reader.GetInt32(1);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.TotExamSch beData = new RE.Exam.Reporting.TotExamSch();
					if (!(reader[0] is DBNull)) beData.ExamTypeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ExamName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.TotalExamSch = reader.GetInt32(2);
					beDataColl.TotExamSchColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.MarkEntryStatDet.MarkEntryStatComp = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.MarkEntryStatDet.MarkEntryStatPend = reader.GetInt32(1);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.ClassWiseAttendSumm beData = new RE.Exam.Reporting.ClassWiseAttendSumm();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.CWASPresent = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.CWASAbsent = reader.GetInt32(3);
					beDataColl.ClassWiseAttendSumColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.CWPendReExam beData = new RE.Exam.Reporting.CWPendReExam();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.CWPendReExamNo = reader.GetInt32(2);
					beDataColl.CWPendReExamColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.SWPendReExam beData = new RE.Exam.Reporting.SWPendReExam();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SWReExamPend = reader.GetInt32(2);
					beDataColl.SWPendReExamsColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.UpcommingExam beData = new RE.Exam.Reporting.UpcommingExam();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ExamMitti = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Subjects = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Teachers = reader.GetString(4);
					beDataColl.UpcommingExamColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.TopPerformer beData = new RE.Exam.Reporting.TopPerformer();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Remark = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.PhotoPath = reader.GetString(7);
					beDataColl.TopPerformerColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.Award beData = new RE.Exam.Reporting.Award();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Remark = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.PhotoPath = reader.GetString(7);
					beDataColl.AwardColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.CTConducted beData = new RE.Exam.Reporting.CTConducted();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.TotalCT = reader.GetInt32(2);
					beDataColl.CTConductedColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.CASMarkStatDet.CASSetup = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.CASMarkStatDet.CASEntry = reader.GetInt32(1);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Exam.Reporting.ExamResultStat beData = new RE.Exam.Reporting.ExamResultStat();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ERSTotal = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ERSCompleted = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.ERSPending = reader.GetInt32(4);
					beDataColl.ExamResultStatColl.Add(beData);
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