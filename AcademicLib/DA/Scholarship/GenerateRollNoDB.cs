using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{

	internal class GenerateRollNoDB
	{
		DataAccessLayer1 dal = null;
		public GenerateRollNoDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Scholarship.GenerateRollNo beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
			cmd.Parameters.AddWithValue("@StartNo", beData.StartNo);
			cmd.Parameters.AddWithValue("@PadWidth", beData.PadWidth);
			cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
			cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@GenerateId", beData.GenerateId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateGenerateRollNo";
			}
			else
			{
				cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddGenerateRollNo";
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

		public ResponeValues DeleteById(int UserId, int EntityId, int GenerateId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@GenerateId", GenerateId);
			cmd.CommandText = "usp_DelGenerateRollNoById";
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
		public BE.Scholarship.GenerateRollNoCollections getAllGenerateRollNo(int UserId, int EntityId)
		{
			BE.Scholarship.GenerateRollNoCollections dataColl = new BE.Scholarship.GenerateRollNoCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllGenerateRollNo";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Scholarship.GenerateRollNo beData = new BE.Scholarship.GenerateRollNo();
					if (!(reader[0] is DBNull)) beData.GenerateId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.StartNo = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.PadWidth = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Prefix = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Suffix = reader.GetString(5);
					//New Field Added
					if (!(reader[6] is DBNull)) beData.LogDateTime = Convert.ToDateTime(reader[6]);
					if (!(reader[7] is DBNull)) beData.LogMiti = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.CreateBy = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.SubjectName = reader.GetString(9);
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
		public BE.Scholarship.GenerateRollNo getGenerateRollNoById(int UserId, int EntityId, int GenerateId)
		{
			BE.Scholarship.GenerateRollNo beData = new BE.Scholarship.GenerateRollNo();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@GenerateId", GenerateId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetGenerateRollNoById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Scholarship.GenerateRollNo();
					if (!(reader[0] is DBNull)) beData.GenerateId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SubjectId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.StartNo = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.PadWidth = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Prefix = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Suffix = reader.GetString(5);
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

