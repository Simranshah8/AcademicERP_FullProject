using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ClassShift : ResponeValues
    {
        public ClassShift()
        {
            IsActive = true;
        }
        public int? ClassShiftId { get; set; }
        public string Name { get; set; }
        public int WeeklyDayOff { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? AbsentNoticeTime { get; set; }
        public int NoofBreak { get; set; }
        public int Duration { get; set; }

        public int id
        {
            get
            {
                if (ClassShiftId.HasValue)
                    return ClassShiftId.Value;
                return 0;
            }
        }
      
        public string text
        {
            get
            {
                return Name;
            }
        }

        public bool ForOnlineClass { get; set; }
        public bool IsActive { get; set; }
        public int? SecondWeeklyDayOff { get; set; }
        
    }
    public class ClassShiftCollections : List<ClassShift> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
