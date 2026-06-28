using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Exam.Transaction
{
	internal class ObjectiveDB
	{
		DataAccessLayer1 dal = null;
		public ObjectiveDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}
		public ResponeValues SaveUpdate(BE.Exam.Transaction.Objective beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
			cmd.Parameters.AddWithValue("@ExamTypeId", beData.ExamTypeId);
			cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
			cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
			cmd.Parameters.AddWithValue("@FullMark", beData.FullMark);
			cmd.Parameters.AddWithValue("@SectionId", beData.SectionId);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@ObjectiveId", beData.ObjectiveId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateObjective";
			}
			else
			{
				cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddObjective";
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
					SaveObjectiveDetailsDetails(beData.CUserId, resVal.RId, beData.ObjectiveDetailsColl);
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
		private void SaveObjectiveDetailsDetails(int UserId, int ObjectiveId, BE.Exam.Transaction.ObjectiveDetailsCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || ObjectiveId == 0)
				return;

			foreach (BE.Exam.Transaction.ObjectiveDetails beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.Parameters.AddWithValue("@ObjectiveId", ObjectiveId);
				cmd.Parameters.AddWithValue("@SNo", beData.SNo);
				cmd.Parameters.AddWithValue("@Name", beData.Name);
				cmd.Parameters.AddWithValue("@Description", beData.Description);
				cmd.Parameters.AddWithValue("@IsActive", beData.IsActive);
				cmd.Parameters.AddWithValue("@Marks", beData.Marks);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "usp_AddObjectiveDetailsDetails";
				cmd.ExecuteNonQuery();
			}

		}

		public BE.Exam.Transaction.Objective GetObjective(int UserId, int EntityId, int? ClassId, int? ExamTypeId, int? SubjectId)
		{
			BE.Exam.Transaction.Objective beData = new BE.Exam.Transaction.Objective();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@ExamTypeId", ExamTypeId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.CommandText = "usp_GetObjectiveById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				beData.ObjectiveDetailsColl = new BE.Exam.Transaction.ObjectiveDetailsCollections();
				while (reader.Read())
				{
					BE.Exam.Transaction.ObjectiveDetails det1 = new BE.Exam.Transaction.ObjectiveDetails();
					if (!(reader[0] is DBNull)) det1.ObjectiveId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det1.Description = reader.GetString(2);
					if (!(reader[3] is DBNull)) det1.IsActive = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) det1.Marks = Convert.ToDouble(reader[4]);
					if (!(reader[5] is DBNull)) det1.FullMark = Convert.ToDouble(reader[5]);
					beData.ObjectiveDetailsColl.Add(det1);
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

		public ResponeValues DeleteById(int UserId, int EntityId, int ObjectiveId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ObjectiveId", ObjectiveId);
			cmd.CommandText = "usp_DelObjectiveById";
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
		public BE.Exam.Transaction.ObjectiveCollections getAllObjective(int UserId, int EntityId)
		{
			BE.Exam.Transaction.ObjectiveCollections dataColl = new BE.Exam.Transaction.ObjectiveCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllObjective";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Exam.Transaction.Objective beData = new BE.Exam.Transaction.Objective();
					if (!(reader[0] is DBNull)) beData.ObjectiveId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ExamTypeId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.AcademicYearId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.FullMark = Convert.ToDouble(reader[5]);
					if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.ExamName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.SubjectName = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.SectionName = reader.GetString(9);
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

		public ResponeValues TransferObjective(int UserId, int ?FromExamTypeId, int? ToExamTypeId,int? FromClassId,int? ToClassId,int? FromSectionId,int? ToSectionId)
		{
			ResponeValues resVal = new ResponeValues();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@FromExamTypeId", FromExamTypeId);
			cmd.Parameters.AddWithValue("@ToExamTypeId", ToExamTypeId);
			cmd.Parameters.AddWithValue("@FromClassId", FromClassId);
			cmd.Parameters.AddWithValue("@ToClassId", ToClassId);
			cmd.Parameters.AddWithValue("@FromSectionId", FromSectionId);
			cmd.Parameters.AddWithValue("@ToSectionId", ToSectionId);
			cmd.CommandText = "usp_TransferObjective";
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();

				if (!(cmd.Parameters[7].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

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

		public BE.Exam.Transaction.ObjectiveCollections GetAllObjectiveTansfer(int UserId, int EntityId, int? FromExamTypeId, int? ToExamTypeId, int? FromClassId, int? ToClassId)
		{
			BE.Exam.Transaction.ObjectiveCollections dataColl = new BE.Exam.Transaction.ObjectiveCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@FromExamTypeId", FromExamTypeId);
			cmd.Parameters.AddWithValue("@ToExamTypeId", ToExamTypeId);
			cmd.Parameters.AddWithValue("@FromClassId", FromClassId);
			cmd.Parameters.AddWithValue("@ToClassId", ToClassId);
			cmd.CommandText = "usp_GetObjectiveTransfer";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.Exam.Transaction.Objective beData1 = new BE.Exam.Transaction.Objective();
					if (!(reader[0] is DBNull)) beData1.ObjectiveId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData1.FromExamTypeId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData1.ToExamTypeId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData1.FromClassId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData1.ToClassId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData1.FromSectionId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData1.ToSectionId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData1.FromClassName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData1.FromExamTypeName = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData1.ToExamTypeName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData1.ToClassName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData1.FromSectionName = reader.GetString(11);
					if (!(reader[12] is DBNull)) beData1.ToSectionName = reader.GetString(12);
					if (!(reader[13] is DBNull)) beData1.TransferCreateBy = reader.GetString(13);
					if (!(reader[14] is DBNull)) beData1.TransferDate = reader.GetString(14);
					dataColl.Add(beData1);
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

		public BE.Exam.Transaction.Objective GetObjectiveById(int UserId, int EntityId, int ObjectiveId)
		{
			BE.Exam.Transaction.Objective beData = new BE.Exam.Transaction.Objective();

			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ObjectiveId", ObjectiveId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetObjectiveForEdit";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					beData = new BE.Exam.Transaction.Objective();
					beData.ObjectiveId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.ClassId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ExamTypeId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.FullMark = Convert.ToDouble(reader[4]);
					if (!(reader[5] is DBNull)) beData.SectionId = reader.GetInt32(5);
				}
				reader.NextResult();
				beData.ObjectiveDetailsColl = new AcademicLib.BE.Exam.Transaction.ObjectiveDetailsCollections();
				while (reader.Read())
				{
					AcademicLib.BE.Exam.Transaction.ObjectiveDetails det2 = new AcademicLib.BE.Exam.Transaction.ObjectiveDetails();
					if (!(reader[0] is DBNull)) det2.ObjectiveId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det2.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det2.Description = reader.GetString(2);
					if (!(reader[3] is DBNull)) det2.IsActive = Convert.ToBoolean(reader[3]);
					if (!(reader[4] is DBNull)) det2.Marks = Convert.ToDouble(reader[4]);
					if (!(reader[5] is DBNull)) det2.SNo = reader.GetInt32(5);
					beData.ObjectiveDetailsColl.Add(det2);
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

