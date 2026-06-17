using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Infrastructure
{
    internal class FloorMappingDB
    {
        DataAccessLayer1 dal = null;
        public FloorMappingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdateColl(int UserId, BE.Infrastructure.FloorMappingCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                var fst = dataColl.First();

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@EntityId", fst.EntityId);
                cmd.Parameters.AddWithValue("@BuildingId", fst.BuildingId);
                cmd.CommandText = "usp_DelFloorMappingById";
                cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if (beData.FloorId == null || beData.FloorId == 0)
                        continue;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);
                    cmd.Parameters.AddWithValue("@FloorId", beData.FloorId);
                    cmd.Parameters.AddWithValue("@NoOfClassRooms", beData.NoOfClassRooms);
                    cmd.Parameters.AddWithValue("@NoOfOtherRooms", beData.NoOfOtherRooms);
                    cmd.Parameters.AddWithValue("@SafetyMeasures", beData.SafetyMeasures);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId ", beData.TranId);
                    cmd.Parameters.AddWithValue("@FloorType ", beData.FloorType);
                    cmd.CommandText = "usp_AddFloorMapping";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Floor Mapping Saved Successfully";
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
        public BE.Infrastructure.FloorMappingCollections getAllFloorMapping(int UserId, int EntityId, int? BuildingId)
        {
            BE.Infrastructure.FloorMappingCollections dataColl = new BE.Infrastructure.FloorMappingCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.CommandText = "usp_GetAllFloorMapping";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infrastructure.FloorMapping beData = new BE.Infrastructure.FloorMapping();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BuildingId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FloorId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.NoOfClassRooms = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.NoOfOtherRooms = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.SafetyMeasures = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.BuildingName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FloorName = reader.GetString(8);
                    //Add Field
                    if (!(reader[9] is DBNull)) beData.FloorType = reader.GetString(9);
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
        public BE.Infrastructure.FloorMappingCollections getFloorMappingById(int UserId, int EntityId, int BuildingId)
        {
            BE.Infrastructure.FloorMappingCollections dataColl = new BE.Infrastructure.FloorMappingCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetFloorMappingById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infrastructure.FloorMapping beData = new BE.Infrastructure.FloorMapping();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BranchId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BuildingId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FloorId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.NoOfClassRooms = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.NoOfOtherRooms = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.SafetyMeasures = reader.GetString(6);
                    //Add Field
                    if (!(reader[7] is DBNull)) beData.FloorType = reader.GetString(7);
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

        public BE.Infrastructure.FloorMapping DetailsByBuildingFloor(int UserId, int BuildingId, int FloorId)
        {
            BE.Infrastructure.FloorMapping beData = new BE.Infrastructure.FloorMapping();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.Parameters.AddWithValue("@FloorId", FloorId);
            cmd.CommandText = "usp_GetDetailsByBuildingFloor";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new BE.Infrastructure.FloorMapping();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BuildingId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FloorId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.NoOfClassRooms = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.NoOfOtherRooms = reader.GetInt32(4);
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