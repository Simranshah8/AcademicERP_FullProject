using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Hostel
{
    internal class HostelAttendanceDB
    {
        DataAccessLayer1 dal = null;
        public HostelAttendanceDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public BE.Hostel.HostelAttendanceCollections getAllHostelAttendance(int UserId, int AcademicYearId, int? HostelId, int? BuildingId, int? FloorId, DateTime? ForDate, int? ShiftId)
        {
            BE.Hostel.HostelAttendanceCollections dataColl = new BE.Hostel.HostelAttendanceCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@HostelId", HostelId);
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.Parameters.AddWithValue("@FloorId", FloorId);
            cmd.Parameters.AddWithValue("@ForDate", ForDate);
            cmd.Parameters.AddWithValue("@ShiftId", ShiftId);
            cmd.CommandText = "usp_GetAllHostelAttendance";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Hostel.HostelAttendance beData = new BE.Hostel.HostelAttendance();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.StudentName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassSection = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RoomDetail = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StatusId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ObservationRemarks = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ShiftName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.StatusName = reader.GetString(9);
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

        public ResponeValues SaveUpdate(BE.Hostel.HostelAttendanceCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                var fData = dataColl.First();
                cmd.Parameters.AddWithValue("@UserId", fData.CUserId);
                cmd.Parameters.AddWithValue("@HostelId", fData.HostelId);
                cmd.Parameters.AddWithValue("@BuildingId ", fData.BuildingId);
                cmd.Parameters.AddWithValue("@FloorId", fData.FloorId);
                cmd.Parameters.AddWithValue("@ForDate", fData.ForDate);
                cmd.Parameters.AddWithValue("@ShiftId", fData.ShiftId);
                cmd.CommandText = "usp_DelHostelAttendance";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if (!beData.StatusId.HasValue)
                        continue;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@HostelId", beData.HostelId);
                    cmd.Parameters.AddWithValue("@BuildingId ", beData.BuildingId);
                    cmd.Parameters.AddWithValue("@FloorId", beData.FloorId);
                    cmd.Parameters.AddWithValue("@ForDate", beData.ForDate);
                    cmd.Parameters.AddWithValue("@ShiftId", beData.ShiftId);
                    cmd.Parameters.AddWithValue("@ObservationRemarks", beData.ObservationRemarks);
                    cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
                    cmd.Parameters.AddWithValue("@StatusId", beData.StatusId);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
                    cmd.CommandText = "usp_AddHostelAttendance";
                    cmd.ExecuteNonQuery();

                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Student Daily Hostel Attendance Done";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
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