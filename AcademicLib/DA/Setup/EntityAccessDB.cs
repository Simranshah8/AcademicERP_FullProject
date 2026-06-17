using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Setup
{
    internal class EntityAccessDB
    {
        DataAccessLayer1 dal = null;
        public EntityAccessDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int UserId, AcademicLib.BE.Setup.EntityAccessCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                var first = dataColl.First();
                int? userId = first.UserId;
                int? groupId = first.GroupId;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@GroupId", groupId);
                cmd.CommandText = "ups_DelEntityAccess";
                cmd.ExecuteNonQuery();
                foreach (var beData in dataColl)
                {
                    if (beData.EntityTranId>0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CreateBy", UserId);
                        cmd.Parameters.AddWithValue("@UserId", beData.UserId);
                        cmd.Parameters.AddWithValue("@GroupId", beData.GroupId);
                        cmd.Parameters.AddWithValue("@EntityId", beData.EntityTranId);
                        cmd.Parameters.AddWithValue("@Full", beData.Full);
                        cmd.Parameters.AddWithValue("@View", beData.View);
                        cmd.Parameters.AddWithValue("@Add", beData.Add);
                        cmd.Parameters.AddWithValue("@Modify", beData.Modify);
                        cmd.Parameters.AddWithValue("@Delete", beData.Delete);
                        cmd.Parameters.AddWithValue("@Print", beData.Print);
                        cmd.Parameters.AddWithValue("@Export", beData.Export);
                        cmd.CommandText = "ups_AddEntityAccess";
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                dal.CommitTransaction();

                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Allow User Wise Entity Access Update Success";

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
        public BE.Setup.EntityAccessCollections getEntityAccessList(int UserId, int? ForUserId,int? ForGroupId)
        {
            BE.Setup.EntityAccessCollections dataColl = new BE.Setup.EntityAccessCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
            cmd.Parameters.AddWithValue("@ForGroupId", ForGroupId);
            cmd.CommandText = "usp_GetEntityAccessList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.EntityAccess beData = new BE.Setup.EntityAccess();
                    if (!(reader[0] is DBNull)) beData.UserId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.GroupId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ModuleId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EntityId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ModuleName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EntityName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Full = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.View = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.Add = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.Modify = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.Delete = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.Print = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.Export = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.EntityTranId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Icon = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.WebUrl = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Description = reader.GetString(16);
                    if (beData.EntityTranId>0)
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

        public BE.Setup.EntityAccessCollections getEntityAccessList(int UserId)
        {
            BE.Setup.EntityAccessCollections dataColl = new BE.Setup.EntityAccessCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetEntityAccessListForUser";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Setup.EntityAccess beData = new BE.Setup.EntityAccess();
                    if (!(reader[0] is DBNull)) beData.UserId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.GroupId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.ModuleId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.EntityId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ModuleName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.EntityName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Full = reader.GetBoolean(6);
                    if (!(reader[7] is DBNull)) beData.View = reader.GetBoolean(7);
                    if (!(reader[8] is DBNull)) beData.Add = reader.GetBoolean(8);
                    if (!(reader[9] is DBNull)) beData.Modify = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.Delete = reader.GetBoolean(10);
                    if (!(reader[11] is DBNull)) beData.Print = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.Export = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.EntityTranId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.Icon = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.WebUrl = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Description = reader.GetString(16);

                    if (beData.EntityTranId>0)
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

        public ResponeValues checkEntity(int UserId, int EntityId,int Action)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@Action", Action);
            cmd.CommandText = "usp_GetCheckEntityAccessForUser";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
        
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

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


        public API.Admin.LastLoginLogCollections getLoginLog(int UserId, int? ForUserId, string ForUser,DateTime? dateFrom,DateTime? dateTo)
        {
            API.Admin.LastLoginLogCollections dataColl = new API.Admin.LastLoginLogCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@ForUserId", ForUserId);
            cmd.Parameters.AddWithValue("@ForUser", ForUser);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_LastTopLoginLog";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    API.Admin.LastLoginLog beData = new API.Admin.LastLoginLog();
                    if (!(reader[0] is DBNull)) beData.UserName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.GroupName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PublicIP = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.LogDateTimeAD = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.LogDateTimeBS = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.PhotoPath = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.PCName = reader.GetString(6);
                    
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
    }
}
