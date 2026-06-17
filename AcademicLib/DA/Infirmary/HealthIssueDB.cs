using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class HealthIssueDB
	{
		DataAccessLayer1 dal = null;
		public HealthIssueDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.HealthIssue beData , bool isModify)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@Name",beData.Name);
	cmd.Parameters.AddWithValue("@Severity",beData.Severity);
	cmd.Parameters.AddWithValue("@OrderNo",beData.OrderNo);
	cmd.Parameters.AddWithValue("@Description",beData.Description);
	
	cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
	cmd.Parameters.AddWithValue("@EntityId",beData.EntityId);
	cmd.Parameters.AddWithValue("@HealthIssueId",beData.HealthIssueId);
	
	if (isModify)
	{
		cmd.CommandText = "usp_UpdateHealthIssue";
	}
	else
	{
		cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
		cmd.CommandText = "usp_AddHealthIssue";
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

	public ResponeValues DeleteById(int UserId, int EntityId, int HealthIssueId)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.Parameters.AddWithValue("@HealthIssueId", HealthIssueId);
	cmd.CommandText = "usp_DelHealthIssueById";
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
	public BE.HealthIssueCollections getAllHealthIssue(int UserId, int EntityId)
{
	BE.HealthIssueCollections dataColl = new BE.HealthIssueCollections();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetAllHealthIssue";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			BE.HealthIssue beData = new BE.HealthIssue();
			if (!(reader[0] is DBNull)) beData.HealthIssueId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) beData.Severity = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
			if (!(reader[5] is DBNull)) beData.SeverityTypeName = reader.GetString(5);
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
	public BE.HealthIssue getHealthIssueById(int UserId, int EntityId, int HealthIssueId)
{
	BE.HealthIssue beData = new BE.HealthIssue();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@HealthIssueId", HealthIssueId);
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetHealthIssueById";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			beData = new BE.HealthIssue();
			if (!(reader[0] is DBNull)) beData.HealthIssueId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) beData.Severity = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
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

