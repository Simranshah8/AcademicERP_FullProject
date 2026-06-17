using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Academic
{
    public class StudentBirthDay : Dynamic.Accounting.IReportLoadObjectData
    {

        public StudentBirthDay(List<AcademicLib.RE.Academic.StudentBirthDay> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Academic.StudentBirthDay> dataColl = null;
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
            //StudentId, UserId, RegdNo, Name, ClassName, SectionName, RollNo, FatherName, ContactNo, Address, AgeYear, AgeMonth, AgeDay,
            //PhotoPath, DOB_AD, DOB_BS, ClassSection, Age  

            AcademicLib.RE.Academic.StudentBirthDay csl = (AcademicLib.RE.Academic.StudentBirthDay)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.UserId;
            row.Data[2] = csl.RegdNo;
            row.Data[3] = csl.Name;
            row.Data[4] = csl.ClassName;
            row.Data[5] = csl.SectionName;
            row.Data[6] = csl.RollNo;
            row.Data[7] = csl.FatherName;
            row.Data[8] = csl.ContactNo;
            row.Data[9] = csl.Address;
            row.Data[10] = csl.AgeYear;
            row.Data[11] = csl.AgeMonth;
            row.Data[12] = csl.AgeDay;
            row.Data[13] = csl.PhotoPath;
            row.Data[14] = csl.DOB_AD;
            row.Data[15] = csl.DOB_BS;
            row.Data[16] = csl.ClassSection;
            row.Data[17] = csl.Age;
             
        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
