using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Communication.Creation
{
    internal class ContactGroupDB
    {
        DataAccessLayer1 dal = null;
        public ContactGroupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
		public ResponeValues SaveUpdate(AcademicLib.BE.Communication.Creation.ContactGroup beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@GroupId", beData.GroupId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateContactGroup";
			}
			else
			{
				cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddContactGroup";
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

		public AcademicLib.BE.Communication.Creation.ContactGroupCollection getAllContactGroup(int UserId, int EntityId)
		{
			AcademicLib.BE.Communication.Creation.ContactGroupCollection dataColl = new AcademicLib.BE.Communication.Creation.ContactGroupCollection();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllContactGroup";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Communication.Creation.ContactGroup beData = new AcademicLib.BE.Communication.Creation.ContactGroup();
					if (!(reader[0] is DBNull)) beData.GroupId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
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

		public AcademicLib.BE.Communication.Creation.ContactGroup getContactGroupById(int UserId, int EntityId, int GroupId)
		{
			AcademicLib.BE.Communication.Creation.ContactGroup beData = new AcademicLib.BE.Communication.Creation.ContactGroup();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@GroupId", GroupId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetContactGroupById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new AcademicLib.BE.Communication.Creation.ContactGroup();
					if (!(reader[0] is DBNull)) beData.GroupId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.OrderNo = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
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

		public ResponeValues DeleteGroupById(int UserId, int EntityId, int GroupId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@GroupId", GroupId);
			cmd.CommandText = "usp_DelContactGroupById";
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

	}
}













































































