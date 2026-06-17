using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class CheckupGroupDB
	{
		DataAccessLayer1 dal = null;
		public CheckupGroupDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.CheckupGroup beData , bool isModify)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@Name",beData.Name);
	cmd.Parameters.AddWithValue("@Description",beData.Description);
	cmd.Parameters.AddWithValue("@ShowinStudentInfirmary",beData.ShowinStudentInfirmary);
	cmd.Parameters.AddWithValue("@StudentInfirmaryOrderNo",beData.StudentInfirmaryOrderNo);
	cmd.Parameters.AddWithValue("@ShowinEmployeeInfirmary",beData.ShowinEmployeeInfirmary);
	cmd.Parameters.AddWithValue("@EmployeeInfirmaryOrderNo",beData.EmployeeInfirmaryOrderNo);	
	cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
	cmd.Parameters.AddWithValue("@EntityId",beData.EntityId);
	cmd.Parameters.AddWithValue("@CheckupGroupId",beData.CheckupGroupId);	
	if (isModify)
	{
		cmd.CommandText = "usp_UpdateCheckupGroup";
	}
	else
	{
		cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
		cmd.CommandText = "usp_AddCheckupGroup";
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

	public ResponeValues DeleteById(int UserId, int EntityId, int CheckupGroupId)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.Parameters.AddWithValue("@CheckupGroupId", CheckupGroupId);
	cmd.CommandText = "usp_DelCheckupGroupById";
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
	public BE.CheckupGroupCollections getAllCheckupGroup(int UserId, int EntityId)
{
	BE.CheckupGroupCollections dataColl = new BE.CheckupGroupCollections();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetAllCheckupGroup";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			BE.CheckupGroup beData = new BE.CheckupGroup();
			if (!(reader[0] is DBNull)) beData.CheckupGroupId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
			if (!(reader[3] is DBNull)) beData.ShowinStudentInfirmary = Convert.ToBoolean(reader[3]);
			if (!(reader[4] is DBNull)) beData.StudentInfirmaryOrderNo = reader.GetInt32(4);
			if (!(reader[5] is DBNull)) beData.ShowinEmployeeInfirmary = Convert.ToBoolean(reader[5]);
			if (!(reader[6] is DBNull)) beData.EmployeeInfirmaryOrderNo = reader.GetInt32(6);
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
	public BE.CheckupGroup getCheckupGroupById(int UserId, int EntityId, int CheckupGroupId)
{
	BE.CheckupGroup beData = new BE.CheckupGroup();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@CheckupGroupId", CheckupGroupId);
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetCheckupGroupById";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			beData = new BE.CheckupGroup();
			if (!(reader[0] is DBNull)) beData.CheckupGroupId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) beData.Description = reader.GetString(2);
			if (!(reader[3] is DBNull)) beData.ShowinStudentInfirmary = Convert.ToBoolean(reader[3]);
			if (!(reader[4] is DBNull)) beData.StudentInfirmaryOrderNo = reader.GetInt32(4);
			if (!(reader[5] is DBNull)) beData.ShowinEmployeeInfirmary = Convert.ToBoolean(reader[5]);
			if (!(reader[6] is DBNull)) beData.EmployeeInfirmaryOrderNo = reader.GetInt32(6);
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

