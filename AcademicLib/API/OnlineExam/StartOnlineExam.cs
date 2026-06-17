using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.OnlineExam
{
    public class StartOnlineExam : ResponeValues
    {
        public int ExamSetupId { get; set; }
        public string Location { get; set; }
        public double Lat { get; set; }
        public double Lan { get; set; }
        public string IPAddress { get; set; } 
        public string ImagePath { get; set; }
        public string Notes { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
