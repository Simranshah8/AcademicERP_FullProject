using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AppCMS.Creation
{

	internal class HeaderDetailsDB
	{
		DataAccessLayer1 dal = null;
		public HeaderDetailsDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.AppCMS.Creation.HeaderDetails beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@LogoPhoto", beData.LogoPhoto);
			cmd.Parameters.AddWithValue("@CompanyName", beData.CompanyName);
			cmd.Parameters.AddWithValue("@Slogan", beData.Slogan);
			cmd.Parameters.AddWithValue("@HeaderIsActive", beData.HeaderIsActive);
			cmd.Parameters.AddWithValue("@NameIsActive", beData.NameIsActive);
			cmd.Parameters.AddWithValue("@SloganIsActive", beData.SloganIsActive);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@HeaderDetailId", beData.HeaderDetailId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateHeaderDetails";
			}
			else
			{
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddHeaderDetails";
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
		public BE.AppCMS.Creation.HeaderDetails getHeaderDetailsById(int UserId, int EntityId)
		{
			BE.AppCMS.Creation.HeaderDetails beData = new BE.AppCMS.Creation.HeaderDetails();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetHeaderDetailsById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.AppCMS.Creation.HeaderDetails();
					if (!(reader[0] is DBNull)) beData.HeaderDetailId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.LogoPhoto = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.CompanyName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Slogan = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.HeaderIsActive = Convert.ToBoolean(reader[4]);
					if (!(reader[5] is DBNull)) beData.NameIsActive = Convert.ToBoolean(reader[5]);
					if (!(reader[6] is DBNull)) beData.SloganIsActive = Convert.ToBoolean(reader[6]);
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

