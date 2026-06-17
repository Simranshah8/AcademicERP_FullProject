using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.DA.Hostel
{
   internal class RoomDB
    {

        DataAccessLayer1 dal = null;
        public RoomDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Hostel.Room beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HostelId", beData.HostelId);
            cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);
            cmd.Parameters.AddWithValue("@FloorId", beData.FloorId);
            cmd.Parameters.AddWithValue("@RoomName", beData.RoomName);
            cmd.Parameters.AddWithValue("@RoomFee", beData.RoomFee);
            cmd.Parameters.AddWithValue("@NoOfBeds", beData.NoOfBeds);
            cmd.Parameters.AddWithValue("@ImagePath", beData.ImagePath);
            //
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@RoomId", beData.RoomId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateRoom";
            }
            else
            {
                cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddRoom";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[10].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[11].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[12].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@UpdateInMapping", beData.UpdateInMapping);

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

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    if(!resVal.ResponseMSG.Contains("Rate and"))
                        SaveRoomBed(beData.CUserId, resVal.RId, beData.RoomBedColl);

                    SaveRoomAsset(beData.CUserId, resVal.RId, beData.RoomAssetColl);

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
        private void SaveRoomBed(int UserId, int RoomId, List<BE.Hostel.RoomBed> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || RoomId == 0)
                return;

            foreach (BE.Hostel.RoomBed beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@RoomId", RoomId);
                cmd.Parameters.AddWithValue("@BedNo", beData.BedNo);
                cmd.Parameters.AddWithValue("@BedName", beData.BedName);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddRoomBed";
                cmd.ExecuteNonQuery();
            }

        }
        private void SaveRoomAsset(int UserId, int RoomId, List<BE.Hostel.RoomAsset> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || RoomId == 0)
                return;

            foreach (BE.Hostel.RoomAsset beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@RoomId", RoomId);
                cmd.Parameters.AddWithValue("@Particulars", beData.Particulars);
                cmd.Parameters.AddWithValue("@Qty", beData.Qty);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddRoomAsset";
                cmd.ExecuteNonQuery();
            }

        }

        public AcademicLib.BE.Hostel.RoomDetailsForMappingCollections getAllRoomForMapping(int UserId)
        {
            AcademicLib.BE.Hostel.RoomDetailsForMappingCollections dataColl = new BE.Hostel.RoomDetailsForMappingCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetRoomBedForMapping";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Hostel.RoomDetailsForMapping beData = new BE.Hostel.RoomDetailsForMapping();                    
                    if (!(reader[0] is DBNull)) beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.BedId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.BedNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RoomFee = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.IsVacant = Convert.ToBoolean(reader[5]);

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

        public AcademicLib.BE.Hostel.RoomCollections getAllRoom(int UserId, int EntityId)
        {
            AcademicLib.BE.Hostel.RoomCollections dataColl = new AcademicLib.BE.Hostel.RoomCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetAllRoom";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Hostel.Room beData = new AcademicLib.BE.Hostel.Room();
                    beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.HostelId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BuildingId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FloorId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RoomName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RoomFee = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.NoOfBeds = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ImagePath = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.HostelName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.BuildingName = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FloorName = reader.GetString(10);

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
        public AcademicLib.BE.Hostel.Room getRoomById(int UserId, int EntityId, int RoomId)
        {
            AcademicLib.BE.Hostel.Room beData = new AcademicLib.BE.Hostel.Room();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetRoomById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Hostel.Room();
                    beData.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.HostelId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BuildingId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FloorId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RoomName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RoomFee = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.NoOfBeds = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ImagePath = reader.GetString(7);

                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Hostel.RoomBed Bed = new AcademicLib.BE.Hostel.RoomBed();

                    if (!(reader[0] is System.DBNull)) Bed.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Bed.BedNo = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) Bed.BedName = reader.GetString(2);
                    
                    beData.RoomBedColl.Add(Bed);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Hostel.RoomAsset Asset = new AcademicLib.BE.Hostel.RoomAsset();

                    if (!(reader[0] is System.DBNull)) Asset.RoomId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Asset.Particulars = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Asset.Qty = Convert.ToDouble(reader[2]);

                    beData.RoomAssetColl.Add(Asset);
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
        public ResponeValues DeleteById(int UserId, int EntityId, int RoomId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@RoomId", RoomId);
            cmd.CommandText = "usp_DelRoomById";
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
