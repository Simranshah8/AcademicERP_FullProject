using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BE
{

	public class Examiner : ResponeValues
	{

		public int? ExaminerId { get; set; }
		public string Name { get; set; } = "";
		public string Designation { get; set; } = "";
		public string ExaminerRegdNo { get; set; } = "";
		public string MobileNo { get; set; } = "";
		public string Email { get; set; } = "";
		public string Qualification { get; set; } = "";
		public string Specialization { get; set; } = "";
		public int? UsernameId { get; set; }
		public string Address { get; set; } = "";
		public string Remarks { get; set; } = "";
		public byte[] Photo { get; set; }
		public string PhotoPath { get; set; } = "";
		public string SPhotoPath { get; set; } = "";
		public string UserName { get; set; } = "";
		public Examiner()
		{
			AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
		}
		public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
	}
	
	
	public class ExaminerCollections : System.Collections.Generic.List<Examiner>
	{
		public ExaminerCollections()
		{
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}

}