using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class FeeItemGroupDB
    {
        DataAccessLayer1 dal = null;
        public FeeItemGroupDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Fee.Creation.FeeItemGroup beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@FeeItemGroupId", beData.FeeItemGroupId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateFeeItemGroup";
            }
            else
            {
                cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddFeeItemGroup";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.IsSuccess && resVal.RId > 0 && beData.FeeItemIdColl != null && beData.FeeItemIdColl.Count > 0)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    foreach (var m in beData.FeeItemIdColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@FeeItemGroupId", resVal.RId);
                        cmd.Parameters.AddWithValue("@FeeItemId", m);
                        cmd.CommandText = "insert into tbl_FeeItemGroupDetails(FeeItemGroupId,FeeItemId) values(@FeeItemGroupId,@FeeItemId)";
                        cmd.ExecuteNonQuery();
                    }
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
        public BE.Fee.Creation.FeeItemGroupCollections getAllFeeItemGroup(int UserId, int EntityId)
        {
            BE.Fee.Creation.FeeItemGroupCollections dataColl = new BE.Fee.Creation.FeeItemGroupCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllFeeItemGroup";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Fee.Creation.FeeItemGroup beData = new BE.Fee.Creation.FeeItemGroup();
                    beData.FeeItemGroupId = reader.GetInt32(0);                    
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);                    
                    beData.ResponseMSG = GLOBALMSG.SUCCESS;
                    beData.IsSuccess = true;
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
        public BE.Fee.Creation.FeeItemGroup getFeeItemGroupById(int UserId, int EntityId, int FeeItemGroupId)
        {
            BE.Fee.Creation.FeeItemGroup beData = new BE.Fee.Creation.FeeItemGroup();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FeeItemGroupId", FeeItemGroupId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetFeeItemGroupById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Fee.Creation.FeeItemGroup();
                    beData.FeeItemGroupId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);                  

                }
                reader.NextResult();
                beData.FeeItemIdColl = new List<int>();
                while (reader.Read())
                {
                    beData.FeeItemIdColl.Add(reader.GetInt32(0));
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
        public ResponeValues DeleteById(int UserId, int EntityId, int FeeItemGroupId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@FeeItemGroupId", FeeItemGroupId);
            cmd.CommandText = "usp_DelFeeItemGroupById";
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
