using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{

	internal class AttendanceFollowUpDB
	{
		DataAccessLayer1 dal = null;
		public AttendanceFollowUpDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUp beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
			cmd.Parameters.AddWithValue("@FollowUpDate", beData.FollowUpDate);
			cmd.Parameters.AddWithValue("@FollowUpTo", beData.FollowUpTo);
			cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
			cmd.Parameters.AddWithValue("@FollowUpStatus", beData.FollowUpStatus);
			cmd.Parameters.AddWithValue("@FollowUpRemarks", beData.FollowUpRemarks);
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);


			if (isModify)
			{
				cmd.CommandText = "usp_UpdateAttendanceFollowUp";
			}
			else
			{
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddAttendanceFollowUp";

			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[8].Value);

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

		public RE.FrontDesk.AttendanceFollowupCollections getStudentAttendanceFollowup(int UserId, int? StudentId)
		{
			RE.FrontDesk.AttendanceFollowupCollections dataColl = new RE.FrontDesk.AttendanceFollowupCollections();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@StudentId", StudentId);
			cmd.CommandText = "usp_GetStudentAttendanceFollowupList";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					RE.FrontDesk.AttendanceFollowup beData = new RE.FrontDesk.AttendanceFollowup();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.FollowupMiti = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.FollowUpTo = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.ContactNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.FollowUpStatus = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.FollowUpRemarks = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.FollowupBy = reader.GetString(6);

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

		public AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUpColl getAllAttendanceFollowUp(int UserId, DateTime? DateFrom, DateTime? DateTo,int? ClassId, int? SectionId,int? AcademicYearId, int? BatchId, int? SemesterId, int? ClassYearId, int? ClassShiftId)
		{
			AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUpColl dataColl = new AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUpColl();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
			cmd.Parameters.AddWithValue("@DateTo", DateTo);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId); 
			cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.Parameters.AddWithValue("@ClassShiftId", ClassShiftId);
			cmd.CommandText = "usp_GetStudentAttendanceFollowup";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUp beData = new AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUp();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Batch = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.ClassYear = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.Semester = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.FollowUpDate = reader.GetDateTime(9);
					if (!(reader[10] is DBNull)) beData.FollowUpTo = reader.GetInt32(10);
					if (!(reader[11] is DBNull)) beData.ContactNo = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.FollowUpStatus = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) beData.FollowUpRemarks = reader.GetString(13);
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

