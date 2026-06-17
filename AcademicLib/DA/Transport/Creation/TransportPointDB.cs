using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Transport.Creation
{
    internal class TransportPointDB
    {
        DataAccessLayer1 dal = null;
        public TransportPointDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(AcademicLib.BE.Transport.Creation.TransportPoint beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", beData.Name);
            cmd.Parameters.AddWithValue("@RouteId", 0);
            cmd.Parameters.AddWithValue("@PickupTime", beData.PickupTime);
            cmd.Parameters.AddWithValue("@DropTime", beData.DropTime);
            cmd.Parameters.AddWithValue("@InRate", beData.InRate);
            cmd.Parameters.AddWithValue("@OutRate", beData.OutRate);
            cmd.Parameters.AddWithValue("@BothRate", beData.BothRate);
            cmd.Parameters.AddWithValue("@Lat", beData.Lat);
            cmd.Parameters.AddWithValue("@Lan", beData.Lan);
            cmd.Parameters.AddWithValue("@Description", beData.Description);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@PointId", beData.PointId);
            if (isModify)
            {
                cmd.CommandText = "usp_UpdateTransportPoint";
            }
            else
            {
                cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddTransportPoint";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@UpdateInMapping", beData.UpdateInMapping);
            cmd.Parameters.AddWithValue("@OrderNo", beData.OrderNo);

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

                if (resVal.IsSuccess && resVal.RId > 0 && beData.RouteIdColl != null && beData.RouteIdColl.Count > 0)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach (var m in beData.RouteIdColl)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                        cmd.Parameters.AddWithValue("@PointId", resVal.RId);
                        cmd.Parameters.AddWithValue("@RouteId", m);
                        cmd.CommandText = "usp_AddTransportPointRoutes";
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


        public AcademicLib.BE.Transport.Creation.TransportPointCollections getAllTransportPoint(int UserId, int EntityId)
        {
            AcademicLib.BE.Transport.Creation.TransportPointCollections dataColl = new AcademicLib.BE.Transport.Creation.TransportPointCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllTransportPoint";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.TransportPoint beData = new AcademicLib.BE.Transport.Creation.TransportPoint();
                    beData.PointId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    //if (!(reader[2] is DBNull)) beData.RouteId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.PickupTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DropTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.InRate = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.OutRate = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.BothRate = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Lat = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Lan = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Description = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.RouteName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.OrderNo = Convert.ToInt32(reader[12]);
                    beData.RouteIdColl = new List<int>();
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    try
                    {
                        int pid = reader.GetInt32(0);
                        var findPoint = dataColl.Find(p1 => p1.PointId == pid);

                        if (findPoint != null)
                        {
                            int rid = reader.GetInt32(1);
                            findPoint.RouteIdColl.Add(rid);
                        }
                        
                    }
                    catch { }
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
        public AcademicLib.BE.Transport.Creation.TransportPoint getTransportPointById(int UserId, int EntityId, int PointId)
        {
            AcademicLib.BE.Transport.Creation.TransportPoint beData = new AcademicLib.BE.Transport.Creation.TransportPoint();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PointId", PointId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetTransportPointById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Transport.Creation.TransportPoint();
                    beData.PointId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                   // if (!(reader[2] is DBNull)) beData.RouteId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.PickupTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DropTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.InRate = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.OutRate = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.BothRate = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Lat = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Lan = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Description = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.OrderNo = Convert.ToInt32(reader[11]);

                }
                reader.NextResult();
                beData.RouteIdColl = new List<int>();
                while (reader.Read())
                {
                    beData.RouteIdColl.Add(reader.GetInt32(0));
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
        public ResponeValues DeleteById(int UserId, int EntityId, int PointId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@PointId", PointId);
            cmd.CommandText = "usp_DelTransportPointById";
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

        public AcademicLib.API.Teacher.TransportRouteCollections getAllPickupPointsForMap(int UserId)
        {
            AcademicLib.API.Teacher.TransportRouteCollections dataColl = new API.Teacher.TransportRouteCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetTransportRouteDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Teacher.TransportRoute beData = new API.Teacher.TransportRoute();
                    beData.VehicleId = reader.GetInt32(0);
                    beData.RouteId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RouteName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ArrivalTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.DepartureTime = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.StartPointLat = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.StartPointLng = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.EndPointLat = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.EndPointLng = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Radious = Convert.ToDouble(reader[9]);

                    if (!(reader[10] is DBNull)) beData.D_ArrivalTime = reader.GetDateTime(10);
                    if (!(reader[11] is DBNull)) beData.D_DepartureTime = reader.GetDateTime(11);
                    if (!(reader[12] is DBNull)) beData.D_StartPointLat = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.D_StartPointLng = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.D_EndPointLat = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.D_EndPointLng = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.DriverId = Convert.ToInt32(reader[16]);
                    if (!(reader[17] is DBNull)) beData.UserId = Convert.ToInt32(reader[17]);
                    if (!(reader[18] is DBNull)) beData.VehicleName = Convert.ToString(reader[18]);
                    if (!(reader[19] is DBNull)) beData.VehicleNo = Convert.ToString(reader[19]);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Teacher.PickupPoints beData = new API.Teacher.PickupPoints();                    
                    int rid = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.PickupPointName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.PickupTime = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.PickAtLat = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.PickAtLng = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.PointId = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) beData.IsPickPoint = Convert.ToBoolean(reader[6]);
                    if (!(reader[7] is DBNull)) beData.VehicleName = Convert.ToString(reader[7]);
                    if (!(reader[8] is DBNull)) beData.VehicleNo = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.PickupRate = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.DropRate = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.BothRate = Convert.ToDouble(reader[11]);

                    dataColl.Find(p1 => p1.RouteId == rid).PickupPointColl.Add(beData);
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

        public AcademicLib.API.Student.TransportPoint getPickupPoint(int UserId)
        {
            AcademicLib.API.Student.TransportPoint beData = new API.Student.TransportPoint();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GetPickupPointOfStudent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    beData = new API.Student.TransportPoint();                    
                    beData.RouteId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RouteName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ArrivalTime = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.DepartureTime = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.StartPointLat = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.StartPointLng = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.EndPointLat = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.EndPointLng = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.PickupPointName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PickupTime = reader.GetDateTime(9);
                    if (!(reader[10] is DBNull)) beData.PickAtLat = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.PickAtLng = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.PointId = Convert.ToInt32(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Radious = Convert.ToDouble(reader[13]);

                    if (!(reader[14] is DBNull)) beData.D_ArrivalTime = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.D_DepartureTime = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.D_StartPointLat = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.D_StartPointLng = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.D_EndPointLat = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.D_EndPointLng = Convert.ToDouble(reader[19]);

                    if (!(reader[20] is DBNull)) beData.DriverId = Convert.ToInt32(reader[20]);
                    if (!(reader[21] is DBNull)) beData.UserId = Convert.ToInt32(reader[21]);
                    if (!(reader[22] is DBNull)) beData.GPSDevice = Convert.ToString(reader[22]);

                    if (!(reader[23] is DBNull)) beData.Url = Convert.ToString(reader[23]);
                    if (!(reader[24] is DBNull)) beData.User = Convert.ToString(reader[24]);
                    if (!(reader[25] is DBNull)) beData.Pwd = Convert.ToString(reader[25]);
                    if (!(reader[26] is DBNull)) beData.Authentication = Convert.ToString(reader[26]);
                    if (!(reader[27] is DBNull)) beData.Token = Convert.ToString(reader[27]);
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

        public AcademicLib.RE.Transport.MSGForPickupPointCollections getPickUpMSG(int UserId,string pointIdColl,int msgFor, string nextPointIdColl, string pastPointIdColl)
        {
            AcademicLib.RE.Transport.MSGForPickupPointCollections dataColl = new RE.Transport.MSGForPickupPointCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PointIdColl", pointIdColl);
            cmd.Parameters.AddWithValue("@pastPointIdColl", pastPointIdColl);
            cmd.Parameters.AddWithValue("@nextPointIdColl", nextPointIdColl);
            cmd.Parameters.AddWithValue("@msgFor", msgFor);
            cmd.CommandText = "usp_GetMSGForNotificationToNearPickupPoint";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Transport.MSGForPickupPoint beData = new RE.Transport.MSGForPickupPoint();
                    beData.UserId = reader.GetInt32(0);
                    beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Message = reader.GetString(2);                    
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
