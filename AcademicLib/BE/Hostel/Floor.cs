using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Hostel
{
    public class Floor : ResponeValues
    {
        public int? FloorId { get; set; }
        public string Name { get; set; }
       
    }
    public class FloorCollections : List<Floor> {
        public FloorCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
