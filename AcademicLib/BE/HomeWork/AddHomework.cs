using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.HomeWork
{
	public class TeacherName : ResponeValues
    {
        public int EmployeeId { get; set; }
		public string Name { get; set; } = "";
		public string EmployeeCode { get; set; } = "";
    }
	public class TeacherNameCollections : System.Collections.Generic.List<TeacherName>
	{
		public TeacherNameCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

	public class TeacherWiseClass : ResponeValues
	{
		public int? TranId { get; set; }
		public int ClassId { get; set; }
		public string Name { get; set; } = "";
	}

	public class TeacherWiseClassCollections : System.Collections.Generic.List<TeacherWiseClass>
	{
		public TeacherWiseClassCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}