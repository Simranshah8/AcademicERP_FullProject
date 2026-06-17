using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance
{
	internal class AttendanceConfigDB
	{
		DataAccessLayer1 dal = null;
		public AttendanceConfigDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Attendance.AttendanceConfig beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@MonthlyMinAttendance", beData.MonthlyMinAttendance);
			cmd.Parameters.AddWithValue("@MaxConsecutiveAbs", beData.MaxConsecutiveAbs);
			cmd.Parameters.AddWithValue("@AbsFinePerDay", beData.AbsFinePerDay);
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);
			if (isModify)
			{
				cmd.CommandText = "usp_UpdateAttendanceConfig";
			}
			else
			{
				cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddAttendanceConfig";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[5].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

				if (!(cmd.Parameters[6].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

				if (!(cmd.Parameters[7].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

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


		public BE.Attendance.AttendanceConfig getAttendanceConfig(int UserId, int EntityId)
		{
			BE.Attendance.AttendanceConfig beData = new BE.Attendance.AttendanceConfig();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllAttendanceConfig";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Attendance.AttendanceConfig();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.MonthlyMinAttendance = Convert.ToDouble(reader[1]);
					if (!(reader[2] is DBNull)) beData.MaxConsecutiveAbs = Convert.ToDouble(reader[2]);
					if (!(reader[3] is DBNull)) beData.AbsFinePerDay = Convert.ToDouble(reader[3]);

				}
				reader.Close();
				beData.IsSuccess = true;
				beData.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				beData.IsSuccess = false;
				beData.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return beData;

		}

		

	}

}

