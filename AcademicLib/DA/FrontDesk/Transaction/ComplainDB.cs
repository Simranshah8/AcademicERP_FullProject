using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FrontDesk.Transaction
{
    internal class ComplainDB
	{
		DataAccessLayer1 dal = null;
		public ComplainDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.FrontDesk.Transaction.Complain beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ComplainTypeId", beData.ComplainTypeId);
			cmd.Parameters.AddWithValue("@SourceId", beData.SourceId);
			cmd.Parameters.AddWithValue("@PhoneNo", beData.PhoneNo);
			cmd.Parameters.AddWithValue("@AssignTo", beData.AssignTo);
			cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
			cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
			cmd.Parameters.AddWithValue("@ComplainDate", beData.ComplainDate);
			cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
			cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
			cmd.Parameters.AddWithValue("@OthersName", beData.OthersName);
			cmd.Parameters.AddWithValue("@AssignToId", beData.AssignToId);
			cmd.Parameters.AddWithValue("@ComplainBy", beData.ComplainBy);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@ComplainId", beData.ComplainId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateComplain";
			}
			else
			{
				cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddComplain";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[16].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[17].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[14].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[14].Value);

				if (!(cmd.Parameters[15].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[15].Value);

				if (!(cmd.Parameters[16].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[16].Value);

				if (!(cmd.Parameters[17].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[17].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveComplainAttDocDetails(beData.AttachmentColl, resVal.RId, beData.CUserId);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int ComplainId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ComplainId", ComplainId);
			cmd.CommandText = "usp_DelComplainById";
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
		public AcademicLib.BE.FrontDesk.Transaction.ComplainCollections getAllComplain(int UserId, int EntityId, DateTime? dateFrom, DateTime? dateTo, int? SourceId, int? ComplainTypeId, int? StatusId)
		{
			AcademicLib.BE.FrontDesk.Transaction.ComplainCollections dataColl = new AcademicLib.BE.FrontDesk.Transaction.ComplainCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
			cmd.Parameters.AddWithValue("@dateTo", dateTo);
			cmd.Parameters.AddWithValue("@SourceId", SourceId);
			cmd.Parameters.AddWithValue("@ComplainTypeId", ComplainTypeId);
			cmd.Parameters.AddWithValue("@StatusId", StatusId);
			cmd.CommandText = "usp_GetAllComplain";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.FrontDesk.Transaction.Complain beData = new AcademicLib.BE.FrontDesk.Transaction.Complain();
					if (!(reader[0] is DBNull)) beData.ComplainId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.SourceId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ComplainTypeName = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.PhoneNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.AssignToName = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.BranchId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.ComplainDate = Convert.ToDateTime(reader[7]);
					if (!(reader[8] is DBNull)) beData.ComplainMiti = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.OthersName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.ActionRemarks = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.ActionTakenBy = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.ActionMiti = reader.GetString(12);
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
		private void SaveComplainAttDocDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int ComplainId, int UserId)
		{

			foreach (var beData in dataColl)
				if (!string.IsNullOrEmpty(beData.DocPath))
				{
					System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
					cmd.Parameters.AddWithValue("@Name", beData.Name);
					cmd.Parameters.AddWithValue("@docDescription", beData.Description);
					cmd.Parameters.AddWithValue("@Extension", beData.Extension);
					cmd.Parameters.AddWithValue("@Document", beData.Data);
					cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
					cmd.Parameters.AddWithValue("@ComplainId", ComplainId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.CommandText = "usp_AddComplainAttDocDetails";
					cmd.ExecuteNonQuery();
				}

		}

		public AcademicLib.BE.FrontDesk.Transaction.Complain getComplainById(int UserId, int EntityId, int ComplainId)
		{
			AcademicLib.BE.FrontDesk.Transaction.Complain beData = new AcademicLib.BE.FrontDesk.Transaction.Complain();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ComplainId", ComplainId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetComplainById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new AcademicLib.BE.FrontDesk.Transaction.Complain();
					if (!(reader[0] is DBNull)) beData.ComplainId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ComplainTypeId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.SourceId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.PhoneNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.AssignTo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Remarks = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.BranchId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.ComplainDate = Convert.ToDateTime(reader[7]);
					if (!(reader[8] is DBNull)) beData.StudentId = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.EmployeeId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) beData.OthersName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.AssignToId = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) beData.ComplainBy = reader.GetInt32(12);
				}
				reader.NextResult();
				while (reader.Read())
				{
					Dynamic.BusinessEntity.GeneralDocument det = new Dynamic.BusinessEntity.GeneralDocument();
					if (!(reader[0] is DBNull)) det.DocumentTypeId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det.Extension = reader.GetString(2);
					if (!(reader[3] is DBNull)) det.DocPath = reader.GetString(3);
					if (!(reader[4] is DBNull)) det.Description = reader.GetString(4);
					beData.AttachmentColl.Add(det);
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


		public ResponeValues SaveComplainReply(AcademicLib.BE.FrontDesk.Transaction.ComplainReply beData)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ComplainId", beData.ComplainId);
			cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.CommandText = "usp_AddComplainReply";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[4].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

				if (!(cmd.Parameters[5].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

				if (!(cmd.Parameters[6].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

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

