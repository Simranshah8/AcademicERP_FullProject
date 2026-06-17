using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Transaction
{

	internal class ParentTeacherMeetingDB
	{
		DataAccessLayer1 dal= null;
		public ParentTeacherMeetingDB(string hostName, string dbName)
        {
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Academic.Transaction.ParentTeacherMeeting beData , bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
			cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);
			cmd.Parameters.AddWithValue("@PTMDate", beData.PTMDate);
			cmd.Parameters.AddWithValue("@PTMBy", beData.PTMBy);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@TranId", beData.TranId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateParentTeacherMeeting";
			}
			else
			{
				cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddParentTeacherMeeting";
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
				if (!(cmd.Parameters[7].Value is DBNull))	resVal.RId = Convert.ToInt32(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))	resVal.ResponseMSG = Convert.ToString(cmd.Parameters[8].Value);

				if (!(cmd.Parameters[9].Value is DBNull))	resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[9].Value);

				if (!(cmd.Parameters[10].Value is DBNull))	resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[10].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0) 
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
					SaveStudentPTMDetails(beData.CUserId, resVal.RId, beData.StudentPTMColl);

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
			cmd.CommandText = "usp_DelParentTeacherMeetingById";
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

		public BE.Academic.Transaction.ParentTeacherMeetingCollections getAllParentTeacherMeeting(int UserId, int EntityId)
		{
			AcademicLib.BE.Academic.Transaction.ParentTeacherMeetingCollections dataColl = new AcademicLib.BE.Academic.Transaction.ParentTeacherMeetingCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllParentTeacherMeeting";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Academic.Transaction.ParentTeacherMeeting beData = new BE.Academic.Transaction.ParentTeacherMeeting();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassSection = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.PTMDate = Convert.ToDateTime(reader[2]);
					if (!(reader[3] is DBNull)) beData.PTMByName = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.Description = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.PTMDateBS = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.ClassId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.SectionId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.PTMBy = reader.GetInt32(8);
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

		public AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting getParentTeacherMeetingById(int UserId, int EntityId, int TranId)
		{
			AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting beData = new AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@TranId", TranId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetParentTeacherMeetingById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Academic.Transaction.ParentTeacherMeeting();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.SectionId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.PTMDate = Convert.ToDateTime(reader[3]);
					if (!(reader[4] is DBNull)) beData.PTMBy = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.Description = reader.GetString(5);
				}
				reader.NextResult();
				beData.StudentPTMColl = new BE.Academic.Transaction.StudentPTMCollections();
				while (reader.Read())
				{
					BE.Academic.Transaction.StudentPTM det1 = new BE.Academic.Transaction.StudentPTM();
					if (!(reader[0] is DBNull)) det1.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.PTMAttendBy = reader.GetString(1);
					if (!(reader[2] is DBNull)) det1.TeacherRemarks = reader.GetString(2);
					if (!(reader[3] is DBNull)) det1.ParentRemarks = reader.GetString(3);
					if (!(reader[4] is DBNull)) det1.Recommendation = reader.GetString(4);
					if (!(reader[5] is DBNull)) det1.TranId = reader.GetInt32(5);
					beData.StudentPTMColl.Add(det1);
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
        private void SaveStudentPTMDetails(int UserId, int TranId, AcademicLib.BE.Academic.Transaction.StudentPTMCollections beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.Academic.Transaction.StudentPTM beData in beDataColl)
            {
                if (string.IsNullOrWhiteSpace(beData.PTMAttendBy))
                    continue;

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                cmd.Parameters.AddWithValue("@PTMAttendBy", beData.PTMAttendBy);
                cmd.Parameters.AddWithValue("@TeacherRemarks", beData.TeacherRemarks);
                cmd.Parameters.AddWithValue("@ParentRemarks", beData.ParentRemarks);
                cmd.Parameters.AddWithValue("@Recommendation", beData.Recommendation);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddStudentPTMDetails";
                cmd.ExecuteNonQuery();
            }
        }
        public AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting getAllStudentPTM(int UserId, int EntityId,
			int ClassId, int? SectionId, DateTime? PTMDate, int? PTMBy)
		{
			AcademicLib.BE.Academic.Transaction.StudentPTMCollections dataColl = new AcademicLib.BE.Academic.Transaction.StudentPTMCollections();
			AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting returnData = new AcademicLib.BE.Academic.Transaction.ParentTeacherMeeting();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SectionId", SectionId);
			cmd.Parameters.AddWithValue("@PTMDate", PTMDate);
			cmd.Parameters.AddWithValue("@PTMBy", PTMBy);
			cmd.CommandText = "usp_GetAllStudentPTM";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Academic.Transaction.StudentPTM beData = new BE.Academic.Transaction.StudentPTM();
					if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.StudentName = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.AdmNo = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3); 
					if (!(reader[4] is DBNull)) beData.PTMAttendBy = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.ParentRemarks = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.TeacherRemarks = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.Recommendation = reader.GetString(7);
					if (!(reader[8] is DBNull)) returnData.Description = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.TranId = reader.GetInt32(9);
					if (!(reader[10] is DBNull)) returnData.PTMDate = Convert.ToDateTime(reader[10]);
					if (!(reader[11] is DBNull)) returnData.PTMBy = reader.GetInt32(11);
					if (!(reader[12] is DBNull)) returnData.ClassId = reader.GetInt32(12);
					if (!(reader[13] is DBNull)) returnData.SectionId = reader.GetInt32(13);

					if (!(reader[14] is DBNull)) beData.SectionName = reader.GetString(14);

					dataColl.Add(beData);
					returnData.StudentPTMColl = dataColl;
				}
				reader.Close();
				returnData.IsSuccess = true;
				returnData.ResponseMSG = GLOBALMSG.SUCCESS;
			}
			catch (Exception ee)
			{
				returnData.IsSuccess = false;
				returnData.ResponseMSG = ee.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return returnData;

		}

	}

}

