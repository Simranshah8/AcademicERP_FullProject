using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic
{
    public class Common : ResponeValues
    {
        public Common()
        {
            Code = "";
            Name = "";
            Description = "";
            DisplayName = "";
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string text
        {
            get
            {
                return Name;
            }
        }


        public int? Api_Id { get; set; }
        public string Api_ResponseId { get; set; }
        public DateTime? LastApiCallAt { get; set; }
        public string LastResponse { get; set; }
    }
}
