using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Hostel
{
    public class Room : ResponeValues
    {
        public int? RoomId { get; set; }
        public int HostelId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public string RoomName { get; set; }
        public double RoomFee { get; set; }
        public int NoOfBeds { get; set; }
        public string ImagePath { get; set; }

        private RoomBedCollections _RoomBedCollections = new RoomBedCollections();
        public RoomBedCollections RoomBedColl
        {
            get { return _RoomBedCollections; }
            set { _RoomBedCollections = value; }
        }

        private RoomAssetCollections _RoomAssetCollections = new RoomAssetCollections();
        public RoomAssetCollections RoomAssetColl
        {
            get { return _RoomAssetCollections; }
            set { _RoomAssetCollections = value; }
        }

        public string HostelName { get; set; }
        public string BuildingName { get; set; }

        public string FloorName { get; set; }

        public bool UpdateInMapping { get; set; }


    }
    public class RoomCollections : List<Room> {
        public RoomCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class RoomBed
    {
        public int RoomId { get; set; }
        public int BedNo { get; set; }
        public string BedName { get; set; }

    }
    public class RoomBedCollections : List<RoomBed> { }
    public class RoomAsset
    {
      public int RoomId { get; set; }
      public string Particulars { get; set; }
      public double Qty { get; set; }

    }
    public class RoomAssetCollections : List<RoomAsset> { }

    public class RoomDetailsForMapping
    {
        public int? RoomId { get; set; }
        public int? BedId { get; set; }
        public int BedNo { get; set; }
        public string Name { get; set; }
        public double RoomFee { get; set; }
        public bool IsVacant { get; set; }
    }
    public class RoomDetailsForMappingCollections : System.Collections.Generic.List<RoomDetailsForMapping>
    {
        public RoomDetailsForMappingCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
