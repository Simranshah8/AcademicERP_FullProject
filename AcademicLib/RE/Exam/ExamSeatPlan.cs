using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class ExamSeatPlanDetails : ResponeValues
    {
        public ExamSeatPlanDetails()
        {
            ExamSeatPlanColl = new List<ExamSeatPlan>();
            RoomSummaryColl = new List<RoomSummary>();
        }
        public List<ExamSeatPlan>  ExamSeatPlanColl { get; set; }
        public List<RoomSummary> RoomSummaryColl { get; set; }
        public List<RoomSummary> ClassSummaryColl { get; set; } = new List<RoomSummary>();

    }
    public class ExamSeatPlan
    {
        public string RoomName { get; set; }
        public string RowName { get; set; }
        public int BanchNo { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }

        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Class1 { get; set; }
        public string Class2 { get; set; }
        public string Class3 { get; set; }
        public string Class4 { get; set; }
        public string Class5 { get; set; }
        public string RollNo1 { get; set; }
        public string RollNo2 { get; set; }
        public string RollNo3 { get; set; }
        public string RollNo4 { get; set; }
        public string RollNo5 { get; set; }

        public string RegNo1 { get; set; }
        public string RegNo2 { get; set; }
        public string RegNo3 { get; set; }
        public string RegNo4 { get; set; }
        public string RegNo5 { get; set; }

        public string SymbolNo1 { get; set; }
        public string SymbolNo2 { get; set; }
        public string SymbolNo3 { get; set; }
        public string SymbolNo4 { get; set; }
        public string SymbolNo5 { get; set; }

        public string BenchOrdinalNo { get; set; }
        
    }
    
    public class RoomSummary
    {
        public RoomSummary()
        {
            RoomName = "";
            ClassName = "";
            SectionName = "";
            ClassSectionName = "";
            RollNo = "";
            RegNo = "";
            SymbolNo = "";
        }
        public string RoomName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string ClassSectionName { get; set; }
        public string RollNo { get; set; }
        public string RegNo { get; set; }
        public string SymbolNo { get; set; }
        public int OrderNo { get; set; }
        public int NoOfStudent { get; set; }

    }
    
}
