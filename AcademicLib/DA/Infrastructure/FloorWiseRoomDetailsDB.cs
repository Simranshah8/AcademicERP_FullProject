using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Infrastructure
{
    internal class FloorWiseRoomDetailsDB
    {
        DataAccessLayer1 dal = null;
        public FloorWiseRoomDetailsDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public ResponeValues SaveUpdate(int UserId, BE.Infrastructure.FloorWiseRoomDetailsCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                //var fst = dataColl.First();
                //cmd.Parameters.AddWithValue("@BuildingId", fst.BuildingId);
                //cmd.Parameters.AddWithValue("@FloorId", fst.FloorId);
                //cmd.CommandText = "usp_DelRoomDetailById";
                //cmd.ExecuteNonQuery();

                foreach (var beData in dataColl)
                {
                    if (string.IsNullOrWhiteSpace(beData.Name))
                        continue;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);
                    cmd.Parameters.AddWithValue("@FloorId", beData.FloorId);
                    cmd.Parameters.AddWithValue("@Name", beData.Name);
                    cmd.Parameters.AddWithValue("@UtilitiesId", beData.UtilitiesId);
                    cmd.Parameters.AddWithValue("@SubUtility", beData.SubUtility);
                    cmd.Parameters.AddWithValue("@Length", beData.Length);
                    cmd.Parameters.AddWithValue("@Breadth", beData.Breadth);
                    cmd.Parameters.AddWithValue("@Capacity", beData.Capacity);
                    cmd.Parameters.AddWithValue("@Resources", beData.Resources);
                    cmd.Parameters.AddWithValue("@RoomTypeId", beData.RoomTypeId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@FloorWiseRoomDetailsId ", beData.FloorWiseRoomDetailsId);
                    cmd.CommandText = "usp_AddFloorWiseRoomDetails";
                    cmd.ExecuteNonQuery();
                }
                dal.CommitTransaction();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Room Details Saved Successfully";
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


        //public ResponeValues SaveUpdate(BE.Infrastructure.FloorWiseRoomDetails beData, bool isModify)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    dal.OpenConnection();
        //    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    try
        //    {
        //        foreach (var dataColl in beData.subFloorWiseRoomDetailsColl)
        //    {
        //        dal.OpenConnection();
        //        cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);
        //        cmd.Parameters.AddWithValue("@FloorId", beData.FloorId);
        //        cmd.Parameters.AddWithValue("@Name", dataColl.Name);
        //        cmd.Parameters.AddWithValue("@UtilitiesId", dataColl.UtilitiesId);
        //        cmd.Parameters.AddWithValue("@SubUtility", dataColl.SubUtility);
        //        cmd.Parameters.AddWithValue("@Length", dataColl.Length);
        //        cmd.Parameters.AddWithValue("@Breadth", dataColl.Breadth);
        //        cmd.Parameters.AddWithValue("@Capacity", dataColl.Capacity);
        //        cmd.Parameters.AddWithValue("@Resources", dataColl.Resources);
        //        cmd.Parameters.AddWithValue("@RoomTypeId", dataColl.RoomTypeId);

        //        cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
        //        cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
        //        cmd.Parameters.AddWithValue("@FloorWiseRoomDetailsId", beData.FloorWiseRoomDetailsId);

        //        if (isModify)
        //        {
        //            cmd.CommandText = "usp_UpdateFloorWiseRoomDetails";
        //        }
        //        else
        //        {
        //            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
        //            cmd.CommandText = "usp_AddFloorWiseRoomDetails";
        //        }
        //        cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
        //        cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
        //        cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
        //        cmd.Parameters[13].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters[14].Direction = System.Data.ParameterDirection.Output;
        //        cmd.Parameters[15].Direction = System.Data.ParameterDirection.Output;
               
        //            cmd.ExecuteNonQuery();
        //            if (!(cmd.Parameters[12].Value is DBNull))
        //                resVal.RId = Convert.ToInt32(cmd.Parameters[12].Value);

        //            if (!(cmd.Parameters[13].Value is DBNull))
        //                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[13].Value);

        //            if (!(cmd.Parameters[14].Value is DBNull))
        //                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[14].Value);

        //            if (!(cmd.Parameters[15].Value is DBNull))
        //                resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[15].Value);

        //            if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
        //                resVal.ResponseMSG = resVal.ResponseMSG + "(" + resVal.ErrorNumber.ToString() + ")";

        //        }
        //        catch (System.Data.SqlClient.SqlException ee)
        //        {
        //            resVal.IsSuccess = false;
        //            resVal.ResponseMSG = ee.Message;
        //        }
        //        catch (Exception ee)
        //        {
        //            resVal.IsSuccess = false;
        //            resVal.ResponseMSG = ee.Message;
        //        }
        //        finally
        //        {
        //            dal.CloseConnection();
        //        }
        //    }

        //    return resVal;

        //}
        
        public BE.Infrastructure.FloorWiseRoomDetailsCollections getAllFloorWiseRoomDetails(int UserId, int EntityId, int? BuildingId, int? FloorId)
        {
            BE.Infrastructure.FloorWiseRoomDetailsCollections dataColl = new BE.Infrastructure.FloorWiseRoomDetailsCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
            cmd.Parameters.AddWithValue("@FloorId", FloorId);
            cmd.CommandText = "usp_GetAllFloorWiseRoomDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BE.Infrastructure.FloorWiseRoomDetails beData = new BE.Infrastructure.FloorWiseRoomDetails();
                    if (!(reader[0] is DBNull)) beData.FloorWiseRoomDetailsId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BuildingId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FloorId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.UtilitiesId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.SubUtility = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Length = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Breadth = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Capacity = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.Resources = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.RoomTypeId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.Utilities = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.BuildingName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.FloorName = reader.GetString(13);
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



        //Added by suresh on 15 falgun for room distribution reportingh
        public RE.Infrastructure.RoomDistributionCollections getAllRoomDistribution(int UserId, int EntityId)
        {
            RE.Infrastructure.RoomDistributionCollections dataColl = new RE.Infrastructure.RoomDistributionCollections();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllRoomSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Infrastructure.RoomDistribution beData = new RE.Infrastructure.RoomDistribution();
                    if (!(reader[0] is DBNull)) beData.BuildingId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FloorId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BuildingName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.FloorName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.NoOfClassRooms = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.NoOfOtherRooms = reader.GetInt32(5);
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