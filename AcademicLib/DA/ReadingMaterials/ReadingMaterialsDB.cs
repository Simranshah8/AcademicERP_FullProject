using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.ReadingMaterials
{
    public class ReadingMaterialsDB
    {
        DataAccessLayer1 dal = null;
        public ReadingMaterialsDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
		public ResponeValues SaveUpdateVideo(List<BE.ReadingMaterials.ReadingMaterials.ReadMatVideo> beDataColl, int UserId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
				foreach (BE.ReadingMaterials.ReadingMaterials.ReadMatVideo beData in beDataColl)
				{
					cmd.Parameters.Clear();
					cmd.CommandText = "usp_AddReadMatVideo";
					cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
					cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
					cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
					cmd.Parameters.AddWithValue("@VideoTitle", beData.VideoTitle);
					cmd.Parameters.AddWithValue("@VideoLink", beData.VideoLink);
					cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
					cmd.Parameters.AddWithValue("@ReadMatVideoId", beData.ReadMatVideoId);

					cmd.Parameters.Add("@ResponseMSG", SqlDbType.NVarChar, 254).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("@IsSuccess", SqlDbType.Bit).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("@ErrorNumber", SqlDbType.Int).Direction = ParameterDirection.Output;

					cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
					cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);

					cmd.ExecuteNonQuery();

					if (!(cmd.Parameters[9].Value is DBNull))
						resVal.ResponseMSG = Convert.ToString(cmd.Parameters[9].Value);

					if (!(cmd.Parameters[10].Value is DBNull))
						resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[10].Value);

					if (!(cmd.Parameters[11].Value is DBNull))
						resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[11].Value);
					if (!resVal.IsSuccess)
					{
						dal.RollbackTransaction();
						return resVal;
					}
				}
				dal.CommitTransaction();
				resVal.IsSuccess = true;
				resVal.ResponseMSG = resVal.ResponseMSG;
			}

			catch (System.Data.SqlClient.SqlException ex)
			{
				dal.RollbackTransaction();
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ex.Message;
			}
			catch (Exception ex)
			{
				dal.RollbackTransaction();
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ex.Message;
			}
			finally
			{
				dal.CloseConnection();
			}

			return resVal;
		}
		public ResponeValues DeleteVideoById(int UserId, int EntityId, int ReadMatVideoId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ReadMatVideoId", ReadMatVideoId);
			cmd.CommandText = "usp_DelReadMatVideoById";
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
		public BE.ReadingMaterials.ReadingMaterials.ReadMatVideo GetReadMatVideoById(int UserId, int EntityId, int ReadMatVideoId)
		{
			BE.ReadingMaterials.ReadingMaterials.ReadMatVideo beData = new BE.ReadingMaterials.ReadingMaterials.ReadMatVideo();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ReadMatVideoId", ReadMatVideoId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetReadMatVideoById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.ReadMatVideoId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BatchId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.VideoTitle = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.VideoLink = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.SemesterId = reader.GetInt32(7);
					if (!(reader[8] is DBNull)) beData.ClassYearId = reader.GetInt32(8);
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
		public ResponeValues SaveUpdateContent(BE.ReadingMaterials.ReadingMaterials.ReadMatContent beData, bool isModify)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@BatchId", beData.BatchId);
			cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
			cmd.Parameters.AddWithValue("@SubjectId", beData.SubjectId);
			cmd.Parameters.AddWithValue("@Title", beData.Title);

			cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
			cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
			cmd.Parameters.AddWithValue("@ReadMatContId", beData.ReadMatContId);

			if (isModify)
			{
				cmd.CommandText = "usp_UpdateReadMatCont";
			}
			else
			{
				cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
				cmd.CommandText = "usp_AddReadMatCont";
			}
			cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
			cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
			cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);

			cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
			cmd.Parameters.AddWithValue("@ClassyearId", beData.ClassYearId);

			cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
			cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
			try
			{
				cmd.ExecuteNonQuery();
				if (!(cmd.Parameters[6].Value is DBNull))
					resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

				if (!(cmd.Parameters[7].Value is DBNull))
					resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

				if (!(cmd.Parameters[8].Value is DBNull))
					resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

				if (!(cmd.Parameters[9].Value is DBNull))
					resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

				if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
					resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

				if (resVal.RId > 0 && resVal.IsSuccess)
				{
					SaveContentDocDetails(beData.CUserId, resVal.RId, beData.ContentAttach);
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
		public ResponeValues DeleteContentById(int UserId, int EntityId, int ReadMatContId)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ReadMatContId", ReadMatContId);
			cmd.CommandText = "usp_DelReadMatContById";
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
		private void SaveContentDocDetails(int UserId, int ReadMatContId, Dynamic.BusinessEntity.GeneralDocumentCollections beDataColl)
		{
			if (beDataColl == null || beDataColl.Count == 0 || ReadMatContId == 0)
				return;

			foreach (Dynamic.BusinessEntity.GeneralDocument beData in beDataColl)
			{
				System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
				cmd.Parameters.AddWithValue("@ReadMatContId", ReadMatContId);
				cmd.Parameters.AddWithValue("@FileName", beData.Name);
				cmd.Parameters.AddWithValue("@FilePath", beData.DocPath);
				cmd.Parameters.AddWithValue("@Extension", beData.Extension);
				cmd.Parameters.AddWithValue("@UserId", UserId);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "usp_AddContAttach";
				cmd.ExecuteNonQuery();
			}
		}
		public BE.ReadingMaterials.ReadingMaterials.ReadMatContent GetReadMatContById(int UserId, int EntityId, int ReadMatContId)
		{
			BE.ReadingMaterials.ReadingMaterials.ReadMatContent beData = new BE.ReadingMaterials.ReadingMaterials.ReadMatContent();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ReadMatContId", ReadMatContId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetReadMatContById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					if (!(reader[0] is DBNull)) beData.ReadMatContId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BatchId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Title = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.SemesterId = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.ClassYearId = reader.GetInt32(6);
					if (!(reader[7] is DBNull)) beData.SemesterName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.ClassYearName = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.BatchName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.ClassName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.SubjectName = reader.GetString(11);
				}
				reader.NextResult();
				while (reader.Read())
				{
					Dynamic.BusinessEntity.GeneralDocument det1 = new Dynamic.BusinessEntity.GeneralDocument();
					if (!(reader[0] is DBNull)) det1.Id = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) det1.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) det1.DocPath = reader.GetString(2);
					if (!(reader[3] is DBNull)) det1.Extension = reader.GetString(3);
					beData.ContentAttach.Add(det1);
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
		public BE.ReadingMaterials.ReadingMaterials GetClassSubWiseData(int ClassId, int? ClassYearId, int? SemesterId, int SubjectId, int UserId, int EntityId, int? BatchId)
		{
			BE.ReadingMaterials.ReadingMaterials dataColl = new BE.ReadingMaterials.ReadingMaterials();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ClassId", ClassId);
			cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
			cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
			cmd.Parameters.AddWithValue("@BatchId", BatchId);
			cmd.CommandText = "usp_GetClassSubWiseData";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.ReadingMaterials.ReadingMaterials.ReadMatContent beData = new BE.ReadingMaterials.ReadingMaterials.ReadMatContent();
					if (!(reader[0] is DBNull)) beData.ReadMatContId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BatchId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.Title = reader.GetString(4);
					dataColl.ReadMatContentColl.Add(beData);
				}
				reader.NextResult();
				while (reader.Read())
				{
					BE.ReadingMaterials.ReadingMaterials.ReadMatVideo beData = new BE.ReadingMaterials.ReadingMaterials.ReadMatVideo();
					if (!(reader[0] is DBNull)) beData.ReadMatVideoId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BatchId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.ClassId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.SubjectId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.VideoTitle = reader.GetString(4);
					if (!(reader[5] is DBNull)) beData.VideoLink = reader.GetString(5);
					if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
					dataColl.ReadMadVideoColl.Add(beData);
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