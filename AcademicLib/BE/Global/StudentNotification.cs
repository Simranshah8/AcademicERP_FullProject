using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public class StudentNotification  
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RegNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public DateTime TodayAD { get; set; }
        public string TodayBS { get; set; }
    }
    public class StudentNotificationCollections : System.Collections.Generic.List<StudentNotification>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}
