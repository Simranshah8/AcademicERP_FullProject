using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Exam.Transaction
{

	public class BenchColumn : ResponeValues
	{

		public int? BenchColumnId { get; set; }
		public string BenchTypeName { get; set; } = "";
		public int? NoOfColumn { get; set; }
		public int OrderNo { get; set; }
		public string ColumnDetails { get; set; } = "";
		public BenchColumn()
		{
			BenchColumnDetailColl = new BenchColumnDetailCollections();
		}
		public BenchColumnDetailCollections BenchColumnDetailColl { get; set; }
	}
	public class BenchColumnDetail
	{

		public int BenchColumnId { get; set; }
		public int ColSNo { get; set; }
		public string ColumnName { get; set; } = "";
	}

	public class BenchColumnDetailCollections : System.Collections.Generic.List<BenchColumnDetail>
	{

		public string ResponseMSG { get; set; } = "";

		public bool IsSuccess { get; set; }

	}

	public class BenchColumnCollections : System.Collections.Generic.List<BenchColumn>
	{
		public BenchColumnCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}

