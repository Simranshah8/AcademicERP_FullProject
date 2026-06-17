using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Transport.Creation
{
    internal class TransportAttDB
    {
        DataAccessLayer1 dal = null;
        public TransportAttDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(int UserId, BE.Transport.Creation.TransportAttCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                if (dataColl.Count > 0)
                {
                    var fst = dataColl[0];
                    cmd.CommandText = "usp_delTransportAtt";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@ForDate", fst.ForDate);
                    cmd.Parameters.AddWithValue("@VehicleId", fst.VehicleId);
                    cmd.Parameters.AddWithValue("@RouteId", fst.RouteId);

                    var uniqueAttendanceForIds = dataColl.Select(x => x.AttendanceForId).Distinct().ToList();

                    foreach (var attId in uniqueAttendanceForIds)
                    {
                        cmd.Parameters.AddWithValue("@AttendanceForId", attId);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.RemoveAt("@AttendanceForId");
                    }
                }

                foreach (var beData in dataColl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
                    cmd.Parameters.AddWithValue("@VehicleId", beData.VehicleId);
                    cmd.Parameters.AddWithValue("@AttendanceForId", beData.AttendanceForId);
                    cmd.Parameters.AddWithValue("@RouteId", beData.RouteId);
                    cmd.Parameters.AddWithValue("@StudentID", beData.StudentId);
                    cmd.Parameters.AddWithValue("@VehiclePointId", beData.VehiclePointId);
                    cmd.Parameters.AddWithValue("@Attendance", beData.Attendance);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);

                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    cmd.CommandText = "usp_AddTransportAtt";
                    cmd.ExecuteNonQuery();
                    
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Transport Attendance Saved Successfully";
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

        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelTransportAttById";
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
        public BE.Transport.Creation.TransportAtt getTransportAttById(int UserId, int EntityId, DateTime ForDate)
        {
            BE.Transport.Creation.TransportAtt beData = new BE.Transport.Creation.TransportAtt();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetTransportAttById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Transport.Creation.TransportAtt();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ForDate = Convert.ToDateTime(reader[1]);
                    if (!(reader[2] is DBNull)) beData.VehicleId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.AttendanceForId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RouteId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.StudentId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.VehiclePointId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.PickUp = Convert.ToBoolean(reader[7]);
                    if (!(reader[8] is DBNull)) beData.DropOff = Convert.ToBoolean(reader[8]);
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

        public BE.Transport.Creation.TransportAttCollections getAllStudentTransportAtt(int UserId, int EntityId, int VehicleId, int RouteId, DateTime ForDate, int AttendanceForId, int? AcademicYearId)
        {
            BE.Transport.Creation.TransportAttCollections dataColl = new BE.Transport.Creation.TransportAttCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
            cmd.Parameters.AddWithValue("@RouteId", RouteId);
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@AttendanceForId", AttendanceForId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetStudentForTransportAtt";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.TransportAtt beData = new BE.Transport.Creation.TransportAtt();
                    beData.ForDate = ForDate;
                    beData.AttendanceForId = AttendanceForId;
                    beData.VehicleId = VehicleId;
                    beData.RouteId = RouteId;
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.VehicleId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RouteId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RegNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StudentName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Class = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Section = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.SAddress = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.PointName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.VehiclePointId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.Attendance = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.Remarks = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ForDate = Convert.ToDateTime(reader[14]);
                    if (!(reader[15] is DBNull)) beData.PointId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.Batch = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.ClassYear = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Semester = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.AttendanceForId = reader.GetInt32(19);
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
        public BE.Transport.Creation.TransportAttCollections getAllTransportAttendanceList(int UserId, int EntityId, DateTime? DateFrom, DateTime? DateTo)
        {
            BE.Transport.Creation.TransportAttCollections dataColl = new BE.Transport.Creation.TransportAttCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.CommandText = "usp_GetAllTransportAtt";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.TransportAtt beData = new BE.Transport.Creation.TransportAtt();
                    if (!(reader[0] is DBNull)) beData.ForDate = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) beData.ForMiti = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.VehicleNumber = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Route = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.TotalPickup = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TotalDropOff = Convert.ToInt32(reader[6]);
                    if (!(reader[7] is DBNull)) beData.VehicleId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.RouteId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.TotalStudents = reader.GetInt32(9);
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

        //Add by Prashnt
        //GetDetails
        public BE.Transport.Creation.TransportAttCollections GetTransportAttDetail(int UserId, int? EntityId, DateTime ForDate, int VehicleId,int RouteId)
        {
            BE.Transport.Creation.TransportAttCollections dataColl = new BE.Transport.Creation.TransportAttCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
            cmd.Parameters.AddWithValue("@RouteId", RouteId);
            cmd.CommandText = "usp_GetTransportAttDetail";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Transport.Creation.TransportAtt beData = new BE.Transport.Creation.TransportAtt();
                    if (!(reader[0] is DBNull)) beData.ForDate = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) beData.ForMiti = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.VehicleName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.VehicleNumber = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Route = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Attendance = reader.GetBoolean(5);
                    if (!(reader[6] is DBNull)) beData.DriverName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ConductorName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.InchargeName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.StudentId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.RegNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Address = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ClassDetails = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TranId = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.StudentName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.TransportPoint = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Remarks = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.AttendanceForId = reader.GetInt32(18);
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

