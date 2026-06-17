using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Setup
{
    internal class SENTDB
    {
        DataAccessLayer1 dal = null;
        public SENTDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }        
        public ResponeValues SaveUpdate(BE.Setup.SENT beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@ForATS", beData.ForATS);
            cmd.Parameters.AddWithValue("@TemplateType", beData.TemplateType);
            cmd.Parameters.AddWithValue("@ActionType", beData.ActionType);
            cmd.Parameters.AddWithValue("@Status", beData.Status);
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Title", beData.Title);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@Recipients", beData.Recipients);
            cmd.Parameters.AddWithValue("@EmailCC", beData.EmailCC);
            cmd.Parameters.AddWithValue("@EmailBCC", beData.EmailBCC);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);            
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateSENT";
            }
            else
            {
                cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddSENT";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
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
        public BE.Setup.SENTCollections getTemplates(int UserId, int EntityId,int ForATS,int TemplateType)
        {
            BE.Setup.SENTCollections dataColl = new BE.Setup.SENTCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ForATS", ForATS);
            cmd.Parameters.AddWithValue("@TemplateType", TemplateType);
            cmd.CommandText = "usp_GetSENT";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.SENT beData = new BE.Setup.SENT();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ForATS = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.TemplateType = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ActionType = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Status = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Title = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Description = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Recipients = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.EmailCC = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.EmailBCC = reader.GetString(11);

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
        public BE.Setup.SENT getSENTById(int UserId, int EntityId, int TranId)
        {
            BE.Setup.SENT beData = new BE.Setup.SENT();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetSENTById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Setup.SENT();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.EntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ForATS = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.TemplateType = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ActionType = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Status = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Title = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Description = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Recipients = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.EmailCC = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.EmailBCC = reader.GetString(11);
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
            cmd.CommandText = "usp_DelSENTById";
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
