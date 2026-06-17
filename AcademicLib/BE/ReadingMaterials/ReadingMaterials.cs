using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.ReadingMaterials
{
    public class ReadingMaterials: ResponeValues
    {
		public ReadMatContentCollection ReadMatContentColl { get; set; }
		public ReadMatVideoCollection ReadMadVideoColl { get; set; }
		public ReadingMaterials()
        {
			ReadMatContentColl = new ReadMatContentCollection();
			ReadMadVideoColl = new ReadMatVideoCollection();
        }
		public class ReadMatVideo : ResponeValues
		{
			public int? ReadMatVideoId { get; set; }
			public int? BatchId { get; set; }
			public string BatchName { get; set; } = "";
			public int? ClassId { get; set; }
			public string ClassName { get; set; } = "";
			public int? SemesterId { get; set; }
			public string SemesterName { get; set; } = "";
			public int? ClassYearId { get; set; }
			public string ClassYearName { get; set; } = "";
			public int? SubjectId { get; set; }
			public string SubjectName { get; set; } = "";
			public string VideoTitle { get; set; } = "";
			public string VideoLink { get; set; } = "";
			public string Remarks { get; set; } = "";
		}
		public class ReadMatVideoCollection: System.Collections.Generic.List<ReadMatVideo>
        {
			public bool IsSuccess { get; set; }
			public string ResponseMSG { get; set; }
        }
		public class ReadMatContent : ResponeValues
		{

			public int? ReadMatContId { get; set; }
			public int? BatchId { get; set; }
			public string BatchName { get; set; } = "";
			public int? ClassId { get; set; }
			public string ClassName { get; set; } = "";
			public int? SemesterId { get; set; }
			public string SemesterName { get; set; } = "";
			public int? ClassYearId { get; set; }
			public string ClassYearName { get; set; } = "";
			public int? SubjectId { get; set; }
			public string SubjectName { get; set; } = "";
			public string Title { get; set; } = "";
			public Dynamic.BusinessEntity.GeneralDocumentCollections ContentAttach { get; set;}
			public ReadMatContent()
            {
				ContentAttach = new Dynamic.BusinessEntity.GeneralDocumentCollections();
            }
		}
		public class ReadMatContentCollection : System.Collections.Generic.List<ReadMatContent>
		{
			public bool IsSuccess { get; set; }
			public string ResponseMSG { get; set; }
		}
	}
}