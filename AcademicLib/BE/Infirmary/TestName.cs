using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class TestName : ResponeValues
	{

		public int? TestNameId { get; set; }
		public string Name { get; set; } = "";
		public int? CheckupGroupId { get; set; }
		public int OrderNo { get; set; }
		public int? InputTextType { get; set; }
		public string SampleCollection { get; set; } = "";
		public string SampleUnit { get; set; } = "";
		public string Description { get; set; } = "";
		public string CheckUpGroupName { get; set; } = "";
		public TestName()
		{
			TestNameLabDetailColl = new TestNameLabDetailCollections();
		}
		public TestNameLabDetailCollections TestNameLabDetailColl { get; set; }
	}
	public class TestNameLabDetail
	{

		public int TestNameId { get; set; }
		public string Name { get; set; } = "";
		public int DefaultValue { get; set; }
		public string NormalRange { get; set; } = "";
		public string LowerRange { get; set; } = "";
		public string UpperRange { get; set; } = "";
		public string GroupName { get; set; } = "";
		public string Remarks { get; set; } = "";
	}

	public class TestNameLabDetailCollections : System.Collections.Generic.List<TestNameLabDetail>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class TestNameCollections : System.Collections.Generic.List<TestName>
	{
		public TestNameCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}