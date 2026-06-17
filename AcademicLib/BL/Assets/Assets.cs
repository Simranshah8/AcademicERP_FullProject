using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AssetsMgmt
{

	public class Assets
	{

		DA.AssetsMgmt.AssetsDB db = null;

		int _UserId = 0;

		public Assets(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AssetsMgmt.AssetsDB(hostName, dbName);
		}
		
		public ResponeValues SaveFormData(BE.AssetsMgmt.AssetsCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();

			resVal = db.SaveUpdate(_UserId, dataColl);

			return resVal;
		}
		public BE.AssetsMgmt.AssetsCollections GetAllAssets(int EntityId, int? BuildingId, int? FloorId, int? RoomTypeId, int? RoomId)
		{
			return db.getAllAssets(_UserId, EntityId, BuildingId, FloorId, RoomTypeId, RoomId);
		}

		public BE.AssetsMgmt.FloorwiseRoomCollections GetAllFloorwiseRoom(int EntityId, int? BuildingId, int? FloorId, int? RoomTypeId)
		{
			return db.getAllFloorwiseRoom(_UserId, EntityId, BuildingId, FloorId, RoomTypeId);
		}

		public BE.AssetsMgmt.AssetsProductCollections GetAllAssetProduct(int EntityId)
		{
			return db.getAllAssetsProduct(_UserId, EntityId);
		}

		public ResponeValues IsValidData(ref BE.AssetsMgmt.Assets beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.TranId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.TranId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else
				{
					resVal.IsSuccess = true;
					resVal.ResponseMSG = "Valid";
				}
			}
			catch (Exception ee)
			{
				resVal.IsSuccess = false;
				resVal.ResponseMSG = ee.Message;
			}
			return resVal;
		}
	}

}

