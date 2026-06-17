using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{
    internal class HouseDressDB
    {
        DataAccessLayer1 dal = null;
        public HouseDressDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Academic.Creation.HouseDress beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@Description", beData.Description);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@HouseDressId", beData.HouseDressId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateHouseDress";
            }
            else
            {
                cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddHouseDress";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@HouseNameId", beData.HouseNameId);
            cmd.Parameters.AddWithValue("@ColorCode", beData.ColorCode);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[8].Value);

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
        public BE.Academic.Creation.HouseDressCollections getAllHouseDress(int UserId, int EntityId)
        {
            BE.Academic.Creation.HouseDressCollections dataColl = new BE.Academic.Creation.HouseDressCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllHouseDress";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.HouseDress beData = new BE.Academic.Creation.HouseDress();
                    beData.HouseDressId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.HouseNameId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ColorCode = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.HouseName = reader.GetString(6);
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
        public BE.Academic.Creation.HouseDress getHouseDressById(int UserId, int EntityId, int HouseDressId)
        {
            BE.Academic.Creation.HouseDress beData = new BE.Academic.Creation.HouseDress();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HouseDressId", HouseDressId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetHouseDressById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.HouseDress();
                    beData.HouseDressId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.HouseNameId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ColorCode = reader.GetString(5);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int HouseDressId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@HouseDressId", HouseDressId);
            cmd.CommandText = "usp_DelHouseDressById";
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
