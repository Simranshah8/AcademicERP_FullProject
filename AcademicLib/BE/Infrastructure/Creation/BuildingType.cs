using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Infrastructure.Creation
{
    public class BuildingType : ResponeValues
    {
        public int? BuildingTypeId { get; set; }
        public int? OrderNo { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

    }
    public class BuildingTypeCollections : List<BuildingType>
    {
        public BuildingTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}