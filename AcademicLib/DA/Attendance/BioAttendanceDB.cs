using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance
{
	internal class BioAttendenceDB
	{
		DataAccessLayer1 dal = null;

		public BioAttendenceDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Attendance.BioAttendenceCollections dataColl)
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

					cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
					cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
					cmd.Parameters.AddWithValue("@AttendenceMode", beData.AttendenceMode);
					cmd.Parameters.AddWithValue("@InOutTime", beData.InOutTime);
					cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
					cmd.Parameters.AddWithValue("@selectedFor", beData.selectedFor);
					cmd.Parameters.AddWithValue("@AttendenceDate", beData.AttendenceDate);

					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
					cmd.Parameters.Add("@TranId", System.Data.SqlDbType.Int);


					cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
					cmd.CommandText = "usp_AddBioAttendence";

					cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
					cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
					cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
					cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
					cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
					cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;

					try
					{
						cmd.ExecuteNonQuery();
						if (!(cmd.Parameters[9].Value is DBNull))
							resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

						if (!(cmd.Parameters[10].Value is DBNull))
							resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

						if (!(cmd.Parameters[11].Value is DBNull))
							resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

						if (!(cmd.Parameters[12].Value is DBNull))
							resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

						if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
							resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

					}
					catch (System.Data.SqlClient.SqlException ee)
					{
						resVal.IsSuccess = false;
						resVal.ResponseMSG = ee.Message;
					}


				}
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
		public BE.Attendance.BioAttendenceCollections getAllBioAttendence(int UserId, int? StudentId, int? EmployeeId)
		{
			BE.Attendance.BioAttendenceCollections dataColl = new BE.Attendance.BioAttendenceCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@StudentId", StudentId);
			cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

			cmd.CommandText = "usp_GetAllBioAttendence";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Attendance.BioAttendence beData = new BE.Attendance.BioAttendence();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.EmployeeId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.AttendenceMode = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) beData.InOutTime = Convert.ToDateTime(reader[4]);
					if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.selectedFor = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.AttendenceDate = Convert.ToDateTime(reader[7]);

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

