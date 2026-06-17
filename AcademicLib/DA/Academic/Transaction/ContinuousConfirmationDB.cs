using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{

	internal class ContinuousConfirmationDB
	{
		DataAccessLayer1 dal = null;
		public ContinuousConfirmationDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Academic.Transaction.ContinuousConfirmation beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
			cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
			cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
			cmd.Parameters.AddWithValue("@ContinueYes", beData.ContinueYes);
			cmd.Parameters.AddWithValue("@ContinueNo", beData.ContinueNo);
			cmd.Parameters.AddWithValue("@NotContinueReasonId", beData.NotContinueReasonId);
			cmd.Parameters.AddWithValue("@OtherReason", beData.OtherReason);
			cmd.Parameters.AddWithValue("@Feedback", beData.Feedback);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateContinuousConfirmation";
			}
			else
			{
				cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddContinuousConfirmation";
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

		public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.CommandText = "usp_DelContinuousConfirmationById";
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
		public BE.Academic.Transaction.ContinuousConfirmationCollections getAllContinuousConfirmation(int UserId, int EntityId,int? ClassId, int? SectionId)
		{
			BE.Academic.Transaction.ContinuousConfirmationCollections dataColl = new BE.Academic.Transaction.ContinuousConfirmationCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.CommandText = "usp_GetAllContinuousConfirmation";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Academic.Transaction.ContinuousConfirmation beData = new BE.Academic.Transaction.ContinuousConfirmation();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.NotContinueReason = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Feedback = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Name = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.RegNo = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Gender = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.RollNo = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.ClassName = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.SectionName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.ContactNo = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.FatherName = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.FContactNo = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.MotherName = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData.MContactNo = reader.GetString(14);
					if (!(reader[15] is DBNull)) beData.GuardianName = reader.GetString(15);
					if (!(reader[16] is DBNull)) beData.GContactNo = reader.GetString(16);
					if (!(reader[17] is DBNull)) beData.Continuous = reader.GetString(17);
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
		public BE.Academic.Transaction.ContinuousConfirmation getContinuousConfirmationById(int UserId, int EntityId, int TranId)
		{
			BE.Academic.Transaction.ContinuousConfirmation beData = new BE.Academic.Transaction.ContinuousConfirmation();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetContinuousConfirmationById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Academic.Transaction.ContinuousConfirmation();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.AcademicYearId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.ContinueYes = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.ContinueNo = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.NotContinueReasonId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.OtherReason = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.Feedback = reader.GetString(8);
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

