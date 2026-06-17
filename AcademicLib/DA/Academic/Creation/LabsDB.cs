using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Academic.Creation
{

    internal class LabsDB
    {
        DataAccessLayer1 dal = null;
        public LabsDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(BE.Academic.Creation.Labs beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LabName", beData.LabName);
            cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);
            cmd.Parameters.AddWithValue("@AreaCoveredByLab", beData.AreaCoveredByLab);
            cmd.Parameters.AddWithValue("@LabType", beData.LabType);
            cmd.Parameters.AddWithValue("@AdequencyOfLabEquipment", beData.AdequencyOfLabEquipment);
            cmd.Parameters.AddWithValue("@HasInternetConnection", beData.HasInternetConnection);
            cmd.Parameters.AddWithValue("@EquipmentAtLab", beData.EquipmentAtLab);
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@LabsId", beData.LabsId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateLabs";
            }
            else
            {
                cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddLabs";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
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

        public ResponeValues DeleteById(int UserId, int EntityId, int LabsId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@LabsId", LabsId);
            cmd.CommandText = "usp_DelLabsById";
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
        public BE.Academic.Creation.LabsCollections getAllLabs(int UserId)
        {
            BE.Academic.Creation.LabsCollections dataColl = new BE.Academic.Creation.LabsCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetAllLabs";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Academic.Creation.Labs beData = new BE.Academic.Creation.Labs();
                    if (!(reader[0] is DBNull)) beData.LabsId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LabName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.BuildingId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.BuildingName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.AreaCoveredByLab = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.LabType = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AdequencyOfLabEquipment = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData.HasInternetConnection = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EquipmentAtLab = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Remarks = reader.GetString(9);
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
        public BE.Academic.Creation.Labs getLabsById(int UserId, int EntityId, int LabsId)
        {
            BE.Academic.Creation.Labs beData = new BE.Academic.Creation.Labs();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LabsId", LabsId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetLabsById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Academic.Creation.Labs();
                    if (!(reader[0] is DBNull)) beData.LabsId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.LabName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.BuildingId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.BuildingName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.AreaCoveredByLab = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.LabType = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AdequencyOfLabEquipment = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData.HasInternetConnection = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EquipmentAtLab = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Remarks = reader.GetString(9);
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

