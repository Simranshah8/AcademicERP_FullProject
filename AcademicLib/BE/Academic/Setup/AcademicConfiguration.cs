using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Setup
{
    public class AcademicConfiguration : ResponeValues
    {
        public bool ActiveLevel { get; set; }
        public bool ActiveFaculty { get; set; }
        public bool ActiveSemester { get; set; }
        public bool ActiveBatch { get; set; }
        public bool ActiveClassYear { get; set; }

        public int? BranchId { get; set; }

        //New Field Added by Suresh on 15 Poush
        public bool ActiveClassWiseMonth { get; set; }
        public bool SectionWiseSetup { get; set; }

        public bool SectionWiseSubjectMapping { get; set; }
        public bool SectionWiseExamSchedule { get; set; }
        public bool SectionWiseMarkSetup { get; set; }
        public bool SectionWiseLessonPlan { get; set; }
    }
}
