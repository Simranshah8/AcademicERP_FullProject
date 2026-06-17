using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.DA.Health.Transaction
{
    internal class StoolTestDB
    {
        DataAccessLayer1 dal = null;
        public StoolTestDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicERP.BE.Health.Transaction.StoolTest beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TestDate", beData.TestDate);
            cmd.Parameters.AddWithValue("@Color", beData.Color);
            cmd.Parameters.AddWithValue("@Mucus", beData.Mucus);
            cmd.Parameters.AddWithValue("@Puscell", beData.Puscell);
            cmd.Parameters.AddWithValue("@RBC", beData.RBC);
            cmd.Parameters.AddWithValue("@Cyst", beData.Cyst);
            cmd.Parameters.AddWithValue("@Ova", beData.Ova);
            cmd.Parameters.AddWithValue("@Others", beData.Others);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateStoolTest";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddStoolTest";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", beData.EmployeeId);
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
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0)
                {
                    SaveAttachDocuments(beData.AttachStTeColl, resVal.RId, beData.CUserId);
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

        private void SaveAttachDocuments(Dynamic.BusinessEntity.GeneralDocumentCollections dataColl, int TranId, int UserId)
        {
            foreach (var beData in dataColl)
            {
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocumentTypeId", beData.DocumentTypeId);
                cmd.Parameters.AddWithValue("@Name", beData.Name);
                cmd.Parameters.AddWithValue("@docDescription", beData.Description);
                cmd.Parameters.AddWithValue("@Extension", beData.Extension);
                cmd.Parameters.AddWithValue("@Document", beData.Data);
                cmd.Parameters.AddWithValue("@DocPath", beData.DocPath);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_AddStoolTestAttDoc";
                cmd.ExecuteNonQuery();
            }
        }
        public AcademicERP.BE.Health.Transaction.StoolTestCollections getAllStoolTest(int UserId, int EntityId, int? StudentId, int? EmployeeId)
        {
            AcademicERP.BE.Health.Transaction.StoolTestCollections dataColl = new AcademicERP.BE.Health.Transaction.StoolTestCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            cmd.CommandText = "usp_GetAllStoolTest";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicERP.BE.Health.Transaction.StoolTest beData = new AcademicERP.BE.Health.Transaction.StoolTest();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Color = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Mucus = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Puscell = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RBC = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Cyst = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Ova = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Others = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.TestMiti = reader.GetString(9);

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

        public AcademicERP.BE.Health.Transaction.StoolTest getStoolTestById(int UserId, int EntityId, int TranId)
        {
            AcademicERP.BE.Health.Transaction.StoolTest beData = new AcademicERP.BE.Health.Transaction.StoolTest();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetStoolTestById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicERP.BE.Health.Transaction.StoolTest();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TestDate = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) beData.Color = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Mucus = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Puscell = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RBC = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Cyst = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Ova = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Others = reader.GetString(8);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelStoolTestById";
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
    }
}