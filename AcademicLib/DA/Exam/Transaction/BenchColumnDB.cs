using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{

	internal class BenchColumnDB
	{
		DataAccessLayer1 dal = null;
		public BenchColumnDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.Exam.Transaction.BenchColumn beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@BenchTypeName", beData.BenchTypeName);
			cmd.Parameters.AddWithValue("@NoOfColumn", beData.NoOfColumn);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@BenchColumnId", beData.BenchColumnId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateBenchColumn";
			}
			else
			{
				cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddBenchColumn";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[5].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

				if (!(cmd.Parameters[6].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

				if (!(cmd.Parameters[7].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveBenchColumnDetailDetails(beData.CUserId, resVal.RId, beData.BenchColumnDetailColl);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int BenchColumnId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@BenchColumnId", BenchColumnId);
			cmd.CommandText = "usp_DelBenchColumnById";
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
		public BE.Exam.Transaction.BenchColumnCollections getAllBenchColumn(int UserId, int EntityId)
		{
			BE.Exam.Transaction.BenchColumnCollections dataColl = new BE.Exam.Transaction.BenchColumnCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllBenchColumn";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Exam.Transaction.BenchColumn beData = new BE.Exam.Transaction.BenchColumn();
					if (!(reader[0] is DBNull)) beData.BenchColumnId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BenchTypeName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.NoOfColumn = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.ColumnDetails = reader.GetString(4);
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
		private void SaveBenchColumnDetailDetails(int UserId, int BenchColumnId, BE.Exam.Transaction.BenchColumnDetailCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || BenchColumnId == 0)
				return;

			foreach (BE.Exam.Transaction.BenchColumnDetail beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ColSNo", beData.ColSNo);
				cmd.Parameters.AddWithValue("@ColumnName", beData.ColumnName);
				cmd.Parameters.AddWithValue("@BenchColumnId", BenchColumnId);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandText = "usp_AddBenchColumnDetail";
				cmd.ExecuteNonQuery();
			}
		}

		public BE.Exam.Transaction.BenchColumn getBenchColumnById(int UserId, int EntityId, int BenchColumnId)
		{
			BE.Exam.Transaction.BenchColumn beData = new BE.Exam.Transaction.BenchColumn();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@BenchColumnId", BenchColumnId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetBenchColumnById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Exam.Transaction.BenchColumn();
					if (!(reader[0] is DBNull)) beData.BenchColumnId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BenchTypeName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.NoOfColumn = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.OrderNo = reader.GetInt32(3);
				}
				reader.NextResult();
				beData.BenchColumnDetailColl = new BE.Exam.Transaction.BenchColumnDetailCollections();
				while (reader.Read())
				{
					BE.Exam.Transaction.BenchColumnDetail det1 = new BE.Exam.Transaction.BenchColumnDetail();
					if (!(reader[0] is DBNull)) det1.BenchColumnId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.ColSNo = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.ColumnName = reader.GetString(2);
					beData.BenchColumnDetailColl.Add(det1);
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


		public BE.Exam.Transaction.BenchColumnDetailCollections getColumnNameById(int UserId, int EntityId, int NoOfColumn)
		{
			BE.Exam.Transaction.BenchColumnDetailCollections dataColl = new BE.Exam.Transaction.BenchColumnDetailCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@NoOfColumn", NoOfColumn);
			cmd.CommandText = "usp_GetColumnNameById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Exam.Transaction.BenchColumnDetail beData = new BE.Exam.Transaction.BenchColumnDetail();
					if (!(reader[0] is DBNull)) beData.ColumnName = reader.GetString(0);
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

