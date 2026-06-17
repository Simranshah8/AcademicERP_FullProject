using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.FormEntity
{
    internal class EntityFieldsAllowDB
    {
        DataAccessLayer1 dal = null;
        public EntityFieldsAllowDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.FormEntity.EntityFieldsAllowCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                var uid = dataColl.First();
                cmd.Parameters.AddWithValue("@EntityId", uid.EntityId);
                cmd.Parameters.AddWithValue("@ForUserId", uid.ForUserId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandText = "usp_DelEntityFeildsAllow";
                cmd.ExecuteNonQuery();
                foreach (var beData in dataColl)
                {
                    if (beData.IsAllow)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
                        cmd.Parameters.AddWithValue("@FieldId", beData.FieldId);

                        if(beData.ForUserId==0)
                            cmd.Parameters.AddWithValue("@ForUserId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ForUserId", beData.ForUserId);

                        cmd.CommandText = "usp_SaveEntityFeildsAllow";
                        cmd.ExecuteNonQuery();
                    }
                }
                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Allow User Wise Entity Fields Update Success";

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
        public ResponeValues CheckAllowFields(int UserId,int? ForUserId,int EntityId,int FieldId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            { 
                cmd.Parameters.AddWithValue("@EntityId", EntityId);
                cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@FieldId", FieldId);
                cmd.CommandText = "usp_CheckEntityFieldAllow";
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Entity Was disabled for Update";

                }
                reader.Close();
              
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
        public List<int> getAllowFields(int UserId, int ForUserId,int EntityId)
        {
            List<int> dataColl = new List<int>();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetEntityFieldsAllow";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataColl.Add(reader.GetInt32(0));
                }
                reader.Close();
              
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataColl;
        }
    }
}
