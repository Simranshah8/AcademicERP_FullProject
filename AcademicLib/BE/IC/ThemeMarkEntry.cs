using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ThemeMarkEntry: ResponeValues
    {
		public int? TranId { get; set; }
		public int AcademicYearId { get; set; }
		public int? ClassId { get; set; }
		public int? SectionId { get; set; }
		public int? BatchId { get; set; }
		public int? SemesterId { get; set; }
		public int? ClassYearId { get; set; }
		public int SubjectId { get; set; }
		public int LessonSno { get; set; }
		public int? AssessmentTypeId { get; set; }
		public DateTime? AssessmentDate { get; set; }
		public int StudentId { get; set; }
		public double AchievementNo { get; set; }
		public double FullMarks { get; set; }
		public double? ObtainMarks { get; set; }
		public string Remarks { get; set; } = "";
	}
    public class ThemeWiseMarkEntry
    {
		public int? StudentId { get; set; }
		public int AcademicYearId { get; set; }
		public string StudentName { get; set; } = "";
		public string RegdNo { get; set; } = "";
		public int? RollNo { get; set; }
		public int? ClassId { get; set; }
		public int? SectionId { get; set; }
		public string SectionName { get; set; } = "";
		public int? BatchId { get; set; }
		public int? SemesterId { get; set; }
		public int? ClassYearId { get; set; }
		public int SubjectId { get; set; }
		public int LessonSno { get; set; }
		public double AchievementNo { get; set; }
		public double FullMarks { get; set; }
		public double? ObtainMarks { get; set; }
		public string Remarks { get; set; } = "";
		public int? AssessmentTypeId { get; set; }
		public DateTime? AssessmentDate { get; set; }
	}
	public class ThemeWiseMarkEntryCollections : List<ThemeWiseMarkEntry>
    {
        public ThemeWiseMarkEntryCollections()
        {
			ResponseMSG = "";
		}
		public string ResponseMSG { get; set; }
		public bool IsSuccess { get; set; }
	}
}
