using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class TestNameDB
	{

		DataAccessLayer1 dal = null;
		public TestNameDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.TestName beData , bool isModify)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@Name",beData.Name);
	cmd.Parameters.AddWithValue("@CheckupGroupId",beData.CheckupGroupId);
	cmd.Parameters.AddWithValue("@OrderNo",beData.OrderNo);
	cmd.Parameters.AddWithValue("@InputTextType",beData.InputTextType);
	cmd.Parameters.AddWithValue("@SampleCollection",beData.SampleCollection);
	cmd.Parameters.AddWithValue("@SampleUnit",beData.SampleUnit);
	cmd.Parameters.AddWithValue("@Description",beData.Description);
	
	cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
	cmd.Parameters.AddWithValue("@EntityId",beData.EntityId);
	cmd.Parameters.AddWithValue("@TestNameId",beData.TestNameId);
	
	if (isModify)
	{
		cmd.CommandText = "usp_UpdateTestName";
	}
	else
	{
		cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
		cmd.CommandText = "usp_AddTestName";
	}
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

	if (resVal.RId > 0 && resVal.IsSuccess)
			{
				SaveTestNameLabDetailDetails(beData.CUserId, resVal.RId, beData.TestNameLabDetailColl);
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

	public ResponeValues DeleteById(int UserId, int EntityId, int TestNameId)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.Parameters.AddWithValue("@TestNameId", TestNameId);
	cmd.CommandText = "usp_DelTestNameById";
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
	public BE.TestNameCollections getAllTestName(int UserId, int EntityId)
{
	BE.TestNameCollections dataColl = new BE.TestNameCollections();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetAllTestName";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			BE.TestName beData = new BE.TestName();
			if (!(reader[0] is DBNull)) beData.TestNameId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) beData.CheckupGroupId = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.InputTextType = reader.GetInt32(4);
			if (!(reader[5] is DBNull)) beData.SampleCollection = reader.GetString(5);
			if (!(reader[6] is DBNull)) beData.SampleUnit = reader.GetString(6);
			if (!(reader[7] is DBNull)) beData.Description = reader.GetString(7);
			if (!(reader[8] is DBNull)) beData.CheckUpGroupName = reader.GetString(8);
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
	private void SaveTestNameLabDetailDetails(int UserId, int TestNameId,BE.TestNameLabDetailCollections  beDataColl)
{
	if (beDataColl == null || beDataColl.Count == 0 || TestNameId == 0)
		return;

	foreach (BE.TestNameLabDetail beData in beDataColl)
	{
		System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@Name",beData.Name);
		cmd.Parameters.AddWithValue("@DefaultValue",beData.DefaultValue);
		cmd.Parameters.AddWithValue("@NormalRange",beData.NormalRange);
		cmd.Parameters.AddWithValue("@LowerRange",beData.LowerRange);
		cmd.Parameters.AddWithValue("@UpperRange",beData.UpperRange);
		cmd.Parameters.AddWithValue("@GroupName",beData.GroupName);
		cmd.Parameters.AddWithValue("@Remarks",beData.Remarks);
		cmd.Parameters.AddWithValue("@TestNameId", TestNameId);
		cmd.Parameters.AddWithValue("@UserId", UserId);
		cmd.CommandText = "usp_AddTestNameLabDetailDetails";
		cmd.ExecuteNonQuery();
	}

}

public BE.TestName getTestNameById(int UserId, int EntityId, int TestNameId)
{
	BE.TestName beData = new BE.TestName();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@TestNameId", TestNameId);
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetTestNameById";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			beData = new BE.TestName();
			if (!(reader[0] is DBNull)) beData.TestNameId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) beData.CheckupGroupId = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.InputTextType = reader.GetInt32(4);
			if (!(reader[5] is DBNull)) beData.SampleCollection = reader.GetString(5);
			if (!(reader[6] is DBNull)) beData.SampleUnit = reader.GetString(6);
			if (!(reader[7] is DBNull)) beData.Description = reader.GetString(7);
			}
		reader.NextResult();
		beData.TestNameLabDetailColl = new BE.TestNameLabDetailCollections();
		while (reader.Read())
		{
			BE.TestNameLabDetail det1 = new BE.TestNameLabDetail();
			if (!(reader[0] is DBNull)) det1.TestNameId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
			if (!(reader[2] is DBNull)) det1.DefaultValue = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) det1.NormalRange = reader.GetString(3);
			if (!(reader[4] is DBNull)) det1.LowerRange = reader.GetString(4);
			if (!(reader[5] is DBNull)) det1.UpperRange = reader.GetString(5);
			if (!(reader[6] is DBNull)) det1.GroupName = reader.GetString(6);
			if (!(reader[7] is DBNull)) det1.Remarks = reader.GetString(7);
					beData.TestNameLabDetailColl.Add(det1);
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

