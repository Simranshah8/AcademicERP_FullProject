using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Infrastructure
{

	internal class UtilitiesDB
	{
		DataAccessLayer1 dal = null;
		public UtilitiesDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Infrastructure.Utilities beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
			cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@UtilitiesId", beData.UtilitiesId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateUtilities";
			}
			else
			{
				cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddUtilities";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[6].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

				if (!(cmd.Parameters[7].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveSubUtilitiesDetails(beData.CUserId, resVal.RId, beData.SubUtilitiesColl);
				}
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

		public ResponeValues DeleteById(int UserId, int EntityId, int UtilitiesId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@UtilitiesId", UtilitiesId);
			cmd.CommandText = "usp_DelUtilitiesById";
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
		public BE.Infrastructure.UtilitiesCollections getAllUtilities(int UserId, int EntityId)
		{
			BE.Infrastructure.UtilitiesCollections dataColl = new BE.Infrastructure.UtilitiesCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllUtilities";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Infrastructure.Utilities beData = new BE.Infrastructure.Utilities();
					if (!(reader[0] is DBNull)) beData.UtilitiesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[4]);
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
		private void SaveSubUtilitiesDetails(int UserId, int UtilitiesId, BE.Infrastructure.SubUtilitiesCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || UtilitiesId == 0)
				return;

			foreach (BE.Infrastructure.SubUtilities beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.Parameters.AddWithValue("@UtilitiesId", UtilitiesId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.Parameters.AddWithValue("@SNo", beData.SNo);
				cmd.Parameters.AddWithValue("@Name", beData.Name);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "usp_AddSubUtilitiesDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.Infrastructure.Utilities getUtilitiesById(int UserId, int EntityId, int UtilitiesId)
		{
			BE.Infrastructure.Utilities beData = new BE.Infrastructure.Utilities();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UtilitiesId", UtilitiesId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetUtilitiesById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Infrastructure.Utilities();
					if (!(reader[0] is DBNull)) beData.UtilitiesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[4]);
				}
				reader.NextResult();
				beData.SubUtilitiesColl = new BE.Infrastructure.SubUtilitiesCollections();
				while (reader.Read())
				{
					BE.Infrastructure.SubUtilities det1 = new BE.Infrastructure.SubUtilities();
					if (!(reader[0] is DBNull)) det1.UtilitiesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.SNo = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);
					beData.SubUtilitiesColl.Add(det1);
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

