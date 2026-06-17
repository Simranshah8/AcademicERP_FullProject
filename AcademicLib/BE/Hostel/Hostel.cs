using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Hostel
{
   public class Hostel : ResponeValues
    {
        public Hostel()
        {
            FacilitiesColl = new List<string>();
        }
        public int? HostelId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int? WardenId { get; set; }
        public bool IsBoy { get; set; }
        public bool IsGirls { get; set; }
        public bool IsGuest { get; set; }
        public bool IsStaff { get; set; }
        public string WardenName { get; set; }
        public List<string> FacilitiesColl { get; set; }
    }
    public class HostelCollections : List<Hostel> {
        public HostelCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
