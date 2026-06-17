using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Setup
{
    public class Roles : ResponeValues
    {
        public int ForUser { get; set; } //--1=Student,2=Employee,3=Admin
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
    public class RolesCollections : System.Collections.Generic.List<Roles>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}
