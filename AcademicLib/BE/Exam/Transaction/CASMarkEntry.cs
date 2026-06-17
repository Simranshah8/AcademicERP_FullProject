using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
  public  class CASMarkEntry : ResponeValues
    {
        public  int TranId { get; set; }
        public  int ClassId { get; set; }
        public  int TeacherId { get; set; }
        public  int SubjectId { get; set; }
        public  DateTime Date { get; set; }
        public  int StudentId { get; set; }
        public  bool IsCastTypeName { get; set; }
        public  string Remarks { get; set; }
     
    }
public class CASMarkEntryCollections : List<CASMarkEntry> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
