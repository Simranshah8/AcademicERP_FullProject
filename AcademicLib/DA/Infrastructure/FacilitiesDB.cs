using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Infrastructure
{

	internal class FacilitiesDB
	{
		DataAccessLayer1 dal = null;
		public FacilitiesDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.Infrastructure.Facilities beData, bool isModify)
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
			cmd.Parameters.AddWithValue("@FacilitiesId", beData.FacilitiesId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateFacilities";
			}
			else
			{
				cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddFacilities";
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
					SaveSubFacilitiesDetails(beData.CUserId, resVal.RId, beData.SubFacilitiesColl);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int FacilitiesId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@FacilitiesId", FacilitiesId);
			cmd.CommandText = "usp_DelFacilitiesById";
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
		public BE.Infrastructure.FacilitiesCollections getAllFacilities(int UserId, int EntityId)
		{
			BE.Infrastructure.FacilitiesCollections dataColl = new BE.Infrastructure.FacilitiesCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllFacilities";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Infrastructure.Facilities beData = new BE.Infrastructure.Facilities();
					if (!(reader[0] is DBNull)) beData.FacilitiesId = reader.GetInt32(0);
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
		private void SaveSubFacilitiesDetails(int UserId, int FacilitiesId, BE.Infrastructure.SubFacilitiesCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || FacilitiesId == 0)
				return;

			foreach (BE.Infrastructure.SubFacilities beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.Parameters.AddWithValue("@FacilitiesId", FacilitiesId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.Parameters.AddWithValue("@SNo", beData.SNo);
				cmd.Parameters.AddWithValue("@Name", beData.Name);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "usp_AddSubFacilitiesDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.Infrastructure.Facilities getFacilitiesById(int UserId, int EntityId, int FacilitiesId)
		{
			BE.Infrastructure.Facilities beData = new BE.Infrastructure.Facilities();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@FacilitiesId", FacilitiesId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetFacilitiesById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Infrastructure.Facilities();
					if (!(reader[0] is DBNull)) beData.FacilitiesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.IsActive = Convert.ToBoolean(reader[4]);
				}
				reader.NextResult();
				beData.SubFacilitiesColl = new BE.Infrastructure.SubFacilitiesCollections();
				while (reader.Read())
				{
					BE.Infrastructure.SubFacilities det1 = new BE.Infrastructure.SubFacilities();
					if (!(reader[0] is DBNull)) det1.FacilitiesId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.SNo = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.Name = reader.GetString(2);
					beData.SubFacilitiesColl.Add(det1);
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

