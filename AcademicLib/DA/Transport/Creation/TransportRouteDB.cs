using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Transport.Creation
{
    internal class TransportRouteDB
    {
        DataAccessLayer1 dal = null;
        public TransportRouteDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Transport.Creation.TransportRoute beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@VehicleId", beData.VehicleId);
            cmd.Parameters.AddWithValue("@FuelConsume", beData.FuelConsume);
            cmd.Parameters.AddWithValue("@ArrivalTime", beData.ArrivalTime);
            cmd.Parameters.AddWithValue("@DepartureTime", beData.DepartureTime);
            cmd.Parameters.AddWithValue("@Lat", beData.Lat);
            cmd.Parameters.AddWithValue("@Lan", beData.Lan);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@RouteId", beData.RouteId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateTransportRoute";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddTransportRoute";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@EndLan", beData.EndLan);
            cmd.Parameters.AddWithValue("@EndLat", beData.EndLat);
            cmd.Parameters.AddWithValue("@Radious", beData.Radious);

            cmd.Parameters.AddWithValue("@D_ArrivalTime", beData.D_ArrivalTime);
            cmd.Parameters.AddWithValue("@D_DepartureTime", beData.D_DepartureTime);
            cmd.Parameters.AddWithValue("@D_Lat", beData.D_Lat);
            cmd.Parameters.AddWithValue("@D_Lan", beData.D_Lan);
            cmd.Parameters.AddWithValue("@D_EndLan", beData.D_EndLan);
            cmd.Parameters.AddWithValue("@D_EndLat", beData.D_EndLat);

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


        public AcademicLib.BE.Transport.Creation.TransportRouteCollections getAllTransportRoute(int UserId, int EntityId)
        {
            AcademicLib.BE.Transport.Creation.TransportRouteCollections dataColl = new AcademicLib.BE.Transport.Creation.TransportRouteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllTransportRoute";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.TransportRoute beData = new AcademicLib.BE.Transport.Creation.TransportRoute();
                    beData.RouteId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FuelConsume = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ArrivalTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.DepartureTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.Lat = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Lan = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.VehicleName = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.EndLat = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.EndLan = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Radious = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.D_ArrivalTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.D_DepartureTime = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.D_Lat = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.D_Lan = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.D_EndLat = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.D_EndLan = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.VehicleNumber = reader.GetString(18);

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
        public AcademicLib.BE.Transport.Creation.TransportRouteCollections getTransportRouteByVehicleId(int UserId, int VehicleId)
        {
            AcademicLib.BE.Transport.Creation.TransportRouteCollections dataColl = new AcademicLib.BE.Transport.Creation.TransportRouteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
            cmd.CommandText = "usp_GetTransportRouteByVehicleId";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.TransportRoute beData = new AcademicLib.BE.Transport.Creation.TransportRoute();
                    beData.RouteId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FuelConsume = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ArrivalTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.DepartureTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.Lat = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Lan = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.VehicleName = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.EndLat = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.EndLan = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Radious = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.D_ArrivalTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.D_DepartureTime = reader.GetDateTime(13);
                    if (!(reader[14] is DBNull)) beData.D_Lat = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.D_Lan = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.D_EndLat = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.D_EndLan = Convert.ToDouble(reader[17]);

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
        public AcademicLib.BE.Transport.Creation.TransportRoute getTransportRouteById(int UserId, int EntityId, int RouteId)
        {
            AcademicLib.BE.Transport.Creation.TransportRoute beData = new AcademicLib.BE.Transport.Creation.TransportRoute();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteId", RouteId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetTransportRouteById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Transport.Creation.TransportRoute();
                    beData.RouteId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FuelConsume = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.ArrivalTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.DepartureTime = reader.GetDateTime(5);
                    if (!(reader[6] is DBNull)) beData.Lat = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Lan = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EndLat = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.EndLan = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Radious = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.D_ArrivalTime = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.D_DepartureTime = reader.GetDateTime(12);
                    if (!(reader[13] is DBNull)) beData.D_Lat = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.D_Lan = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.D_EndLat = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.D_EndLan = Convert.ToDouble(reader[16]);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int RouteId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@RouteId", RouteId);
            cmd.CommandText = "usp_DelTransportRouteById";
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
