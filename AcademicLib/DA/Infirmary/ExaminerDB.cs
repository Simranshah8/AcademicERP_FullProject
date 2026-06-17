using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA
{

	internal class ExaminerDB
	{
		DataAccessLayer1 dal = null;
		public ExaminerDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(BE.Examiner beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@Designation", beData.Designation);
			cmd.Parameters.AddWithValue("@ExaminerRegdNo", beData.ExaminerRegdNo);
			cmd.Parameters.AddWithValue("@MobileNo", beData.MobileNo);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@Qualification", beData.Qualification);
			cmd.Parameters.AddWithValue("@Specialization", beData.Specialization);
			cmd.Parameters.AddWithValue("@UsernameId", beData.UsernameId);
			cmd.Parameters.AddWithValue("@Address", beData.Address);
			cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
			cmd.Parameters.AddWithValue("@PhotoPath", beData.PhotoPath);
			cmd.Parameters.AddWithValue("@SPhotoPath", beData.SPhotoPath);
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@ExaminerId", beData.ExaminerId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateExaminer";
			}
			else
			{
				cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddExaminer";
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

				if (resVal.IsSuccess && resVal.RId > 0)
				{
					SaveExaminerAttDocDetails(beData.AttachmentColl, resVal.RId, beData.CUserId);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int ExaminerId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ExaminerId", ExaminerId);
			cmd.CommandText = "usp_DelExaminerById";
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
		public BE.ExaminerCollections getAllExaminer(int UserId, int EntityId)
		{
			BE.ExaminerCollections dataColl = new BE.ExaminerCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllExaminer";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Examiner beData = new BE.Examiner();
					if (!(reader[0] is DBNull)) beData.ExaminerId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.ExaminerRegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.MobileNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Email = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Qualification = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.Specialization = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.UsernameId = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.Remarks = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.PhotoPath = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.SPhotoPath = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData.UserName = reader.GetString(13);
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
		private void SaveExaminerAttDocDetails(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int ExaminerId, int UserId)
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
				cmd.Parameters.AddWithValue("@ExaminerId", ExaminerId);
				cmd.Parameters.AddWithValue("@UserId", UserId);

				cmd.CommandText = "usp_AddExaminerAttDocDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.Examiner getExaminerById(int UserId, int EntityId, int ExaminerId)
		{
			BE.Examiner beData = new BE.Examiner();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ExaminerId", ExaminerId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetExaminerById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Examiner();
					if (!(reader[0] is DBNull)) beData.ExaminerId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Designation = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.ExaminerRegdNo = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.MobileNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Email = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Qualification = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.Specialization = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.UsernameId = reader.GetInt32(8);
					if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.Remarks = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.PhotoPath = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData.SPhotoPath = reader.GetString(12);
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

	}

}

