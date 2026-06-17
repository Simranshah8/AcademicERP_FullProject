using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Homework.Reporting
{
    public class HADashboard
    {
        DataAccessLayer1 dal = null;
        public HADashboard(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

		public RE.Homework.Reporting.HADashboard getDashboardData(int UserId, int EntityId)
		{
			RE.Homework.Reporting.HADashboard beDataColl = new RE.Homework.Reporting.HADashboard();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetHADashboardData";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					if (!(reader[0] is DBNull)) beDataColl.HomeworkColl.TotalHW = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.HomeworkColl.THSubmission = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beDataColl.HomeworkColl.THPending = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beDataColl.HomeworkColl.TSHWOnTime = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beDataColl.HomeworkColl.TSHWLate = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beDataColl.HomeworkColl.PSHWDueToday = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beDataColl.HomeworkColl.PSHWPastDue = reader.GetInt32(6);
				}
				reader.NextResult();
                while (reader.Read())
                {
					if (!(reader[0] is DBNull)) beDataColl.AssignmentColl.TotalAssign = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beDataColl.AssignmentColl.TASubmission = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beDataColl.AssignmentColl.TAPending = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beDataColl.AssignmentColl.TSAssignOnTime = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beDataColl.AssignmentColl.TSAssignLate = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beDataColl.AssignmentColl.PSAssignDueToday = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beDataColl.AssignmentColl.PSAssignPastDue = reader.GetInt32(6);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.TeacherWiseHW beData = new RE.Homework.Reporting.TeacherWiseHW();
					if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TeacherName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.AssignedToday = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SubmittedToday = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.Pending = reader.GetInt32(6);
					if (!(reader[6] is DBNull)) beData.Overdue = reader.GetInt32(7);
					beDataColl.TeacherWiseHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.TeacherWiseAssignment beData = new RE.Homework.Reporting.TeacherWiseAssignment();
					if (!(reader[0] is DBNull)) beData.EmployeeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.TeacherName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.SubjectId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.AssignedToday = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SubmittedToday = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.Pending = reader.GetInt32(6);
					if (!(reader[6] is DBNull)) beData.Overdue = reader.GetInt32(7);
					beDataColl.TeacherWiseAssignColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.OverAndUpcommingdueHW beData = new RE.Homework.Reporting.OverAndUpcommingdueHW();
					if (!(reader[0] is DBNull)) beData.ClasId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.PastDeadline = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.UpcommingDue = reader.GetInt32(3);
					beDataColl.OverAndUpcommingdueHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.OverAndUpcommingdueAssignment beData = new RE.Homework.Reporting.OverAndUpcommingdueAssignment();
					if (!(reader[0] is DBNull)) beData.ClasId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.PastDeadline = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.UpcommingDue = reader.GetInt32(3);
					beDataColl.OverAndUpcommingdueAssignColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.SubmissionStatHW beData = new RE.Homework.Reporting.SubmissionStatHW();

					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.SubjectId = reader.GetInt32(6);
					if (!(reader[6] is DBNull)) beData.SubjectName = reader.GetString(7);
					if (!(reader[3] is DBNull)) beData.HomeWorkId = reader.GetInt32(8);
					if (!(reader[4] is DBNull)) beData.HWStatus = reader.GetString(9);
					if (!(reader[5] is DBNull)) beData.FullMark = reader.GetInt32(10);
					if (!(reader[6] is DBNull)) beData.ObtainedMark = reader.GetInt32(11);
					beDataColl.SubmissionStatHWColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					RE.Homework.Reporting.SubmissionStatAssignment beData = new RE.Homework.Reporting.SubmissionStatAssignment();

					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.SectionId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[3] is DBNull)) beData.AssignmentId = reader.GetInt32(6);
					if (!(reader[6] is DBNull)) beData.SubjectId = reader.GetInt32(7);
					if (!(reader[6] is DBNull)) beData.SubjectName = reader.GetString(8);
					if (!(reader[4] is DBNull)) beData.AssignmentStatus = reader.GetString(9);
					if (!(reader[5] is DBNull)) beData.FullMark = reader.GetInt32(10);
					if (!(reader[6] is DBNull)) beData.ObtainedMark = reader.GetInt32(11);
					beDataColl.SubmissionStatAssignColl.Add(beData);
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