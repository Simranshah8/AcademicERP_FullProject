using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.AssetsMgmt
{

	public class Assets : ResponeValues
	{

		public int? TranId { get; set; }
		public int? BranchId { get; set; }
		public int? BuildingId { get; set; }
		public int? FloorId { get; set; }
		public int? RoomTypeId { get; set; }
		public int? RoomId { get; set; }
		public int? ProductId { get; set; }
		public int? Qty { get; set; }
		public string Remarks { get; set; } = "";
		public string BuildingName { get; set; } = "";
		public string FloorName { get; set; } = "";
		public string ProductName { get; set; } = "";
		public string RoomName { get; set; } = "";
	}

	public class AssetsCollections : System.Collections.Generic.List<Assets>
	{
		public AssetsCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class FloorwiseRoom : ResponeValues
	{

		public int? FloorWiseRoomDetailsId { get; set; }	
		public string Name { get; set; } = "";
	}

	public class FloorwiseRoomCollections : System.Collections.Generic.List<FloorwiseRoom>
	{
		public FloorwiseRoomCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class AssetsProduct : ResponeValues
	{

		public int? ProductId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; } = "";
	}

	public class AssetsProductCollections : System.Collections.Generic.List<AssetsProduct>
	{
		public AssetsProductCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}
