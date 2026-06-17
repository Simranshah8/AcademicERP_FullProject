using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class RoomDetails: ResponeValues
    {
        public int RoomDetailsId { get; set; }
        public string Name { get; set; }
        public int RoomNo { get; set; }
        public int NoFOBench { get; set; }
        public int NoOfColumns { get; set; }
        public int PerBenchStudents { get; set; }
    }
    public class RoomDetailsCollections : List<RoomDetails> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}