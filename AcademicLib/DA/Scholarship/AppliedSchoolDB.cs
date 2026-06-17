using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Scholarship
{

	internal class AppliedSchoolDB
	{
		DataAccessLayer1 dal = null;
		public AppliedSchoolDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(AcademicLib.BE.Scholarship.AppliedSchool beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", beData.Name);
			cmd.Parameters.AddWithValue("@Address", beData.Address);
			cmd.Parameters.AddWithValue("@Email", beData.Email);
			cmd.Parameters.AddWithValue("@ContactNo", beData.ContactNo);
			cmd.Parameters.AddWithValue("@Description", beData.Description);
			cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@SchoolId", beData.SchoolId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateAppliedSchool";
			}
			else
			{
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddAppliedSchool";
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

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveSchoolSubjectListDetails(beData.CUserId, resVal.RId, beData.SchoolSubjectListColl);
					SaveSchoolClassListDetails(beData.CUserId, resVal.RId, beData.ForClassIdColl);
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


		private void SaveSchoolSubjectListDetails(int UserId, int SchoolId, AcademicLib.BE.Scholarship.SchoolSubjectListCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || SchoolId == 0)
				return;

			foreach (AcademicLib.BE.Scholarship.SchoolSubjectList beData in beDataColl)
			{
                if (beData.AllowSubject)
                {
					System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
					cmd.CommandType = System.Data.CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
					cmd.Parameters.AddWithValue("@AllowSubject", beData.AllowSubject);
					cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.CommandText = "usp_AddSchoolSubjectListDetails";
					cmd.ExecuteNonQuery();
				}				
			}

		}

		private void SaveSchoolClassListDetails(int UserId, int SchoolId, List<int> beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || SchoolId == 0)
				return;

			foreach (var classId in beDataColl)
			{
				if (classId>0 && SchoolId>0)
				{
					System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
					cmd.CommandType = System.Data.CommandType.Text;
					cmd.Parameters.AddWithValue("@ClassId", classId);
					cmd.Parameters.AddWithValue("@SchoolId", SchoolId);					
					cmd.CommandText = "insert into tbl_SchoolClassList(SchoolId,ClassId) values(@SchoolId,@ClassId)";
					cmd.ExecuteNonQuery();
				}
			}

		}

		public ResponeValues DeleteById(int UserId, int EntityId, int SchoolId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
			cmd.CommandText = "usp_DelAppliedSchoolById";
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
		public AcademicLib.BE.Scholarship.AppliedSchoolCollections getAllAppliedSchool(int UserId, int EntityId)
		{
			AcademicLib.BE.Scholarship.AppliedSchoolCollections dataColl = new AcademicLib.BE.Scholarship.AppliedSchoolCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllAppliedSchool";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					AcademicLib.BE.Scholarship.AppliedSchool beData = new AcademicLib.BE.Scholarship.AppliedSchool();
					if (!(reader[0] is DBNull)) beData.SchoolId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Email = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Description = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.OrderNo = reader.GetInt32(6);
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


		public AcademicLib.BE.Scholarship.AppliedSchool getAppliedSchoolById(int UserId, int EntityId, int SchoolId)
		{
			AcademicLib.BE.Scholarship.AppliedSchool beData = new AcademicLib.BE.Scholarship.AppliedSchool();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAppliedSchoolById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new AcademicLib.BE.Scholarship.AppliedSchool();
					if (!(reader[0] is DBNull)) beData.SchoolId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Address = reader.GetString(2);
					if (!(reader[3] is DBNull)) beData.Email = reader.GetString(3);
					if (!(reader[4] is DBNull)) beData.ContactNo = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.Description = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.OrderNo = reader.GetInt32(6);
				}
				reader.NextResult();
				beData.SchoolSubjectListColl = new AcademicLib.BE.Scholarship.SchoolSubjectListCollections();
				while (reader.Read())
				{
					AcademicLib.BE.Scholarship.SchoolSubjectList det1 = new AcademicLib.BE.Scholarship.SchoolSubjectList();
					if (!(reader[0] is DBNull)) det1.SchoolId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.SubjectId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) det1.AllowSubject = Convert.ToBoolean(reader[2]);
					if (!(reader[3] is DBNull)) det1.Name = reader.GetString(3);
					beData.SchoolSubjectListColl.Add(det1);
				}
				reader.NextResult();
				beData.ForClassIdColl = new List<int>();
                while (reader.Read())
                {
					beData.ForClassIdColl.Add(reader.GetInt32(0));
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

