using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Infrastructure
{

    internal class LandDetailsDB
    {
        DataAccessLayer1 dal = null;
        public LandDetailsDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Infrastructure.LandDetails beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TotalArea", beData.TotalArea);
            cmd.Parameters.AddWithValue("@OwnerShipId", beData.OwnerShipId);
            cmd.Parameters.AddWithValue("@OtherOwnerShip", beData.OtherOwnerShip);
            cmd.Parameters.AddWithValue("@UtilizationId", beData.UtilizationId);
            cmd.Parameters.AddWithValue("@OtherUtilizationType", beData.OtherUtilizationType);
            cmd.Parameters.AddWithValue("@Attachment", beData.Attachment);
            cmd.Parameters.AddWithValue("@LandRemarks", beData.LandRemarks);

            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@LandDetailsId", beData.LandDetailsId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateLandDetails";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddLandDetails";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@unit", beData.unit);
            cmd.Parameters.AddWithValue("@sheetNo", beData.sheetNo);
            cmd.Parameters.AddWithValue("@kittaNo", beData.kittaNo);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!(cmd.Parameters[10].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[10].Value);

                if (!(cmd.Parameters[11].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[11].Value);

                if (!(cmd.Parameters[12].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[12].Value);

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

        public ResponeValues DeleteById(int UserId, int EntityId, int LandDetailsId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@LandDetailsId", LandDetailsId);
            cmd.CommandText = "usp_DelLandDetailsById";
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
        public BE.Infrastructure.LandDetailsCollections getAllLandDetails(int UserId, int EntityId)
        {
            BE.Infrastructure.LandDetailsCollections dataColl = new BE.Infrastructure.LandDetailsCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllLandDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infrastructure.LandDetails beData = new BE.Infrastructure.LandDetails();
                    if (!(reader[0] is DBNull)) beData.LandDetailsId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TotalArea = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.OwnerShipId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.OtherOwnerShip = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.UtilizationId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.OtherUtilizationType = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Attachment = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LandRemarks = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Utilization = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.OwnerShip = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.unit = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.sheetNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.kittaNo = reader.GetString(12);
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
        public BE.Infrastructure.LandDetails getLandDetailsById(int UserId, int EntityId, int LandDetailsId)
        {
            BE.Infrastructure.LandDetails beData = new BE.Infrastructure.LandDetails();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LandDetailsId", LandDetailsId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetLandDetailsById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Infrastructure.LandDetails();
                    if (!(reader[0] is DBNull)) beData.LandDetailsId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TotalArea = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.OwnerShipId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.OtherOwnerShip = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.UtilizationId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.OtherUtilizationType = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Attachment = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.LandRemarks = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.unit = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.sheetNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.kittaNo = reader.GetString(10);
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

