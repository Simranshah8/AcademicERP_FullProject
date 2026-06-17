using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Homework.Reporting
{
    public class HAAnalysisDB
    {
		DataAccessLayer1 dal = null;
		public HAAnalysisDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public RE.Homework.Reporting.HAAnalysis getAnalysisData(int UserId, int EntityId)
		{
			RE.Homework.Reporting.HAAnalysis beDataColl = new RE.Homework.Reporting.HAAnalysis();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetHAAnalysisData";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					RE.Homework.Reporting.TotalHW beData = new RE.Homework.Reporting.TotalHW();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.THSubmitted = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.THPending = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.THOverdue = reader.GetInt32(4);
					beDataColl.TotalHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.TotalAssign beData = new RE.Homework.Reporting.TotalAssign();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.TASubmitted = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.TAPending = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.TAOverdue = reader.GetInt32(4);
					beDataColl.TotalAssignColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.CSWSummaryHW beData = new RE.Homework.Reporting.CSWSummaryHW();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.CSWTH = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.CSWTHSubmitted = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.CSWTHPending = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.CSWTHOverdue = reader.GetInt32(7);
					beDataColl.CSWSummaryHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.CSWSummaryAssign beData = new RE.Homework.Reporting.CSWSummaryAssign();
					if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.CSWTA = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.CSWTASubmitted = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.CSWTAPending = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.CSWTAOverdue = reader.GetInt32(7);
					beDataColl.CSWSummaryAssignColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.TeachPerfomanceHW beData = new RE.Homework.Reporting.TeachPerfomanceHW();
					if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TeacherName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.PhotoPath = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.TPTHGiven = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.TPTHOverdue = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.TPTHSubmittedPct = Convert.ToDouble(reader.GetDecimal(5)); ;
					beDataColl.TPHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.subVsOverdueHW.HWOverdueByTeach = Convert.ToDouble(reader.GetDecimal(0)); ;
					if (!(reader[1] is DBNull)) beDataColl.subVsOverdueHW.HWSubByTeach = Convert.ToDouble(reader.GetDecimal(1)); ;
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.TeachPerformanceAssign beData = new RE.Homework.Reporting.TeachPerformanceAssign();
					if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TeacherName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.PhotoPath = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.TPTAGiven = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.TPTAOverdue = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.TPTASubmittedPct = Convert.ToDouble(reader.GetDecimal(5)); ;
					beDataColl.TPAColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.subVsOverdueAssign.AssignOverdueByTeach = Convert.ToDouble(reader.GetDecimal(0)); ;
					if (!(reader[1] is DBNull)) beDataColl.subVsOverdueAssign.AssignSubByTeach = Convert.ToDouble(reader.GetDecimal(1)); ;
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.SPRHW beData = new RE.Homework.Reporting.SPRHW();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SPRTotal = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SPRSubmittedPct = Convert.ToDouble(reader.GetDecimal(3));
					if (!(reader[4] is DBNull)) beData.SPRSubmitted = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SPRAvgMark = Convert.ToDouble(reader.GetDecimal(5));
					if (!(reader[6] is DBNull)) beData.SPRFullMark = reader.GetInt32(6);
					beDataColl.SPRHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.SPRAssign beData = new RE.Homework.Reporting.SPRAssign();
					if (!(reader[0] is DBNull)) beData.SubjectId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SPRTotal = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SPRSubmittedPct = Convert.ToDouble(reader.GetDecimal(3));
					if (!(reader[4] is DBNull)) beData.SPRSubmitted = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SPRAvgMark = Convert.ToDouble(reader.GetDecimal(5));
					if (!(reader[6] is DBNull)) beData.SPRFullMark = reader.GetInt32(6);
					beDataColl.SPRAssignColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.TopPerformer beData = new RE.Homework.Reporting.TopPerformer();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.PhotoPath = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.AvgMarks = Convert.ToDouble(reader.GetDecimal(7)); ;
					if (!(reader[8] is DBNull)) beData.TotalMarks = Convert.ToDouble(reader.GetDecimal(8)); ;
                    beDataColl.TopPerformerColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.StudentNeedAttent beData = new RE.Homework.Reporting.StudentNeedAttent();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.PhotoPath = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.AvgMarks = Convert.ToDouble(reader.GetDecimal(7)); ;
					if (!(reader[8] is DBNull)) beData.TotalMarks = Convert.ToDouble(reader.GetDecimal(8)); ;
					beDataColl.studentNeedAttent.Add(beData);
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