using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{
	internal class ExamCenterDB
	{
		DataAccessLayer1 dal = null;
		public ExamCenterDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Scholarship.ExamCenter beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@Address", beData.Address);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
			cmd.Parameters.AddWithValue("@Latitude", beData.Latitude);
			cmd.Parameters.AddWithValue("@Longitude", beData.Longitude);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@ExamCenterId", beData.ExamCenterId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateExamCenter";
			}
			else
			{
				cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddExamCenter";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[10].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[10].Value);

				if (!(cmd.Parameters[11].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[11].Value);

				if (!(cmd.Parameters[12].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[12].Value);

				if (!(cmd.Parameters[13].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[13].Value);

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

		public ResponeValues DeleteById(int UserId, int EntityId, int ExamCenterId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ExamCenterId", ExamCenterId);
			cmd.CommandText = "usp_DelExamCenterById";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[3].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

				if (!(cmd.Parameters[4].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

				if (!(cmd.Parameters[5].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

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
		public BE.Scholarship.ExamCenterCollections getAllExamCenter(int UserId, int EntityId)
		{
			BE.Scholarship.ExamCenterCollections dataColl = new BE.Scholarship.ExamCenterCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllExamCenter";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.ExamCenter beData = new BE.Scholarship.ExamCenter();
					if (!(reader[0] is DBNull)) beData.ExamCenterId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Email = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Description = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.OrderNo = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.Latitude = Convert.ToDouble(reader[7]);
					if (!(reader[8] is DBNull)) beData.Longitude = Convert.ToDouble(reader[8]);
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
		public BE.Scholarship.ExamCenter getExamCenterById(int UserId, int EntityId, int ExamCenterId)
		{
			BE.Scholarship.ExamCenter beData = new BE.Scholarship.ExamCenter();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ExamCenterId", ExamCenterId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetExamCenterById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Scholarship.ExamCenter();
					if (!(reader[0] is DBNull)) beData.ExamCenterId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Email = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Description = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.OrderNo = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.Latitude = Convert.ToDouble(reader[7]);
					if (!(reader[8] is DBNull)) beData.Longitude = Convert.ToDouble(reader[8]);
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

