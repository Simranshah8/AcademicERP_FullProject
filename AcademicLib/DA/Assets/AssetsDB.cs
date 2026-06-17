using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.AssetsMgmt
{

	internal class AssetsDB
	{
		DataAccessLayer1 dal = null;
		public AssetsDB(string hostName, string dbName)
		{
			dal = new DataAccessLayer1(hostName, dbName);
		}

		public ResponeValues SaveUpdate(int UserId, BE.AssetsMgmt.AssetsCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			dal.OpenConnection();
			dal.BeginTransaction();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			try
			{
				var fst = dataColl.First();
				cmd.Parameters.AddWithValue("@BuildingId", fst.BuildingId);
				cmd.Parameters.AddWithValue("@FloorId", fst.FloorId);
				cmd.Parameters.AddWithValue("@RoomTypeId", fst.RoomTypeId);
				cmd.Parameters.AddWithValue("@RoomId", fst.RoomId);
				cmd.CommandText = "usp_DelAssetsDetailById";
				cmd.ExecuteNonQuery();

				foreach (var beData in dataColl)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@BranchId", beData.BranchId);
					cmd.Parameters.AddWithValue("@BuildingId", beData.BuildingId);
					cmd.Parameters.AddWithValue("@FloorId", beData.FloorId);
					cmd.Parameters.AddWithValue("@RoomTypeId", beData.RoomTypeId);
					cmd.Parameters.AddWithValue("@ProductId", beData.ProductId);
					cmd.Parameters.AddWithValue("@Qty", beData.Qty);
					cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@RoomId", beData.RoomId);
					cmd.CommandText = "usp_AddAssetsAllocation";
					cmd.ExecuteNonQuery();
				}
				dal.CommitTransaction();
				resVal.IsSuccess = true;
				resVal.ResponseMSG = "Assets Details Saved Successfully";
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




	

		
		public BE.AssetsMgmt.AssetsCollections getAllAssets(int UserId, int EntityId, int? BuildingId, int? FloorId, int? RoomTypeId, int? RoomId)
		{
			BE.AssetsMgmt.AssetsCollections dataColl = new BE.AssetsMgmt.AssetsCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
			cmd.Parameters.AddWithValue("@FloorId", FloorId);
			cmd.Parameters.AddWithValue("@RoomTypeId", RoomTypeId);
			cmd.Parameters.AddWithValue("@RoomId", RoomId);
			cmd.CommandText = "usp_GetAllAssets";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AssetsMgmt.Assets beData = new BE.AssetsMgmt.Assets();
					if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.BuildingId = reader.GetInt32(1);
					if (!(reader[2] is DBNull)) beData.FloorId = reader.GetInt32(2);
					if (!(reader[3] is DBNull)) beData.RoomTypeId = reader.GetInt32(3);
					if (!(reader[4] is DBNull)) beData.ProductId = reader.GetInt32(4);
					if (!(reader[5] is DBNull)) beData.Qty = reader.GetInt32(5);
					if (!(reader[6] is DBNull)) beData.Remarks = reader.GetString(6);
					if (!(reader[7] is DBNull)) beData.BuildingName = reader.GetString(7);
					if (!(reader[8] is DBNull)) beData.FloorName = reader.GetString(8);
					if (!(reader[9] is DBNull)) beData.ProductName = reader.GetString(9);
					if (!(reader[10] is DBNull)) beData.RoomName = reader.GetString(10);
					if (!(reader[11] is DBNull)) beData.RoomId = reader.GetInt32(11);
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


		public BE.AssetsMgmt.FloorwiseRoomCollections getAllFloorwiseRoom(int UserId, int EntityId, int? BuildingId, int? FloorId, int? RoomTypeId)
		{
			BE.AssetsMgmt.FloorwiseRoomCollections dataColl = new BE.AssetsMgmt.FloorwiseRoomCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.Parameters.AddWithValue("@BuildingId", BuildingId);
			cmd.Parameters.AddWithValue("@FloorId", FloorId);
			cmd.Parameters.AddWithValue("@RoomTypeId", RoomTypeId);
			cmd.CommandText = "usp_GetFloorwiseRoomById";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AssetsMgmt.FloorwiseRoom beData = new BE.AssetsMgmt.FloorwiseRoom();
					if (!(reader[0] is DBNull)) beData.FloorWiseRoomDetailsId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
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


		public BE.AssetsMgmt.AssetsProductCollections getAllAssetsProduct(int UserId, int EntityId)
		{
			BE.AssetsMgmt.AssetsProductCollections dataColl = new BE.AssetsMgmt.AssetsProductCollections();
			dal.OpenConnection();
			System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@EntityId", EntityId);
			cmd.CommandText = "usp_GetAllAssetsProduct";
			try
			{
				System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					BE.AssetsMgmt.AssetsProduct beData = new BE.AssetsMgmt.AssetsProduct();
					if (!(reader[0] is DBNull)) beData.ProductId = reader.GetInt32(0);
					if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
					if (!(reader[2] is DBNull)) beData.Code = reader.GetString(2);
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

