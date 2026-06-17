using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class HealthChekUpExamDB
	{
		DataAccessLayer1 dal = null;
		public HealthChekUpExamDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.HealthChekUpExam beData , bool isModify)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@ClassTypeId",beData.ClassTypeId);
	cmd.Parameters.AddWithValue("@SectionTypeId",beData.SectionTypeId);
	cmd.Parameters.AddWithValue("@ExamTypeId",beData.ExamTypeId);
	cmd.Parameters.AddWithValue("@ClassCopyId",beData.ClassCopyId);
	cmd.Parameters.AddWithValue("@SectionCopyId",beData.SectionCopyId);
	
	cmd.Parameters.AddWithValue("@UserId",beData.CUserId);
	cmd.Parameters.AddWithValue("@EntityId",beData.EntityId);
	cmd.Parameters.AddWithValue("@ExamCheckUpId",beData.ExamCheckUpId);
	
	if (isModify)
	{
		cmd.CommandText = "usp_UpdateHealthChekUpExam";
	}
	else
	{
		cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
		cmd.CommandText = "usp_AddHealthChekUpExam";
	}
	cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
	cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
	cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
	cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
	cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
	cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
	try
	{
		cmd.ExecuteNonQuery();
		if (!(cmd.Parameters[7].Value is DBNull))
			resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

		if (!(cmd.Parameters[8].Value is DBNull))
			resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

		if (!(cmd.Parameters[9].Value is DBNull))
			resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

		if (!(cmd.Parameters[10].Value is DBNull))
			resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

		if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
			resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

	if (resVal.RId > 0 && resVal.IsSuccess)
			{
				SaveExamCheckUpDetDetails(beData.CUserId, resVal.RId, beData.ExamCheckUpDetColl);
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

	public ResponeValues DeleteById(int UserId, int EntityId, int ExamCheckUpId)
{
	ResponeValues resVal = new ResponeValues();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.Parameters.AddWithValue("@ExamCheckUpId", ExamCheckUpId);
	cmd.CommandText = "usp_DelHealthChekUpExamById";
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
	public BE.HealthChekUpExamCollections getAllHealthChekUpExam(int UserId, int EntityId)
{
	BE.HealthChekUpExamCollections dataColl = new BE.HealthChekUpExamCollections();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetAllHealthChekUpExam";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			BE.HealthChekUpExam beData = new BE.HealthChekUpExam();
			if (!(reader[0] is DBNull)) beData.ExamCheckUpId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.ClassTypeId = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) beData.SectionTypeId = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.ExamTypeId = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.ClassCopyId = reader.GetInt32(4);
			if (!(reader[5] is DBNull)) beData.SectionCopyId = reader.GetInt32(5);
			if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
			if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
			if (!(reader[8] is DBNull)) beData.ExamTypeName = reader.GetString(8);
			if (!(reader[9] is DBNull)) beData.TestName = reader.GetString(9);
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




	private void SaveExamCheckUpDetDetails(int UserId, int ExamCheckUpId,BE.ExamCheckUpDetCollections  beDataColl)
{
	if (beDataColl == null || beDataColl.Count == 0 || ExamCheckUpId == 0)
		return;

	foreach (BE.ExamCheckUpDet beData in beDataColl)
	{
		System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@TestNameTypeId",beData.TestNameTypeId);
		cmd.Parameters.AddWithValue("@DefaultValue",beData.DefaultValue);
		cmd.Parameters.AddWithValue("@DefaultRemarks",beData.DefaultRemarks);
		cmd.Parameters.AddWithValue("@ExamCheckUpId", ExamCheckUpId);
		cmd.Parameters.AddWithValue("@UserId", UserId);

		cmd.CommandText = "usp_AddExamCheckUpDetDetails";
		cmd.ExecuteNonQuery();
	}

}

public BE.HealthChekUpExam getHealthChekUpExamById(int UserId, int EntityId, int ExamCheckUpId)
{
	BE.HealthChekUpExam beData = new BE.HealthChekUpExam();
	dal.OpenConnection();
	System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
	cmd.CommandType = System.Data.CommandType.StoredProcedure;
	cmd.Parameters.AddWithValue("@ExamCheckUpId", ExamCheckUpId);
	cmd.Parameters.AddWithValue("@UserId", UserId);
	cmd.Parameters.AddWithValue("@EntityId", EntityId);
	cmd.CommandText = "usp_GetHealthChekUpExamById";
	try
	{
		System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			beData = new BE.HealthChekUpExam();
			if (!(reader[0] is DBNull)) beData.ExamCheckUpId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) beData.ClassTypeId = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) beData.SectionTypeId = reader.GetInt32(2);
			if (!(reader[3] is DBNull)) beData.ExamTypeId = reader.GetInt32(3);
			if (!(reader[4] is DBNull)) beData.ClassCopyId = reader.GetInt32(4);
			if (!(reader[5] is DBNull)) beData.SectionCopyId = reader.GetInt32(5);
			}
		reader.NextResult();
		beData.ExamCheckUpDetColl = new BE.ExamCheckUpDetCollections();
		while (reader.Read())
		{
			BE.ExamCheckUpDet det1 = new BE.ExamCheckUpDet();
			if (!(reader[0] is DBNull)) det1.ExamCheckUpId = reader.GetInt32(0);
			if (!(reader[1] is DBNull)) det1.TestNameTypeId = reader.GetInt32(1);
			if (!(reader[2] is DBNull)) det1.DefaultValue = reader.GetString(2);
			if (!(reader[3] is DBNull)) det1.DefaultRemarks = reader.GetString(3);
					beData.ExamCheckUpDetColl.Add(det1);
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



