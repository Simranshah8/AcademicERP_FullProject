using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class CopyExamTypeDB
	{
		DataAccessLayer1 dal = null;
		public CopyExamTypeDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.CopyExamType beData , bool isModify)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@FromExamTermId",beData.FromExamTermId);
	cmd.Parameters.AddWithValue("@ToExamTermId",beData.ToExamTermId);
	
	cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
	cmd.Parameters.AddWithValue("@EntityId",beData.EntityId);
	cmd.Parameters.AddWithValue("@CopyExamTypeId",beData.CopyExamTypeId);
	
	if (isModify)
	{
		cmd.CommandText = "usp_UpdateCopyExamType";
	}
	else
	{
		cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
		cmd.CommandText = "usp_AddCopyExamType";
	}
	cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
	cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
	cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
	cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
	cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
	cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
	try
	{
		cmd.ExecuteNonQuery();
		if (!(cmd.Parameters[4].Value is DBNull))
			resVal.RId = Convert.ToInt32(cmd.Parameters[4].Value);

		if (!(cmd.Parameters[5].Value is DBNull))
			resVal.ResponseMSG = Convert.ToString(cmd.Parameters[5].Value);

		if (!(cmd.Parameters[6].Value is DBNull))
			resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[6].Value);

		if (!(cmd.Parameters[7].Value is DBNull))
			resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[7].Value);

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

	public ResponeValues DeleteById(int UserId, int EntityId, int CopyExamTypeId)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.Parameters.AddWithValue("@CopyExamTypeId", CopyExamTypeId);
	cmd.CommandText = "usp_DelCopyExamTypeById";
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
	public BE.CopyExamTypeCollections getAllCopyExamType(int UserId, int EntityId)
{
	BE.CopyExamTypeCollections dataColl = new BE.CopyExamTypeCollections();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetAllCopyExamType";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			BE.CopyExamType beData = new BE.CopyExamType();
			if (!(reader[0] is DBNull)) beData.CopyExamTypeId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.FromExamTermId = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) beData.ToExamTermId = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.LogDateTime = Convert.ToDateTime(reader[3]);
			if (!(reader[4] is DBNull)) beData.FromExamName = reader.GetString(4);
			if (!(reader[5] is DBNull)) beData.ToExamName = reader.GetString(5);
			if (!(reader[6] is DBNull)) beData.CopyBy = reader.GetString(6);
			
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
	public BE.CopyExamType getCopyExamTypeById(int UserId, int EntityId, int CopyExamTypeId)
{
	BE.CopyExamType beData = new BE.CopyExamType();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@CopyExamTypeId", CopyExamTypeId);
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetCopyExamTypeById";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			beData = new BE.CopyExamType();
			if (!(reader[0] is DBNull)) beData.CopyExamTypeId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.FromExamTermId = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) beData.ToExamTermId = reader.GetInt32(2);
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

