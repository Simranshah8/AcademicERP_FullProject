using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{

	internal class EnquiryFormConfigDB
	{
		DataAccessLayer1 dal = null;
		public EnquiryFormConfigDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Academic.Creation.EnquiryFormConfig beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@FormStatus", beData.FormStatus);
			cmd.Parameters.AddWithValue("@ClosedMsg", beData.ClosedMsg);
			cmd.Parameters.AddWithValue("@FormHeading", beData.FormHeading);
			cmd.Parameters.AddWithValue("@IsLogoDisplayed", beData.IsLogoDisplayed);
			cmd.Parameters.AddWithValue("@IsHeaderBDisplayed", beData.IsHeaderBDisplayed);
			cmd.Parameters.AddWithValue("@IsFooterBDisplayed", beData.IsFooterBDisplayed);
			cmd.Parameters.AddWithValue("@IsAbouUsContentDisplayed", beData.IsAbouUsContentDisplayed);
			cmd.Parameters.AddWithValue("@IsEnqContentDisplayed", beData.IsEnqContentDisplayed);
			cmd.Parameters.AddWithValue("@EnqContent", beData.EnqContent);
			cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateEnquiryFormConfig";
			}
			else
			{
				cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddEnquiryFormConfig";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[12].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[12].Value);

				if (!(cmd.Parameters[13].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[13].Value);

				if (!(cmd.Parameters[14].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[14].Value);

				if (!(cmd.Parameters[15].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[15].Value);

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

		public BE.Academic.Creation.EnquiryFormConfig getAllEnquiryFormConfig(int UserId, int EntityId)
		{
			BE.Academic.Creation.EnquiryFormConfig beData = new BE.Academic.Creation.EnquiryFormConfig();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllEnquiryFormConfig";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Academic.Creation.EnquiryFormConfig();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.FormStatus = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ClosedMsg = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.FormHeading = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.IsLogoDisplayed = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.IsHeaderBDisplayed = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.IsFooterBDisplayed = Convert.ToBoolean(reader[6]);
					if (!(reader[7] is DBNull)) beData.IsAbouUsContentDisplayed = Convert.ToBoolean(reader[7]);
					if (!(reader[8] is DBNull)) beData.IsEnqContentDisplayed = Convert.ToBoolean(reader[8]);
					if (!(reader[9] is DBNull)) beData.EnqContent = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.BranchId = reader.GetInt32(10);
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

