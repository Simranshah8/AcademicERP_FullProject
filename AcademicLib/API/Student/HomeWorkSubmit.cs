using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Student
{
    public class HomeWorkSubmit
    {        
        public int UserId { get; set; }
        public int HomeWorkId { get; set; }
        public string Notes { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
    }

    public class AssignmentSubmit
    {
        public int UserId { get; set; }
        public int AssignmentId { get; set; }
        public string Notes { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
    }
}
