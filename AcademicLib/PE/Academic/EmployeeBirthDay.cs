using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Academic
{
    public class EmployeeBirthDay : Dynamic.Accounting.IReportLoadObjectData
    {

        public EmployeeBirthDay(List<AcademicLib.RE.Academic.EmployeeBirthDay> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Academic.EmployeeBirthDay> dataColl = null;
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
            //EmployeeId, UserId, Code, Name, Department, Designation, EnrollNo, FatherName, ContactNo, Address, AgeYear, AgeMonth, AgeDay, PhotoPath,
            //DOB_AD, DOB_BS, Age 
            
            AcademicLib.RE.Academic.EmployeeBirthDay csl = (AcademicLib.RE.Academic.EmployeeBirthDay)obj;
            row.Data[0] = csl.EmployeeId;
            row.Data[1] = csl.UserId;
            row.Data[2] = csl.Code;
            row.Data[3] = csl.Name;
            row.Data[4] = csl.Department;
            row.Data[5] = csl.Designation;
            row.Data[6] = csl.EnrollNo;
            row.Data[7] = csl.FatherName;
            row.Data[8] = csl.ContactNo;
            row.Data[9] = csl.Address;
            row.Data[10] = csl.AgeYear;
            row.Data[11] = csl.AgeMonth;
            row.Data[12] = csl.AgeDay;
            row.Data[13] = csl.PhotoPath;
            row.Data[14] = csl.DOB_AD;
            row.Data[15] = csl.DOB_BS; 
            row.Data[16] = csl.Age;

        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
