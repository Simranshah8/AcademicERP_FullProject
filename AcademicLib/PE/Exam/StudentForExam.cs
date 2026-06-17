using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Exam
{
    public class StudentForExam : Dynamic.Accounting.IReportLoadObjectData
    {

        public StudentForExam(List<AcademicLib.RE.Exam.StudentForExam> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Exam.StudentForExam> dataColl = null;
        public System.Collections.IEnumerable DataColl
        {
            get
            {
                return dataColl;
            }
        }

        public string ReportPath
        {
            get
            {
                return "";
            }
        }
        public void GetDataOfCurrentRow(object obj, Dynamic.ReportEngine.RDL.Row row)
        {
            //StudentId, AutoNumber, RegNo, BoardRegNo, BoardName, Name, RollNo, ClassName, SectionName, SymbolNo, SubjectDetails,
            //SubjectCodeDetails, SubjectDetailsWExamDate, SubjectDetailsWExamDateTime 

            AcademicLib.RE.Exam.StudentForExam csl = (AcademicLib.RE.Exam.StudentForExam)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.AutoNumber;
            row.Data[2] = csl.RegNo;
            row.Data[3] = csl.BoardRegNo;
            row.Data[4] = csl.BoardName;
            row.Data[5] = csl.Name;
            row.Data[6] = csl.RollNo;
            row.Data[7] = csl.ClassName;
            row.Data[8] = csl.SectionName;
            row.Data[9] = csl.SymbolNo;
            row.Data[10] = csl.SubjectDetails;
            row.Data[11] = csl.SubjectCodeDetails;
            row.Data[12] = csl.SubjectDetailsWExamDate;
            row.Data[13] = csl.SubjectDetailsWExamDateTime;

            try
            {
                row.Data[14] = csl.TotalSubject;
                row.Data[15] = csl.Room;
                row.Data[16] = csl.RowName;
                row.Data[17] = csl.BenchNo;
                row.Data[18] = csl.BenchOrdinalNo;
                row.Data[19] = csl.SeatCol;
                row.Data[20] = csl.ExamShiftName;

            }
            catch { }

            
        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
